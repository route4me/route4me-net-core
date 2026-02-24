using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: get a single route destination by ID (API V5).
    /// Demonstrates accessing destination-level custom key/value data (custom_fields).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Fetches the first destination of a route by its integer ID and prints its
        /// custom_fields (destination-level custom key/value data).
        /// Uses GET /route-destinations/{id}.
        /// </summary>
        public void GetRouteDestinationV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            bool isInnerExample = SD10Stops_route_id_V5 == null;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10StopsV5();
                OptimizationsToRemove = new List<string>
                {
                    SD10Stops_optimization_problem_id_V5
                };
            }

            var routeId = SD10Stops_route_id_V5;
            const string tag = "GetRouteDestinationV5";

            Console.WriteLine("");

            // Step 1: get the list to find a valid destination ID
            var listRequest = new GetDestinationsRequest
            {
                Filters = new DestinationFilters { RouteId = routeId },
                Page = 1,
                PerPage = 1
            };

            var listResult = route4Me.GetDestinationsList(listRequest, out ResultResponse listResponse);

            if (listResponse != null || listResult?.Items == null || listResult.Items.Length == 0)
            {
                Console.WriteLine("{0}: Could not retrieve destination list.", tag);
                PrintFailResponse(listResponse, tag);
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }

            var destinationId = listResult.Items[0].RouteDestinationId?.ToString();

            // Step 2: get the destination by its ID
            Console.WriteLine("{0}: Getting destination id={1}", tag, destinationId);
            var result = route4Me.GetDestination(destinationId, out ResultResponse resultResponse);

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("{0} executed successfully.", tag);
                Console.WriteLine("  destination_id={0}", result.RouteDestinationId);
                Console.WriteLine("  stop_type={0}", result.AddressStopType);
                Console.WriteLine("  status={0}", result.StopStatusId ?? "(none)");
                Console.WriteLine("  customer_id={0}", result.CustomerId ?? "(none)");
                Console.WriteLine("  is_visited={0}", result.IsVisited);

                // Print destination-level custom key/value data
                if (result.CustomFields == null || result.CustomFields.Count == 0)
                {
                    Console.WriteLine("  custom_fields: (none)");
                }
                else
                {
                    Console.WriteLine("  custom_fields:");
                    foreach (var kvp in result.CustomFields)
                        Console.WriteLine("    {0}: {1}", kvp.Key, kvp.Value);
                }
            }
            else
            {
                PrintFailResponse(resultResponse, tag);
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
