using System;

using Microsoft.Extensions.Logging;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrating how to use Microsoft.Extensions.Logging with Route4Me SDK.
        /// This shows how to inject a logger to capture HTTP requests, responses, and errors.
        ///
        /// Note: Logger injection is supported by specialized V5 managers (RouteManagerV5, VehicleManagerV5, etc.)
        /// that inherit from Route4MeManagerBase. The facade Route4MeManagerV5 does not support logger injection.
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
            var logger = loggerFactory.CreateLogger<RouteManagerV5>();

            Console.WriteLine("Creating RouteManagerV5 with logger...\n");

            // Step 3: Create a specialized manager with logger injection
            // The logger is passed to the constructor and will be used for all API calls
            // This is thread-safe - each manager instance has its own logger reference
            var routeManager = new RouteManagerV5(ActualApiKey, logger);

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
                var result = routeManager.GetRoutes(routeParameters, out ResultResponse resultResponse);

                Console.WriteLine("----------------------------------------\n");

                if (resultResponse.Status)
                {
                    Console.WriteLine($"Success! Retrieved {result?.Length ?? 0} routes");
                }
                else
                {
                    Console.WriteLine("Request failed - check logs for details");
                    if (resultResponse.Messages != null)
                    {
                        foreach (var msg in resultResponse.Messages)
                        {
                            Console.WriteLine($"  {msg.Key}: {string.Join(", ", msg.Value)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            Console.WriteLine("\n=== Logging Example Complete ===");
            Console.WriteLine("\nTip: You can use different specialized managers with their own loggers:");
            Console.WriteLine("  - VehicleManagerV5(apiKey, vehicleLogger)");
            Console.WriteLine("  - OrderManagerV5(apiKey, orderLogger)");
            Console.WriteLine("  - AddressBookContactsManagerV5(apiKey, contactLogger)");
            Console.WriteLine("  Each manager instance is thread-safe with its own logger context.");
        }
    }
}