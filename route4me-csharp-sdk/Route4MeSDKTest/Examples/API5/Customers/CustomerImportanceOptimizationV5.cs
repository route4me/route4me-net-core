using System;
using System.Linq;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Customers;
using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of optimizing a route by customer importance:
        /// create an importance level, assign it to a customer, and run an optimization
        /// with use_customer_importance, where one stop sets its own importance.
        /// The optimization ignores use_customer_importance if the account does not have the feature.
        /// </summary>
        public void CustomerImportanceOptimizationV5()
        {
            var route4MeV5 = new Route4MeManagerV5(ActualApiKey);
            var route4Me = new Route4MeManager(ActualApiKey);

            // 1. Create an importance level. Lower sort order is more important.
            var priority = route4MeV5.CreateCustomerPriority(
                new CustomerPriorityRequest { Name = "VIP " + DateTime.Now.ToString("yyyyMMddHHmmss"), SortOrder = 1 },
                out var priorityResponse);

            if (priority == null)
            {
                PrintTestCustomer(null, priorityResponse);
                return;
            }

            // 2. Create a customer with that importance level. The API requires an accountable team member.
            var customer = route4MeV5.CreateCustomer(
                new StoreRequest
                {
                    Name = "VIP Customer " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Status = 1,
                    AccountablePersonId = route4MeV5.GetTeamMembers(out _)?.FirstOrDefault()?.MemberId,
                    PriorityId = priority.CustomerPriorityId
                },
                out var customerResponse);

            try
            {
                if (customer == null)
                {
                    PrintTestCustomer(null, customerResponse);
                    return;
                }

                Console.WriteLine($"Customer {customer.CustomerId} has importance {customer.PriorityLabel}");

                // 3. Optimize. A stop uses its customer's importance when the stop comes from an address book
                //    contact or an order that belongs to the customer; a customer_id sent on the stop is ignored here.
                var optimizationParameters = new OptimizationParameters
                {
                    Addresses = new[]
                    {
                        new Address
                        {
                            AddressString = "3634 W Market St, Fairlawn, OH 44333",
                            IsDepot = true,
                            Latitude = 41.135762259364,
                            Longitude = -81.629313826561
                        },
                        new Address
                        {
                            AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                            Latitude = 41.143505096435,
                            Longitude = -81.46549987793
                        },
                        new Address
                        {
                            AddressString = "512 Florida Pl, Barberton, OH 44203",
                            Latitude = 41.003671512008,
                            Longitude = -81.598461046815,
                            // The stop's own importance wins over its customer's. 1 is the most important.
                            Importance = 2
                        },
                        new Address
                        {
                            AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                            Latitude = 41.162971496582,
                            Longitude = -81.479049682617
                        }
                    },
                    Parameters = new RouteParameters
                    {
                        RouteName = "Customer importance " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                        AlgorithmType = AlgorithmType.TSP,
                        Optimize = Optimize.Distance.Description(),
                        DistanceUnit = DistanceUnit.MI.Description(),
                        UseCustomerImportance = true
                    }
                };

                var dataObject = route4Me.RunOptimization(optimizationParameters, out var errorString);

                PrintExampleOptimizationResult(dataObject, errorString);

                foreach (var address in dataObject?.Addresses ?? Array.Empty<Address>())
                {
                    Console.WriteLine($"{address.AddressString}: importance {address.Importance}");
                }

                if (dataObject?.OptimizationProblemId != null)
                {
                    route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out _);
                }
            }
            finally
            {
                // 4. Clean up the test customer and importance level.
                if (customer != null)
                {
                    route4MeV5.DeleteCustomer(new CustomerIdParameters { CustomerId = customer.CustomerId }, out _);
                }

                if (!route4MeV5.DeleteCustomerPriority(priority.CustomerPriorityId, out var deleteResponse))
                {
                    PrintTestCustomer(null, deleteResponse);
                }
            }
        }
    }
}