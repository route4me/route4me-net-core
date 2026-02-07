using System;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of creating a strategic optimization with locations
        /// </summary>
        public void CreateStrategicOptimization()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new CreateOrUpdateStrategicOptimizationRequest
            {
                Name = "Weekly Delivery Optimization " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                RootMemberId = 1,
                Source = "API",
                Locations = new[]
                {
                    new StrategicOptimizationLocation
                    {
                        Address = "123 Main St, New York, NY",
                        Alias = "Customer A",
                        Lat = 40.7128,
                        Lng = -74.0060,
                        Days = new[] { 1, 3, 5 }, // Monday, Wednesday, Friday
                        StartingCycle = 1,
                        DaysBetweenCycle = 7,
                        TimeWindowStart = 28800, // 8:00 AM
                        TimeWindowEnd = 61200,   // 5:00 PM
                        Time = 1800 // 30 minutes service time
                    },
                    new StrategicOptimizationLocation
                    {
                        Address = "456 Oak Ave, Brooklyn, NY",
                        Alias = "Customer B",
                        Lat = 40.6782,
                        Lng = -73.9442,
                        Days = new[] { 2, 4 }, // Tuesday, Thursday
                        StartingCycle = 1,
                        DaysBetweenCycle = 7,
                        TimeWindowStart = 32400, // 9:00 AM
                        TimeWindowEnd = 57600,   // 4:00 PM
                        Time = 1200 // 20 minutes service time
                    }
                }
            };

            var optimization = route4Me.StrategicPlanner.CreateOrUpdateOptimization(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to create optimization: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Strategic Optimization created successfully!");
            Console.WriteLine($"Optimization ID: {optimization.StrategicOptimizationId}");
            Console.WriteLine($"Locations created: {optimization.Locations?.Length ?? 0}");
        }

        /// <summary>
        /// Example of creating a strategic optimization with locations asynchronously
        /// </summary>
        public async void CreateStrategicOptimizationAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new CreateOrUpdateStrategicOptimizationRequest
            {
                Name = "Weekly Delivery Optimization " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                RootMemberId = 1,
                Source = "API",
                Locations = new[]
                {
                    new StrategicOptimizationLocation
                    {
                        Address = "123 Main St, New York, NY",
                        Alias = "Customer A",
                        Lat = 40.7128,
                        Lng = -74.0060,
                        Days = new[] { 1, 3, 5 },
                        StartingCycle = 1,
                        DaysBetweenCycle = 7,
                        TimeWindowStart = 28800,
                        TimeWindowEnd = 61200,
                        Time = 1800
                    }
                }
            };

            var (optimization, resultResponse) = await route4Me.StrategicPlanner.CreateOrUpdateOptimizationAsync(request);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to create optimization: " +
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Strategic Optimization created successfully!");
            Console.WriteLine($"Optimization ID: {optimization.StrategicOptimizationId}");
        }
    }
}
