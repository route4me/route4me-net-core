using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets the custom data for multiple routes in a single request using the dedicated
        /// POST /route-custom-data/bulk endpoint (API V5).
        /// </summary>
        public void GetBulkRouteCustomDataV5()
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

            var routeIds = new[] { SD10Stops_route_id_V5 };

            // Act - retrieve custom data for multiple routes at once
            var result = route4Me.GetBulkRouteCustomData(routeIds, out ResultResponse resultResponse);

            Console.WriteLine("");

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("GetBulkRouteCustomDataV5 executed successfully");
                Console.WriteLine($"Received custom data for {result.Data?.Count ?? 0} route(s):");

                if (result.Data != null)
                {
                    foreach (var routeEntry in result.Data)
                    {
                        Console.WriteLine($"  Route ID: {routeEntry.Key}");
                        if (routeEntry.Value != null && routeEntry.Value.Count > 0)
                        {
                            foreach (var kvp in routeEntry.Value)
                            {
                                Console.WriteLine($"    {kvp.Key}: {kvp.Value}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("    (no custom data)");
                        }
                    }
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "GetBulkRouteCustomDataV5");
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}