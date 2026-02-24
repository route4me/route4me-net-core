using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: get the combined destinations list (data + column config) for a route (API V5).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets the combined destinations list with pagination metadata and column configuration
        /// for a single route.
        /// Uses POST /route-destinations/list/combined.
        /// </summary>
        public void GetRouteDestinationsCombinedV5()
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
            const string tag = "GetRouteDestinationsCombinedV5";

            Console.WriteLine("");

            var request = new GetDestinationsRequest
            {
                Filters = new DestinationFilters { RouteId = routeId },
                Page = 1,
                PerPage = 20
            };

            var result = route4Me.GetDestinationsCombined(request, out ResultResponse resultResponse);

            if (resultResponse == null && result != null)
            {
                Console.WriteLine("{0} executed successfully.", tag);

                var data = result.Data;
                if (data != null)
                {
                    Console.WriteLine("  total_items_count={0}", data.TotalItemsCount);
                    Console.WriteLine("  current_page_index={0}", data.CurrentPageIndex);
                    Console.WriteLine("  items_returned={0}", data.Items?.Length ?? 0);

                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            Console.WriteLine("    destination_id={0}, type={1}",
                                item.RouteDestinationId, item.AddressStopType);
                        }
                    }
                }

                var config = result.Config;
                if (config != null)
                {
                    Console.WriteLine("  columns_configurator_key={0}", config.ColumnsConfiguratorKey ?? "(none)");
                    Console.WriteLine("  columns_count={0}", config.Columns?.Length ?? 0);
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
