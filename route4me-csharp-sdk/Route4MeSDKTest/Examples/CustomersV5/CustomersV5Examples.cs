using System;

using Route4MeSDKLibrary.DataTypes.V5.Customers;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void CustomersV5Examples()
        {
            var route4Me = new CustomerManagerV5(ActualApiKey);

            var createRequest = new StoreRequest()
            {
                Name = "Test Customer " + (new Random()).Next(),
                ExternalId = "EXT-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                Status = 1,
                Currency = "USD",
                TaxId = "TAX-" + (new Random()).Next(1000, 9999)
            };

            var createdCustomer = route4Me.CreateCustomer(createRequest, out var createResponse);
            if (createdCustomer != null)
            {
                Console.WriteLine($"Customer created: {createdCustomer.CustomerId}, {createdCustomer.Name}");
            }
            else
            {
                PrintTestCustomer(createdCustomer, createResponse);
                return;
            }

            var getParams = new CustomerIdParameters()
            {
                CustomerId = createdCustomer.CustomerId
            };

            var retrievedCustomer = route4Me.GetCustomerById(getParams, out var getResponse);
            if (retrievedCustomer != null)
            {
                Console.WriteLine($"Customer loaded: {retrievedCustomer.CustomerId}, {retrievedCustomer.Name}");
            }
            else
            {
                PrintTestCustomer(retrievedCustomer, getResponse);
                return;
            }

            var updateParams = new UpdateCustomerParameters()
            {
                CustomerId = createdCustomer.CustomerId,
                Name = "Updated Name " + DateTime.Now.ToString("HHmmss"),
                Status = 0
            };

            var updatedCustomer = route4Me.UpdateCustomer(updateParams, out var updateResponse);
            if (updatedCustomer != null)
            {
                Console.WriteLine($"Customer updated: {updatedCustomer.CustomerId}, {updatedCustomer.Name}");
            }
            else
            {
                PrintTestCustomer(updatedCustomer, updateResponse);
                return;
            }

            var listParams = new CustomerListParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var customersList = route4Me.GetCustomersList(listParams, out var listResponse);
            if (customersList != null && customersList.Items != null && customersList.Items.Count > 0)
            {
                Console.WriteLine($"Total Customers: {customersList.Items.Count}");
                foreach (var c in customersList.Items)
                {
                    Console.WriteLine($"Customer ID: {c.CustomerId}, Name: {c.Name}, Status: {c.Status}");
                }
            }
            else
            {
                PrintTestCustomer(customersList, listResponse);
            }

            var deleteParams = new CustomerIdParameters()
            {
                CustomerId = createdCustomer.CustomerId
            };

            var deleteResult = route4Me.DeleteCustomer(deleteParams, out var deleteResponse);
            if (deleteResult != null && (deleteResult.Status == true || deleteResult.Code == 200))
            {
                Console.WriteLine($"Customer deleted successfully: {createdCustomer.CustomerId}");
            }
            else
            {
                PrintTestCustomer(deleteResult, deleteResponse);
            }
        }
    }
}