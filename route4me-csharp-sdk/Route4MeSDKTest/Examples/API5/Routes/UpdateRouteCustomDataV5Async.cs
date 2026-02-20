using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Updates (replaces) the custom data for a specific route asynchronously using the dedicated
        /// PUT /route-custom-data/{route_id} endpoint (API V5).
        /// All existing custom data keys not present in the request are removed.
        /// </summary>
        public async Task UpdateRouteCustomDataV5Async()
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
                { "project_code", "PRJ-2024-002" },
                { "status", "active" },
                { "assigned_to", "driver_team_alpha" }
            };

            // Act - update route custom data asynchronously
            var (result, resultResponse) = await route4Me.UpdateRouteCustomDataAsync(routeId, customData);

            Console.WriteLine("");

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("UpdateRouteCustomDataV5Async executed successfully");
                Console.WriteLine($"Updated custom data for route '{routeId}':");
                foreach (var kvp in result)
                {
                    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "UpdateRouteCustomDataV5Async");
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}