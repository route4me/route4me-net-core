using System;

using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of listing strategic optimizations with filters
        /// </summary>
        public void ListStrategicOptimizations()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new ListStrategicOptimizationsRequest
            {
                Page = 1,
                PerPage = 20,
                Filters = new StrategicOptimizationFilters
                {
                    SearchQuery = "",
                    LocationsCount = new[] { 1, 1000 }, // Between 1 and 1000 locations
                    ScenariosCount = new[] { 1, 50 }    // Between 1 and 50 scenarios
                },
                OrderBy = new[]
                {
                    new[] { "created_timestamp", "desc" } // Sort by creation date, newest first
                }
            };

            var result = route4Me.StrategicPlanner.GetOptimizationsCombined(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get optimizations: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"Total optimizations: {result.Data.TotalItemsCount}");
            Console.WriteLine($"Current page: {result.Data.CurrentPageIndex}");
            Console.WriteLine($"Items on this page: {result.Data.Items?.Length ?? 0}");

            if (result.Data.Items != null)
            {
                foreach (var optimization in result.Data.Items)
                {
                    Console.WriteLine($"\nOptimization: {optimization.Name}");
                    Console.WriteLine($"  ID: {optimization.OptimizationId}");
                    Console.WriteLine($"  Locations: {optimization.LocationsCount}");
                    Console.WriteLine($"  Scenarios: {optimization.ScenariosCount}");
                    Console.WriteLine($"  Source: {optimization.Source}");
                }
            }
        }

        /// <summary>
        /// Example of listing strategic optimizations with filters asynchronously
        /// </summary>
        public async void ListStrategicOptimizationsAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new ListStrategicOptimizationsRequest
            {
                Page = 1,
                PerPage = 20,
                Filters = new StrategicOptimizationFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await route4Me.StrategicPlanner.GetOptimizationsCombinedAsync(request);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get optimizations: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"Total optimizations: {result.Data.TotalItemsCount}");
            Console.WriteLine($"Items retrieved: {result.Data.Items?.Length ?? 0}");
        }
    }
}
