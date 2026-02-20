using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets the custom data for a specific route using the dedicated
        /// GET /route-custom-data/{route_id} endpoint (API V5).
        /// </summary>
        public void GetRouteCustomDataV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            bool isInnerExample = SD10Stops_route_id_V5 == null;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10StopsV5();
                OptimizationsToRemove = new System.Collections.Generic.List<string>
                {
                    SD10Stops_optimization_problem_id_V5
                };
            }

            var routeId = SD10Stops_route_id_V5;

            // Act - retrieve route custom data
            var result = route4Me.GetRouteCustomDataDedicated(routeId, out ResultResponse resultResponse);

            Console.WriteLine("");

            if (resultResponse == null)
            {
                Console.WriteLine("GetRouteCustomDataV5 executed successfully");

                if (result != null && result.Count > 0)
                {
                    Console.WriteLine($"Route '{routeId}' has {result.Count} custom data entries:");
                    foreach (var kvp in result)
                    {
                        Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                    }
                }
                else
                {
                    Console.WriteLine($"Route '{routeId}' has no custom data.");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "GetRouteCustomDataV5");
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}