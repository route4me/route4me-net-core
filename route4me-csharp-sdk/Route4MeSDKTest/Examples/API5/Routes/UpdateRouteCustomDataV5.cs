using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Updates (replaces) the custom data for a specific route using the dedicated
        /// PUT /route-custom-data/{route_id} endpoint (API V5).
        /// All existing custom data keys not present in the request are removed.
        /// </summary>
        public void UpdateRouteCustomDataV5()
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

            var customData = new Dictionary<string, string>
            {
                { "project_code", "PRJ-2024-001" },
                { "priority", "high" },
                { "region", "northeast" }
            };

            // Act - update route custom data
            var result = route4Me.UpdateRouteCustomData(routeId, customData, out ResultResponse resultResponse);

            Console.WriteLine("");

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("UpdateRouteCustomDataV5 executed successfully");
                Console.WriteLine($"Updated custom data for route '{routeId}':");
                foreach (var kvp in result)
                {
                    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "UpdateRouteCustomDataV5");
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}