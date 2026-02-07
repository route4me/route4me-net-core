using System;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of managing scenarios - list, generate with AI, and accept
        /// </summary>
        public void ManageScenarios()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // 1. List scenarios for an optimization
            Console.WriteLine("Listing scenarios...");
            var listRequest = new ListScenariosRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new ScenarioFilters
                {
                    StrategicOptimizationId = "YOUR_OPTIMIZATION_ID"
                },
                OrderBy = new[]
                {
                    new[] { "total_routes", "asc" }
                }
            };

            var scenarios = route4Me.StrategicPlanner.GetScenariosCombined(listRequest, out var resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine($"Found {scenarios.Data.TotalItemsCount} scenarios");
                
                if (scenarios.Data.Items != null)
                {
                    foreach (var scenario in scenarios.Data.Items)
                    {
                        Console.WriteLine($"\nScenario: {scenario.Description}");
                        Console.WriteLine($"  Total Routes: {scenario.TotalRoutes}");
                        Console.WriteLine($"  Total Distance: {scenario.TotalDistance} {scenario.TotalDistanceUnit}");
                        Console.WriteLine($"  Total Duration: {scenario.TotalDuration} seconds");
                        Console.WriteLine($"  Routed Locations: {scenario.RoutedLocationsPercent}%");
                    }
                }
            }

            // 2. Get scenario details
            Console.WriteLine("\nGetting scenario details...");
            var scenarioId = "YOUR_SCENARIO_ID";
            var scenario = route4Me.StrategicPlanner.GetScenario(scenarioId, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine($"Scenario: {scenario.Description}");
                Console.WriteLine($"Status: {scenario.StatusValue}");
                Console.WriteLine($"Days Count: {scenario.DaysCount}");
            }

            // 3. Accept scenario
            Console.WriteLine("\nAccepting scenario...");
            var acceptRequest = new AcceptScenarioRequest
            {
                RoutesType = "master" // or "regular"
            };

            var accepted = route4Me.StrategicPlanner.AcceptScenario(scenarioId, acceptRequest, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine("Scenario accepted successfully!");
                Console.WriteLine($"Job ID: {accepted.JobId}");
                Console.WriteLine("Routes are being created in the background...");
            }
        }

        /// <summary>
        /// Example of generating scenario configuration with AI
        /// </summary>
        public void GenerateScenarioWithAI()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new GenerateScenarioRequest
            {
                Prompt = "Create a 4-week delivery schedule with 5 routes per day, " +
                         "starting at 8 AM, maximum 8 hours per route, " +
                         "optimizing for distance with 30-minute breaks every 4 hours"
            };

            var generated = route4Me.StrategicPlanner.GenerateScenarioWithAI(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to generate scenario: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("AI-generated scenario configuration:");
            Console.WriteLine($"Scheduler Name: {generated.SchedulerName}");
            Console.WriteLine($"Cycles: {generated.Scheduler?.Cycles}");
            Console.WriteLine($"Cycle Length: {generated.Scheduler?.CycleLength} days");
            Console.WriteLine($"Start Date: {generated.Scheduler?.StartDate}");
            Console.WriteLine($"Route Max Duration: {generated.RouteMaxDuration} seconds");
            Console.WriteLine($"Vehicle Capacity: {generated.VehicleCapacity}");
            Console.WriteLine($"Timezone: {generated.Timezone}");

            // You can now use this configuration to add a scenario to your optimization
        }

        /// <summary>
        /// Example of adding scenarios to an optimization
        /// </summary>
        public void AddScenariosToOptimization()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var optimizationId = "YOUR_OPTIMIZATION_ID";

            var request = new AddScenariosRequest
            {
                ParamsJson = new[]
                {
                    new OptimizationParameters
                    {
                        SchedulerName = "4-Week Delivery Schedule",
                        Scheduler = new SchedulerConfiguration
                        {
                            Cycles = 4,
                            CycleLength = 7,
                            StartDate = "2026-02-10",
                            BlackoutDays = new[] { "sun" },
                            BlackoutDates = new[] { "12-25", "01-01", "07-04" }
                        },
                        Timezone = "America/New_York",
                        RouteTime = 28800, // 8:00 AM
                        Optimize = "Distance",
                        DistanceUnit = "mi",
                        TravelMode = "Driving",
                        VehicleCapacity = 100
                    }
                }
            };

            var result = route4Me.StrategicPlanner.AddScenarios(optimizationId, request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine("Failed to add scenarios: " + 
                    string.Join(", ", resultResponse.Messages?.Values ?? Array.Empty<string[]>()));
                return;
            }

            Console.WriteLine("Scenarios added successfully!");
            Console.WriteLine($"Optimization ID: {result.OptimizationId}");
            Console.WriteLine("Scenarios are being generated in the background...");
        }
    }
}
