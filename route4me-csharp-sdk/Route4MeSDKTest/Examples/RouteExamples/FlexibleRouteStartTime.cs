using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates how to create an optimization with flexible start time.
        /// Flexible start time allows the optimization engine to determine the optimal 
        /// departure time within a specified time window (earliest_start to latest_start).
        /// This is useful when the exact departure time is flexible and you want the 
        /// optimization to find the best start time to meet time windows efficiently.
        /// </summary>
        public void FlexibleRouteStartTime()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses - NYC locations
            Address[] addresses = new Address[]
            {
                #region Addresses

                new Address()
                {
                    AddressString = "Central Park, New York, NY",
                    Alias = "Depot - Central Park",
                    // This is the depot (starting point)
                    IsDepot = true,
                    Latitude = 40.76798,
                    Longitude = -73.96781,
                    // Service time at depot in seconds
                    Time = 300
                },

                new Address()
                {
                    AddressString = "Times Square, New York, NY",
                    Alias = "Stop 1 - Times Square",
                    Latitude = 40.75549,
                    Longitude = -73.99222,
                    // Service time in seconds
                    Time = 300,
                    // Time window: 8:30 AM to 9:00 AM
                    // The driver should arrive between these times
                    TimeWindowStart = 30600,  // 8:30 AM (8.5 * 60 * 60)
                    TimeWindowEnd = 32400     // 9:00 AM (9 * 60 * 60)
                },

                new Address()
                {
                    AddressString = "Empire State Building, New York, NY",
                    Alias = "Stop 2 - Empire State",
                    Latitude = 40.74881,
                    Longitude = -73.98513,
                    // Service time in seconds
                    Time = 300
                }

                #endregion
            };

            // Set route parameters with flexible start time
            var parameters = new RouteParameters()
            {
                // TSP algorithm for single driver
                AlgorithmType = AlgorithmType.TSP,

                // Route name that appears in the system
                RouteName = "Flexible Start Time Route",

                // Route date (tomorrow) in Unix timestamp
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),

                // Round trip - return to depot after completing all stops
                RT = true,

                // Enable flexible start time
                // When enabled, the optimization engine will find the optimal 
                // departure time within the specified window
                IsFlexibleStartTime = true,

                // Define the flexible start time window
                FlexibleStartTime = new FlexibleStartTime()
                {
                    // Earliest start: 8:00 AM (8 * 60 * 60 = 28800 seconds from midnight)
                    EarliestStart = 28800,

                    // Latest start: 10:00 AM (10 * 60 * 60 = 36000 seconds from midnight)
                    LatestStart = 36000
                },

                // Additional optimization settings
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the optimization
            DataObject dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            // Display flexible start time information
            if (dataObject != null && string.IsNullOrEmpty(errorString))
            {
                Console.WriteLine("");
                Console.WriteLine("Flexible Start Time Configuration:");
                Console.WriteLine("  Earliest Start: 8:00 AM (28800 seconds)");
                Console.WriteLine("  Latest Start: 10:00 AM (36000 seconds)");
                Console.WriteLine("");
                Console.WriteLine("The optimization engine determined the optimal departure time");
                Console.WriteLine("within the 8:00 AM - 10:00 AM window to best meet time windows.");
            }

            RemoveTestOptimizations();
        }
    }
}
