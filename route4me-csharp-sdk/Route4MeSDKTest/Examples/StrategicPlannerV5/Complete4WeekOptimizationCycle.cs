using System;
using System.Linq;
using System.Threading;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Complete example demonstrating a 4-week strategic optimization cycle:
        /// 1. Upload customer locations CSV file
        /// 2. Preview and validate the data
        /// 3. Create optimization with 4-week cycle parameters
        /// 4. Generate multiple scenarios with different parameters
        /// 5. Compare scenarios and select the best one
        /// 6. Accept the scenario and create master routes
        /// 7. Export results for analysis
        /// </summary>
        public void Complete4WeekOptimizationCycle()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            Console.WriteLine("=== 4-Week Strategic Optimization Cycle Example ===\n");

            // ====================================================================
            // Step 1: Upload CSV file with customer locations
            // ====================================================================
            Console.WriteLine("Step 1: Uploading customer locations CSV file...");
            var filePath = "path/to/customers.csv"; // Replace with actual file path

            var uploadResult = route4Me.StrategicPlanner.UploadFile(filePath, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("ERROR: Failed to upload file: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"✓ File uploaded successfully!");
            Console.WriteLine($"  Upload ID: {uploadResult.UploadId}");
            Console.WriteLine($"  Available encodings: {uploadResult.Encodings?.Length ?? 0}");

            // ====================================================================
            // Step 2: Preview and validate the uploaded data
            // ====================================================================
            Console.WriteLine("\nStep 2: Previewing and validating uploaded data...");
            var previewRequest = new UploadPreviewRequest
            {
                StrUploadID = uploadResult.UploadId,
                Limit = 100,
                Sheet = 0,
                IntFromEncodingIndex = 0,
                ArrValidCSVColumns = new[] { "address", "alias", "lat", "lng", "time" }
            };

            var preview = route4Me.StrategicPlanner.GetUploadPreview(previewRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("ERROR: Failed to get preview: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"✓ Preview retrieved!");
            Console.WriteLine($"  Total addresses found: {preview.AddressCount}");
            Console.WriteLine($"  CSV headers: {string.Join(", ", preview.CsvHeader ?? Array.Empty<string>())}");

            if (preview.Warnings != null)
            {
                Console.WriteLine("  ⚠ Warnings found - please review data quality");
            }

            // ====================================================================
            // Step 3: Create strategic optimization with 4-week cycle parameters
            // ====================================================================
            Console.WriteLine("\nStep 3: Creating 4-week strategic optimization...");
            
            var startDate = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"); // Start next week

            var createRequest = new CreateOptimizationRequest
            {
                StrUploadID = uploadResult.UploadId,
                FileName = "customers.csv",
                Name = "4-Week Delivery Optimization " + DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                ParamsJson = new[]
                {
                    // Scenario 1: Optimize for Distance
                    new OptimizationParameters
                    {
                        SchedulerName = "4-Week Distance Optimized",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = startDate,
                            BlackoutDays = new[] { "sun" }, // No deliveries on Sunday
                            BlackoutDates = new[] { "12-25", "01-01" } // Holidays
                        },
                        Timezone = "America/New_York",
                        RouteTime = 28800, // 8:00 AM start time
                        Optimize = "Distance",
                        DistanceUnit = "mi",
                        TravelMode = "Driving",
                        VehicleCapacity = 100,
                        RoundTrip = true
                    },
                    // Scenario 2: Optimize for Time
                    new OptimizationParameters
                    {
                        SchedulerName = "4-Week Time Optimized",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = startDate,
                            BlackoutDays = new[] { "sun" }
                        },
                        Timezone = "America/New_York",
                        RouteTime = 28800,
                        Optimize = "Time",
                        DistanceUnit = "mi",
                        TravelMode = "Driving",
                        VehicleCapacity = 100,
                        RoundTrip = true
                    },
                    // Scenario 3: Optimize with Traffic
                    new OptimizationParameters
                    {
                        SchedulerName = "4-Week Traffic Optimized",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = startDate,
                            BlackoutDays = new[] { "sun" }
                        },
                        Timezone = "America/New_York",
                        RouteTime = 28800,
                        Optimize = "timeWithTraffic",
                        DistanceUnit = "mi",
                        TravelMode = "Driving",
                        VehicleCapacity = 100,
                        RoundTrip = true
                    }
                },
                DepotAddresses = new[]
                {
                    new DepotAddress
                    {
                        Address = "Main Distribution Center, 789 Warehouse Blvd, New York, NY 10001",
                        Lat = 40.7589,
                        Lng = -73.9851
                    }
                },
                ArrValidCSVColumns = new[] { "address", "alias", "lat", "lng", "time" },
                IsUi = false
            };

            var optimization = route4Me.StrategicPlanner.CreateOptimizationFromUpload(createRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("ERROR: Failed to create optimization: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"✓ Optimization created successfully!");
            Console.WriteLine($"  Optimization ID: {optimization.OptimizationId}");
            Console.WriteLine($"  Message: {optimization.Message}");
            Console.WriteLine($"  3 scenarios are being generated in the background...");

            var optimizationId = optimization.OptimizationId;

            // ====================================================================
            // Step 4: Wait for scenarios to be generated and compare them
            // ====================================================================
            Console.WriteLine("\nStep 4: Waiting for scenarios to be generated...");
            
            ScenarioCombinedCollection scenarios = null;
            var maxWaitAttempts = 30;
            var waitAttempt = 0;

            while (waitAttempt < maxWaitAttempts)
            {
                Thread.Sleep(5000); // Wait 5 seconds between checks

                var scenariosRequest = new ListScenariosRequest
                {
                    Filters = new ScenarioFilters
                    {
                        StrategicOptimizationId = optimizationId
                    },
                    Page = 1,
                    PerPage = 10
                };

                scenarios = route4Me.StrategicPlanner.GetScenariosCombined(scenariosRequest, out resultResponse);

                if (resultResponse == null && scenarios?.Data?.Items != null && scenarios.Data.Items.Length >= 3)
                {
                    // Check if all scenarios are optimized (not failed or processing)
                    var allReady = true;
                    foreach (var s in scenarios.Data.Items)
                    {
                        if (s.Status == "processing" || s.Status == "pending")
                        {
                            allReady = false;
                            break;
                        }
                    }

                    if (allReady)
                    {
                        Console.WriteLine($"✓ All {scenarios.Data.Items.Length} scenarios generated!");
                        break;
                    }
                }

                waitAttempt++;
                Console.WriteLine($"  Waiting... (attempt {waitAttempt}/{maxWaitAttempts})");
            }

            if (scenarios == null || scenarios.Data?.Items == null || scenarios.Data.Items.Length == 0)
            {
                Console.WriteLine("ERROR: No scenarios generated");
                return;
            }

            // ====================================================================
            // Step 5: Compare scenarios and select the best one
            // ====================================================================
            Console.WriteLine("\nStep 5: Comparing scenarios...\n");
            Console.WriteLine("Scenario Comparison:");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"{"Scenario",-30} {"Routes",-10} {"Distance",-15} {"Duration",-15} {"Routed %",-12}");
            Console.WriteLine(new string('-', 100));

            ScenarioCombinedResource bestScenario = null;
            double bestScore = double.MaxValue;

            foreach (var scenario in scenarios.Data.Items)
            {
                Console.WriteLine(
                    $"{scenario.Description,-30} " +
                    $"{scenario.TotalRoutes,-10} " +
                    $"{scenario.TotalDistance,10:F2} {scenario.TotalDistanceUnit,-4} " +
                    $"{scenario.TotalDuration,10} sec    " +
                    $"{scenario.RoutedLocationsPercent,8:F2}%");

                // Simple scoring: prefer fewer routes and shorter distance
                var score = (scenario.TotalRoutes ?? 0) * 100 + (scenario.TotalDistance ?? 0);
                
                if (scenario.RoutedLocationsPercent > 95 && score < bestScore)
                {
                    bestScore = score;
                    bestScenario = scenario;
                }
            }

            if (bestScenario == null)
            {
                Console.WriteLine("\nERROR: No suitable scenario found");
                return;
            }

            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"\n✓ Best scenario selected: {bestScenario.Description}");
            Console.WriteLine($"  Scenario ID: {bestScenario.ScenarioId}");
            Console.WriteLine($"  Total Routes: {bestScenario.TotalRoutes}");
            Console.WriteLine($"  Total Distance: {bestScenario.TotalDistance} {bestScenario.TotalDistanceUnit}");
            Console.WriteLine($"  Routed Locations: {bestScenario.RoutedLocationsPercent:F2}%");

            // ====================================================================
            // Step 6: Accept the best scenario and create master routes
            // ====================================================================
            Console.WriteLine("\nStep 6: Accepting scenario and creating master routes...");

            var acceptRequest = new AcceptScenarioRequest
            {
                RoutesType = "master" // Create as master routes for recurring use
            };

            var acceptResult = route4Me.StrategicPlanner.AcceptScenario(bestScenario.ScenarioId, acceptRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("ERROR: Failed to accept scenario: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"✓ Scenario accepted!");
            Console.WriteLine($"  Job ID: {acceptResult.JobId}");
            Console.WriteLine($"  Master routes are being created in the background...");

            // ====================================================================
            // Step 7: Export results for analysis
            // ====================================================================
            Console.WriteLine("\nStep 7: Exporting optimization results...");

            var exportRequest = new NestedOptimizationsExportRequest
            {
                Optimizations = new ExportOptimizationsRequest
                {
                    Ids = new[] { optimizationId },
                    Format = "csv",
                    Columns = new[] { "name", "locations_count", "scenarios_count", "average_routes", "average_distance" }
                },
                Scenarios = new ExportScenariosRequest
                {
                    Filters = new ScenarioFilters
                    {
                        StrategicOptimizationId = optimizationId
                    },
                    Format = "csv",
                    Columns = new[] { "description", "total_routes", "total_distance", "routed_locations_percent" }
                },
                Routes = new ExportRoutesRequest
                {
                    Filters = new RouteFilters
                    {
                        ScenarioId = bestScenario.ScenarioId
                    },
                    Format = "csv"
                },
                Visits = new ExportVisitsRequest
                {
                    Filters = new VisitFilters
                    {
                        ScenarioId = bestScenario.ScenarioId
                    },
                    Format = "csv"
                }
            };

            var exportJob = route4Me.StrategicPlanner.ExportOptimizationsNested(exportRequest, out resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("ERROR: Failed to submit export: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine($"✓ Export job submitted!");
            Console.WriteLine($"  Job ID: {exportJob.JobId}");

            // ====================================================================
            // Step 8: Monitor export progress and download results
            // ====================================================================
            Console.WriteLine("\nStep 8: Monitoring export progress...");

            var maxExportAttempts = 30;
            var exportAttempt = 0;

            while (exportAttempt < maxExportAttempts)
            {
                Thread.Sleep(3000); // Wait 3 seconds between checks

                var status = route4Me.StrategicPlanner.GetExportStatus(exportJob.JobId.Value, out resultResponse);

                if (status != null)
                {
                    Console.WriteLine($"  Export status: {status.Status}");

                    if (status.Status == "finished")
                    {
                        Console.WriteLine("✓ Export completed successfully!");

                        // Download the file
                        var fileContent = route4Me.StrategicPlanner.DownloadExport(exportJob.JobId.Value, out resultResponse);

                        if (resultResponse == null && fileContent != null)
                        {
                            var fileName = $"4week_optimization_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                            System.IO.File.WriteAllBytes(fileName, fileContent);
                            Console.WriteLine($"✓ Export file downloaded: {fileName} ({fileContent.Length:N0} bytes)");
                        }

                        break;
                    }
                    else if (status.Status == "failed")
                    {
                        Console.WriteLine("ERROR: Export failed!");
                        break;
                    }
                }

                exportAttempt++;
            }

            // ====================================================================
            // Summary
            // ====================================================================
            Console.WriteLine("\n=== 4-Week Optimization Cycle Complete ===");
            Console.WriteLine($"Optimization ID: {optimizationId}");
            Console.WriteLine($"Best Scenario: {bestScenario.Description}");
            Console.WriteLine($"Total Routes Created: {bestScenario.TotalRoutes}");
            Console.WriteLine($"Total Locations Routed: {bestScenario.RoutedLocations}/{bestScenario.TotalLocations}");
            Console.WriteLine($"Success Rate: {bestScenario.RoutedLocationsPercent:F2}%");
            Console.WriteLine($"\nNext Steps:");
            Console.WriteLine($"  - Monitor master route creation via Job ID: {acceptResult.JobId}");
            Console.WriteLine($"  - Review exported data for detailed analysis");
            Console.WriteLine($"  - Adjust scenario parameters if needed and re-run");
        }

        /// <summary>
        /// Example of comparing multiple scenarios with detailed statistics
        /// </summary>
        public void CompareOptimizationScenarios()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var optimizationId = "YOUR_OPTIMIZATION_ID"; // Replace with actual ID

            Console.WriteLine("=== Scenario Comparison Report ===\n");

            // Get all scenarios for the optimization
            var request = new ListScenariosRequest
            {
                Filters = new ScenarioFilters
                {
                    StrategicOptimizationId = optimizationId
                },
                Page = 1,
                PerPage = 50
            };

            var scenarios = route4Me.StrategicPlanner.GetScenariosCombined(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get scenarios");
                return;
            }

            if (scenarios?.Data?.Items == null || scenarios.Data.Items.Length == 0)
            {
                Console.WriteLine("No scenarios found");
                return;
            }

            Console.WriteLine($"Found {scenarios.Data.Items.Length} scenarios\n");

            // Detailed comparison table
            Console.WriteLine("Detailed Comparison:");
            Console.WriteLine(new string('=', 120));
            Console.WriteLine(
                $"{"Scenario",-35} " +
                $"{"Routes",-8} " +
                $"{"Distance",-12} " +
                $"{"Duration",-10} " +
                $"{"Routed%",-10} " +
                $"{"Avg Route",-12} " +
                $"{"SPORH",-8}");
            Console.WriteLine(new string('=', 120));

            foreach (var scenario in scenarios.Data.Items)
            {
                Console.WriteLine(
                    $"{scenario.Description,-35} " +
                    $"{scenario.TotalRoutes,-8} " +
                    $"{scenario.TotalDistance,8:F2} mi  " +
                    $"{TimeSpan.FromSeconds(scenario.TotalDuration ?? 0).TotalHours,6:F1} hrs  " +
                    $"{scenario.RoutedLocationsPercent,7:F2}%  " +
                    $"{scenario.AvgRouteDistance,8:F2} mi  " +
                    $"{(scenario.TotalDuration > 0 ? (scenario.TotalVisits ?? 0) * 3600.0 / scenario.TotalDuration.Value : 0),6:F2}");
            }

            Console.WriteLine(new string('=', 120));

            // Find best scenario for each metric
            var shortestDistance = scenarios.Data.Items.OrderBy(s => s.TotalDistance ?? double.MaxValue).FirstOrDefault();
            var shortestTime = scenarios.Data.Items.OrderBy(s => s.TotalDuration ?? int.MaxValue).FirstOrDefault();
            var fewestRoutes = scenarios.Data.Items.OrderBy(s => s.TotalRoutes ?? int.MaxValue).FirstOrDefault();
            var bestRouted = scenarios.Data.Items.OrderByDescending(s => s.RoutedLocationsPercent ?? 0).FirstOrDefault();

            Console.WriteLine("\nBest Performers:");
            Console.WriteLine($"  Shortest Distance: {shortestDistance?.Description} ({shortestDistance?.TotalDistance:F2} mi)");
            Console.WriteLine($"  Shortest Time: {shortestTime?.Description} ({TimeSpan.FromSeconds(shortestTime?.TotalDuration ?? 0).TotalHours:F1} hrs)");
            Console.WriteLine($"  Fewest Routes: {fewestRoutes?.Description} ({fewestRoutes?.TotalRoutes} routes)");
            Console.WriteLine($"  Best Coverage: {bestRouted?.Description} ({bestRouted?.RoutedLocationsPercent:F2}% routed)");

            Console.WriteLine("\nRecommendation:");
            Console.WriteLine($"  For cost efficiency (fewer routes): {fewestRoutes?.Description}");
            Console.WriteLine($"  For driver satisfaction (shorter days): {shortestTime?.Description}");
            Console.WriteLine($"  For fuel savings (shorter distance): {shortestDistance?.Description}");
        }

        /// <summary>
        /// Example of viewing route details for a scenario
        /// </summary>
        public void ViewScenarioRouteDetails()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var scenarioId = "YOUR_SCENARIO_ID"; // Replace with actual ID

            // Get routes for the scenario grouped by week
            var request = new ListRoutesRequest
            {
                Filters = new RouteFilters
                {
                    ScenarioId = scenarioId
                },
                GroupBy = "number_week",
                Page = 1,
                PerPage = 100,
                OrderBy = new[]
                {
                    new[] { "number_week", "asc" },
                    new[] { "week_day", "asc" }
                }
            };

            var routes = route4Me.StrategicPlanner.GetRoutesCombined(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to get routes");
                return;
            }

            Console.WriteLine("=== 4-Week Route Schedule ===\n");

            if (routes?.Data?.Items != null)
            {
                var currentWeek = 0;
                
                foreach (var route in routes.Data.Items)
                {
                    if (route.NumberWeek != currentWeek)
                    {
                        currentWeek = route.NumberWeek ?? 0;
                        Console.WriteLine($"\n--- Week {currentWeek} ---");
                    }

                    Console.WriteLine(
                        $"  {route.WeekDay,-9} {route.StartDate,-12} " +
                        $"| Stops: {route.TotalDestinations,3} " +
                        $"| Distance: {route.TotalDistance,6:F1} {route.TotalDistanceUnit} " +
                        $"| Duration: {TimeSpan.FromSeconds(route.TotalDuration ?? 0).TotalHours:F1}h " +
                        $"| SPORH: {route.Sporh:F2}");
                }

                Console.WriteLine($"\nTotal routes: {routes.Data.Items.Length}");
            }
        }
    }
}
