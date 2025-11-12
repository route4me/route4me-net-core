using System;

using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get location heatmap data for visualization.
        /// This example demonstrates how to retrieve heatmap data showing location density.
        /// Heatmaps are useful for visualizing geographic distribution of locations.
        /// </summary>
        public void GetLocationsHeatmap()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters(),
                Zoom = 8 // Zoom level 0-22 (higher = more granular data)
            };

            var result = route4Me.GetLocationsHeatmap(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine($"Error: {string.Join(", ", resultResponse.Messages)}");
                return;
            }

            if (result?.Data != null && result.Data.Length > 0)
            {
                Console.WriteLine($"Heatmap data at zoom level {request.Zoom}:");
                Console.WriteLine($"  Total data points: {result.Data.Length}");
                Console.WriteLine();

                // Display first few heatmap points as examples
                int displayCount = Math.Min(10, result.Data.Length);
                Console.WriteLine($"  First {displayCount} heatmap points:");

                for (int i = 0; i < displayCount; i++)
                {
                    var point = result.Data[i];
                    Console.WriteLine($"    Point {i + 1}: [{point[0]}, {point[1]}, {point[2]}]");
                    // Typically: [latitude, longitude, intensity/weight]
                }

                if (result.Data.Length > displayCount)
                {
                    Console.WriteLine($"    ... and {result.Data.Length - displayCount} more points");
                }

                Console.WriteLine();
                Console.WriteLine("  Note: Heatmap data is an array of [lat, lng, weight] points");
                Console.WriteLine("  Use this data with mapping libraries like Leaflet.heat or Google Maps Heatmap Layer");
            }
            else
            {
                Console.WriteLine("No heatmap data available.");
            }
        }
    }
}