using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: get a paginated destinations list filtered by route ID (API V5).
    /// Demonstrates reading destination-level custom key/value data (custom_fields).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets a paginated list of route destinations for a single route and prints each
        /// stop's custom_fields (destination-level custom key/value data).
        /// Uses POST /route-destinations/list.
        /// </summary>
        public void GetRouteDestinationsListV5()
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
            const string tag = "GetRouteDestinationsListV5";

            Console.WriteLine("");

            var request = new GetDestinationsRequest
            {
                Filters = new DestinationFilters { RouteId = routeId },
                Fields = new[] { "route_destination_id", "address_stop_type", "stop_status_id", "custom_fields" },
                Page = 1,
                PerPage = 50
            };

            var result = route4Me.GetDestinationsList(request, out ResultResponse resultResponse);

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("{0} executed successfully. Items returned: {1}",
                    tag, result.Items?.Length ?? 0);

                if (result.Items != null)
                {
                    foreach (var item in result.Items)
                    {
                        Console.WriteLine("  destination_id={0}, stop_type={1}, status={2}",
                            item.RouteDestinationId, item.AddressStopType, item.StopStatusId ?? "(none)");

                        // Print destination-level custom key/value data
                        PrintDestinationCustomFields(item.CustomFields);
                    }
                }
            }
            else
            {
                PrintFailResponse(resultResponse, tag);
            }

            if (isInnerExample) RemoveTestOptimizations();
        }

        private static void PrintDestinationCustomFields(Dictionary<string, string> customFields)
        {
            if (customFields == null || customFields.Count == 0)
            {
                Console.WriteLine("    custom_fields: (none)");
                return;
            }

            Console.WriteLine("    custom_fields:");
            foreach (var kvp in customFields)
                Console.WriteLine("      {0}: {1}", kvp.Key, kvp.Value);
        }
    }
}