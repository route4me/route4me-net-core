using System;

using Microsoft.Extensions.Logging;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrating how to use Microsoft.Extensions.Logging with Route4Me SDK.
        /// This shows how to inject a logger to capture HTTP requests, responses, and errors.
        /// </summary>
        public void LoggingExample()
        {
            Console.WriteLine("=== Route4Me Logging Example ===\n");

            // Step 1: Create a logger factory with console output
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()                           // Log to console
                    .SetMinimumLevel(LogLevel.Debug);      // Show Debug and above
            });

            // Step 2: Create a logger for Route4Me operations
            var logger = loggerFactory.CreateLogger<Route4MeManagerV5>();

            Console.WriteLine("Creating Route4MeManagerV5 with logger...\n");

            // Step 3: Create Route4MeManagerV5 with logger injection
            var route4Me = new Route4MeManagerV5(ActualApiKey, logger);

            Console.WriteLine("Making API call - watch for log output below:\n");
            Console.WriteLine("----------------------------------------");

            // Step 4: Make an API call - this will generate log entries
            var routeParameters = new RouteParametersQuery
            {
                Limit = 5,
                Offset = 0
            };

            try
            {
                var result = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);

                Console.WriteLine("----------------------------------------\n");

                if (resultResponse.Status)
                {
                    Console.WriteLine($"Success! Retrieved {result.Length} routes");
                }
                else
                {
                    Console.WriteLine("Request failed - check logs for details");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            Console.WriteLine("\n=== Logging Example Complete ===");
        }
    }
}