using System;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get clustered locations based on zoom level.
        /// This example demonstrates how to retrieve location clusters for map visualization.
        /// Clusters group nearby locations together based on the zoom level.
        /// </summary>
        public void GetLocationsClustering()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationClusteringRequest
            {
                Filters = new LocationFilters(),
                Clustering = new ClusteringParameters
                {
                    Zoom = 10 // Zoom level 0-22 (higher = more detail)
                },
                WithClusters = true
            };

            var result = route4Me.GetLocationsClustering(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine($"Error: {string.Join(", ", resultResponse.Messages)}");
                return;
            }

            if (result != null)
            {
                Console.WriteLine($"Clustering results at zoom level {request.Clustering.Zoom}:");

                if (result.Clusters != null)
                {
                    Console.WriteLine($"  Clusters: {result.Clusters.Length}");
                    foreach (var cluster in result.Clusters)
                    {
                        if (cluster?.Cluster != null)
                        {
                            Console.WriteLine($"    Cluster at ({cluster.Cluster.Lat}, {cluster.Cluster.Lng})");
                            Console.WriteLine($"    - Locations count: {cluster.Cluster.LocationsCount}");
                            if (cluster.Cluster.BoundingBox != null)
                            {
                                Console.WriteLine($"    - Bounding box: Top={cluster.Cluster.BoundingBox.Top}, " +
                                                 $"Bottom={cluster.Cluster.BoundingBox.Bottom}, " +
                                                 $"Left={cluster.Cluster.BoundingBox.Left}, " +
                                                 $"Right={cluster.Cluster.BoundingBox.Right}");
                            }
                        }
                    }
                }

                if (result.Locations != null)
                {
                    Console.WriteLine($"  Individual locations: {result.Locations.Length}");
                    foreach (var location in result.Locations)
                    {
                        Console.WriteLine($"    Location: {location.AddressAlias} at ({location.Lat}, {location.Lng})");
                    }
                }
            }
            else
            {
                Console.WriteLine("No clustering results found.");
            }
        }
    }
}
