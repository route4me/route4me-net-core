using System;

using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of getting location clustering for map visualization
        /// </summary>
        public void GetLocationClustering()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationClusteringRequest
            {
                Filters = new LocationFilters
                {
                    StrategicOptimizationId = "YOUR_OPTIMIZATION_ID"
                },
                Clustering = new ClusteringParameters
                {
                    Zoom = 10 // Zoom level 0-22 (higher = more detailed)
                },
                WithClusters = true
            };

            var clustering = route4Me.StrategicPlanner.GetLocationsClustering(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get clustering: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Location Clustering:");
            Console.WriteLine($"With Clusters: {clustering.WithClusters}");
            Console.WriteLine($"Total Clusters: {clustering.Clusters?.Length ?? 0}");
            Console.WriteLine($"Total Locations: {clustering.Locations?.Length ?? 0}");

            if (clustering.Clusters != null)
            {
                foreach (var clusterItem in clustering.Clusters)
                {
                    var cluster = clusterItem.Cluster;
                    Console.WriteLine($"\nCluster at ({cluster.Lat}, {cluster.Lng})");
                    Console.WriteLine($"  Locations in cluster: {cluster.LocationsCount}");
                    Console.WriteLine($"  Bounding box: ({cluster.BoundingBox?.Top}, {cluster.BoundingBox?.Left}) to ({cluster.BoundingBox?.Bottom}, {cluster.BoundingBox?.Right})");
                }
            }
        }

        /// <summary>
        /// Example of getting location heatmap data
        /// </summary>
        public void GetLocationHeatmap()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters
                {
                    StrategicOptimizationId = "YOUR_OPTIMIZATION_ID"
                },
                Zoom = 12
            };

            var heatmap = route4Me.StrategicPlanner.GetLocationsHeatmap(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get heatmap: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Location Heatmap:");
            Console.WriteLine($"Fields: {string.Join(", ", heatmap.Fields ?? Array.Empty<string>())}");
            Console.WriteLine($"Data points: {heatmap.Data?.Length ?? 0}");

            if (heatmap.Data != null && heatmap.Data.Length > 0)
            {
                Console.WriteLine("\nSample data points:");
                var samplesToShow = Math.Min(5, heatmap.Data.Length);
                
                for (int i = 0; i < samplesToShow; i++)
                {
                    var point = heatmap.Data[i];
                    Console.WriteLine($"  Point {i + 1}: Lat={point[0]}, Lng={point[1]}, Intensity={point[2]}");
                }
            }
        }

        /// <summary>
        /// Example of listing routes with grouping
        /// </summary>
        public void ListRoutesWithGrouping()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new ListRoutesRequest
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilters
                {
                    ScenarioId = "YOUR_SCENARIO_ID",
                    NumberWeek = new[] { 1, 2, 3, 4 }
                },
                GroupBy = "number_week",
                OrderBy = new[]
                {
                    new[] { "start_date", "asc" }
                }
            };

            var routes = route4Me.StrategicPlanner.GetRoutesCombined(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get routes: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"Total Routes: {routes.Data.TotalItemsCount}");
            Console.WriteLine($"Grouped by: {routes.Data.GroupBy}");

            if (routes.Data.Items != null)
            {
                foreach (var route in routes.Data.Items)
                {
                    Console.WriteLine($"\nRoute: {route.DraftRouteName}");
                    Console.WriteLine($"  Week: {route.NumberWeek}, Day: {route.WeekDay}");
                    Console.WriteLine($"  Date: {route.StartDate} at {route.StartTime}");
                    Console.WriteLine($"  Duration: {route.TotalDuration}s, Distance: {route.TotalDistance} {route.TotalDistanceUnit}");
                    Console.WriteLine($"  Destinations: {route.TotalDestinations}");
                    Console.WriteLine($"  Stops/Hour: {route.Sporh:F2}");
                }
            }
        }
    }
}
