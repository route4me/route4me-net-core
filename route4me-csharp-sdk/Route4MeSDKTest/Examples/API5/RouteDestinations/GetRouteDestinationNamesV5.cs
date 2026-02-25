using System;
using System.Collections.Generic;
using System.Threading;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: read a route and list its destination names (API V5).
    /// Demonstrates combining GET /routes/{id} with POST /route-destinations/list.
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Reads a route by ID, then fetches all of its destinations and prints
        /// each stop's <c>destination_name</c> field.
        /// </summary>
        public void GetRouteDestinationNamesV5()
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

                // V5 destination list uses eventual consistency.
                // Poll until the index count stabilises before querying.
                var pollReq = new GetDestinationsRequest
                {
                    Filters = new DestinationFilters { RouteId = SD10Stops_route_id_V5 },
                    Fields = new[] { "route_destination_id" },
                    Page = 1,
                    PerPage = 100
                };
                DateTime deadline = DateTime.UtcNow.AddSeconds(30);
                int prevCount = -1;
                while (DateTime.UtcNow < deadline)
                {
                    Thread.Sleep(2000);
                    int cur = route4Me.GetDestinationsList(pollReq, out _)?.Items?.Length ?? 0;
                    if (cur > 0 && cur == prevCount)
                        break;
                    prevCount = cur;
                }
            }

            const string tag = "GetRouteDestinationNamesV5";

            Console.WriteLine("");

            // Step 1: read the route
            var route = route4Me.GetRoute(SD10Stops_route_id_V5, out ResultResponse routeResponse);

            if (routeResponse != null || route == null)
            {
                PrintFailResponse(routeResponse, tag + " (GetRoute)");
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }

            Console.WriteLine("{0}: route_id={1}", tag, route.Data.RouteID);

            // Step 2: fetch destinations for this route
            var request = new GetDestinationsRequest
            {
                Filters = new DestinationFilters { RouteId = route.Data.RouteID },
                Fields = new[]
                {
                    "route_destination_id", "destination_name", "address_stop_type", "stop_status_id",
                    "custom_fields", "is_depot", "is_visited", "is_departed",
                    "notes", "sequence_no"
                },
                Page = 1,
                PerPage = 100
            };

            var result = route4Me.GetDestinationsList(request, out ResultResponse listResponse);

            if (listResponse != null || result == null)
            {
                PrintFailResponse(listResponse, tag + " (GetDestinationsList)");
                if (isInnerExample) RemoveTestOptimizations();
                return;
            }

            Console.WriteLine("{0}: destinations returned: {1}", tag, result.Items?.Length ?? 0);

            if (result.Items != null)
            {
                foreach (var item in result.Items)
                {
                    Console.WriteLine("  [{0}] {1}",
                        item.RouteDestinationId,
                        item.DestinationName ?? "(no name)");
                }
            }

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}