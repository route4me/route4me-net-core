using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary;

namespace Route4MeSDK.Examples
{
    /// <summary>
    ///     Example demonstrating batch route optimization with retry and resilience.
    ///     Creates 15 optimizations with retry logic enabled to handle transient failures.
    ///
    ///     This example showcases:
    ///     - Configuration of Polly retry policies
    ///     - Exponential backoff with jitter
    ///     - Circuit breaker pattern
    ///     - Batch processing with retry metrics tracking
    ///     - Proper cleanup of created resources
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        private static int _retryCount = 0;
        private static readonly object _lockObject = new object();

        public static async Task RunBatchOptimizations(string apiKey)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Batch Optimization with Polly Retry Demo");
            Console.WriteLine("===========================================\n");

            // Step 1: Configure retry settings
            Console.WriteLine("Step 1: Configuring retry and resilience settings...");
            Route4MeConfig.RetryCount = 3;
            Route4MeConfig.RetryInitialDelay = TimeSpan.FromMilliseconds(200);
            Route4MeConfig.EnableCircuitBreaker = true;
            Route4MeConfig.CircuitBreakerFailureThreshold = 5;
            Route4MeConfig.CircuitBreakerDuration = TimeSpan.FromSeconds(30);

            Console.WriteLine($"  - Retry Count: {Route4MeConfig.RetryCount}");
            Console.WriteLine($"  - Initial Delay: {Route4MeConfig.RetryInitialDelay.TotalMilliseconds}ms");
            Console.WriteLine($"  - Circuit Breaker: {(Route4MeConfig.EnableCircuitBreaker ? "Enabled" : "Disabled")}");
            Console.WriteLine($"  - CB Failure Threshold: {Route4MeConfig.CircuitBreakerFailureThreshold}");
            Console.WriteLine($"  - CB Duration: {Route4MeConfig.CircuitBreakerDuration.TotalSeconds}s\n");

            // Step 2: Set up retry callback for monitoring
            Console.WriteLine("Step 2: Setting up monitoring callbacks...");
            Route4MeConfig.OnRetry = (exception, timespan, retryAttempt, context) =>
            {
                lock (_lockObject)
                {
                    _retryCount++;
                }
                Console.WriteLine($"  [RETRY {retryAttempt}] Waiting {timespan.TotalMilliseconds:F0}ms " +
                                  $"before retry. Reason: {exception.Message}");
            };

            Route4MeConfig.OnCircuitBreakerOpen = (exception, duration) =>
            {
                Console.WriteLine($"  [CIRCUIT BREAKER OPEN] Blocking requests for {duration.TotalSeconds}s " +
                                  $"due to: {exception.Message}");
            };
            Console.WriteLine("  Callbacks configured.\n");

            // Step 3: Create Route4Me manager
            Console.WriteLine("Step 3: Creating Route4Me manager...");
            var route4Me = new Route4MeManager(apiKey);
            Console.WriteLine("  Manager created.\n");

            // Step 4: Prepare test addresses
            Console.WriteLine("Step 4: Preparing sample addresses...");
            var baseAddresses = CreateSampleAddresses();
            Console.WriteLine($"  {baseAddresses.Length} addresses prepared.\n");

            // Step 5: Execute batch optimizations
            var batchSize = 15;
            var results = new List<OptimizationResult>();
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine($"Step 5: Starting batch of {batchSize} optimizations with retry enabled...\n");

            // Process optimizations in parallel (simulate realistic load)
            var tasks = Enumerable.Range(0, batchSize).Select(async i =>
            {
                var optParams = CreateOptimizationParameters(baseAddresses, i);

                try
                {
                    var startTime = DateTime.Now;
                    DataObject dataObject = route4Me.RunOptimization(optParams, out string errorString);
                    var elapsed = DateTime.Now - startTime;

                    var optimizationResult = new OptimizationResult
                    {
                        Index = i,
                        Success = dataObject != null && string.IsNullOrEmpty(errorString),
                        OptimizationId = dataObject?.OptimizationProblemId,
                        Duration = elapsed,
                        ErrorMessage = errorString
                    };

                    var status = optimizationResult.Success ? "SUCCESS" : "FAILED";
                    var idDisplay = optimizationResult.OptimizationId ?? "N/A";
                    Console.WriteLine($"  [{i + 1}/{batchSize}] {status} - ID: {idDisplay} ({elapsed.TotalMilliseconds:F0}ms)");

                    return optimizationResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [{i + 1}/{batchSize}] EXCEPTION - {ex.Message}");
                    return new OptimizationResult
                    {
                        Index = i,
                        Success = false,
                        ErrorMessage = ex.Message
                    };
                }
            });

            results.AddRange(await Task.WhenAll(tasks));

            stopwatch.Stop();

            // Step 6: Display statistics
            Console.WriteLine("\n===========================================");
            Console.WriteLine("Batch Optimization Results");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Total Optimizations:  {batchSize}");
            Console.WriteLine($"Successful:           {results.Count(r => r.Success)}");
            Console.WriteLine($"Failed:               {results.Count(r => !r.Success)}");
            Console.WriteLine($"Total Retries:        {_retryCount}");
            Console.WriteLine($"Average Duration:     {results.Average(r => r.Duration.TotalMilliseconds):F0}ms");
            Console.WriteLine($"Total Elapsed Time:   {stopwatch.Elapsed.TotalSeconds:F1}s");

            // Calculate success rate
            var successRate = (results.Count(r => r.Success) * 100.0) / batchSize;
            Console.WriteLine($"Success Rate:         {successRate:F1}%");

            // Display failed optimizations if any
            var failedOpts = results.Where(r => !r.Success).ToList();
            if (failedOpts.Any())
            {
                Console.WriteLine("\nFailed Optimizations:");
                foreach (var failed in failedOpts)
                {
                    Console.WriteLine($"  - Index {failed.Index + 1}: {failed.ErrorMessage}");
                }
            }

            // Step 7: Cleanup
            Console.WriteLine("\n===========================================");
            Console.WriteLine("Cleanup");
            Console.WriteLine("===========================================");
            Console.WriteLine("Removing created optimizations...");
            var successfulIds = results
                .Where(r => r.Success && !string.IsNullOrEmpty(r.OptimizationId))
                .Select(r => r.OptimizationId)
                .ToArray();

            if (successfulIds.Length > 0)
            {
                bool cleanupResult = route4Me.RemoveOptimization(successfulIds, out string cleanupError);
                if (cleanupResult)
                {
                    Console.WriteLine($"  Successfully removed {successfulIds.Length} optimizations.");
                }
                else
                {
                    Console.WriteLine($"  Cleanup FAILED: {cleanupError}");
                }
            }
            else
            {
                Console.WriteLine("  No optimizations to clean up.");
            }

            Console.WriteLine("\n===========================================");
            Console.WriteLine("Demo Complete");
            Console.WriteLine("===========================================\n");
        }

        /// <summary>
        ///     Creates sample addresses for optimization.
        ///     These are realistic addresses in Milledgeville, GA.
        /// </summary>
        private static Address[] CreateSampleAddresses()
        {
            return new[]
            {
                new Address
                {
                    AddressString = "151 Arbor Way Milledgeville GA 31061",
                    IsDepot = true,
                    Latitude = 33.132675170898,
                    Longitude = -83.244743347168,
                    Time = 0
                },
                new Address
                {
                    AddressString = "230 Arbor Way Milledgeville GA 31061",
                    Latitude = 33.129695892334,
                    Longitude = -83.24577331543,
                    Time = 300
                },
                new Address
                {
                    AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                    Latitude = 33.143497,
                    Longitude = -83.224487,
                    Time = 300
                },
                new Address
                {
                    AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.141784667969,
                    Longitude = -83.237518310547,
                    Time = 300
                },
                new Address
                {
                    AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.141086578369,
                    Longitude = -83.238258361816,
                    Time = 300
                }
            };
        }

        /// <summary>
        ///     Creates optimization parameters with unique route name.
        /// </summary>
        private static OptimizationParameters CreateOptimizationParameters(Address[] addresses, int index)
        {
            return new OptimizationParameters
                {
                    Addresses = addresses,
                    Parameters = new RouteParameters
                    {
                        AlgorithmType = AlgorithmType.CVRP_TW_SD,
                        RouteName = $"Retry Demo Batch {index + 1} - {DateTime.Now:HHmmss}",
                        RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                        RouteTime = 60 * 60 * 7, // 7 AM
                        RT = true,
                        Optimize = Optimize.Distance.Description(),
                        DistanceUnit = DistanceUnit.MI.Description(),
                        DeviceType = DeviceType.Web.Description(),
                        TravelMode = TravelMode.Driving.Description()
                    }
                };
        }

        /// <summary>
        ///     Result container for optimization operations.
        /// </summary>
        private class OptimizationResult
        {
            public int Index { get; set; }
            public bool Success { get; set; }
            public string OptimizationId { get; set; }
            public TimeSpan Duration { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
