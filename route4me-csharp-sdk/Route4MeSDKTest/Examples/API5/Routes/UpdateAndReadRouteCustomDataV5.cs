using System;
using System.Collections.Generic;
using System.Linq;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Route-level custom data example (API V5): read via GET /route-custom-data/{route_id},
    /// update via PUT /route-custom-data/{route_id}, read again, then show custom data
    /// on the route payload (GetRoute and GetRoutesList).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Reads route custom data, updates it, reads again, and shows route-level custom data
        /// returned by GetRoute and GetRoutesList (API V5).
        /// </summary>
        public void UpdateAndReadRouteCustomDataV5()
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
            const string tag = "UpdateAndReadRouteCustomDataV5";

            Console.WriteLine("");

            // Step 1: Read route custom data (GET /route-custom-data/{route_id})
            Console.WriteLine("{0} Step 1: Reading route custom data...", tag);
            var read1 = route4Me.GetRouteCustomDataDedicated(routeId, out ResultResponse resp1);
            if (resp1 != null)
            {
                Console.WriteLine("{0} Step 1 failed.", tag);
                PrintFailResponse(resp1, tag);
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }
            Console.WriteLine("{0} Step 1 OK. Current custom data:", tag);
            PrintRouteCustomDataDictionary(read1);

            // Step 2: Update route custom data (PUT /route-custom-data/{route_id})
            var customData = new Dictionary<string, string>
            {
                { "priority", "high" },
                { "region", "northeast" },
                { "updatedat", DateTime.UtcNow.ToString("o") }
            };
            Console.WriteLine("{0} Step 2: Updating route custom data...", tag);
            var updateResult = route4Me.UpdateRouteCustomData(routeId, customData, out ResultResponse resp2);
            if (resp2 != null)
            {
                Console.WriteLine("{0} Step 2 failed.", tag);
                PrintFailResponse(resp2, tag);
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }
            Console.WriteLine("{0} Step 2 OK. API returned:", tag);
            PrintRouteCustomDataDictionary(updateResult);

            // Step 3: Read route custom data again
            Console.WriteLine("{0} Step 3: Reading route custom data again...", tag);
            var read2 = route4Me.GetRouteCustomDataDedicated(routeId, out ResultResponse resp3);
            if (resp3 != null)
            {
                Console.WriteLine("{0} Step 3 failed.", tag);
                PrintFailResponse(resp3, tag);
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }
            Console.WriteLine("{0} Step 3 OK. Custom data after update:", tag);
            PrintRouteCustomDataDictionary(read2);

            // Step 4: GetRoute by id — route payload includes route-level custom_data
            Console.WriteLine("{0} Step 4: GetRoute by id (route-level custom_data in payload)...", tag);
            var getRouteResponse = route4Me.GetRoute(routeId, out ResultResponse respGetRoute);
            var routeById = getRouteResponse?.Data;
            if (respGetRoute != null)
            {
                PrintFailResponse(respGetRoute, tag);
            }
            else if (routeById != null)
            {
                Console.WriteLine("{0} RouteId: {1}, RouteCustomData count: {2}", tag,
                    routeById.RouteID ?? "(null)", routeById.RouteCustomData?.Count ?? 0);
                PrintRouteCustomDataDictionary(routeById.RouteCustomData);
            }

            // Step 5: GetRoutesList by route_id — route-level custom_data in list
            Console.WriteLine("{0} Step 5: GetRoutesList by route_id (route-level custom_data in list)...", tag);
            var listParams = new RouteParametersQuery { RouteId = routeId };
            var routes = route4Me.GetRoutesList(listParams, out ResultResponse respList);
            if (respList != null)
            {
                PrintFailResponse(respList, tag);
            }
            else if (routes != null && routes.Length > 0)
            {
                var route = routes.FirstOrDefault();
                Console.WriteLine("{0} route_id: {1}, RouteCustomData count: {2}", tag,
                    route?.RouteID ?? "(null)", route?.RouteCustomData?.Count ?? 0);
                if (route?.RouteCustomData != null)
                    PrintRouteCustomDataDictionary(route.RouteCustomData);
            }

            Console.WriteLine("{0} Done.", tag);
            if (isInnerExample) RemoveTestOptimizations();
        }

        private static void PrintRouteCustomDataDictionary(Dictionary<string, string> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("  (none)");
                return;
            }
            foreach (var kvp in data)
                Console.WriteLine("  {0}: {1}", kvp.Key, kvp.Value);
        }
    }
}