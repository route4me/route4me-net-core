using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Find the best position for a new stop that has a time window,
        ///     and show which stops would be served late after the insertion.
        /// </summary>
        public void DynamicInsertStopWithTimeWindow()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var dynamicInsertParams = new DynamicInsertRequest()
            {
                RouteIds = new[] { SD10Stops_route_id },
                Latitude = 33.1296514,
                Longitude = -83.2485687,
                InsertMode = DynamicInsertMode.OptimalAnywhere.Description(),
                RecommendBy = DynamicInsertRecomendBy.Duration.Description(),
                // The new stop must be served between 09:00 and 11:00 or between 14:00 and 16:00.
                // Values are seconds from midnight.
                TimeWindowStart = 9 * 3600,
                TimeWindowEnd = 11 * 3600,
                TimeWindowStart2 = 14 * 3600,
                TimeWindowEnd2 = 16 * 3600
            };

            var result = route4Me.DynamicInsertRouteAddresses(dynamicInsertParams, out ResultResponse resultResponse);

            if (result == null)
            {
                PrintFailResponse(resultResponse, "DynamicInsertStopWithTimeWindow");
            }
            else
            {
                foreach (var route in result)
                {
                    Console.WriteLine($"Route {route.RouteId}: insert at stop {route.RecommendedInsertionStopNumber}, " +
                                      $"time {route.OldTime} -> {route.NewTime}");

                    var violations = route.TimeWindowViolations;

                    if ((violations?.Count ?? 0) == 0)
                    {
                        Console.WriteLine("     No stop becomes late.");
                        continue;
                    }

                    Console.WriteLine($"     {violations.Count} stop(s) become late, worst by {violations.MaxLateSeconds} s:");

                    foreach (var stop in violations.Destinations)
                    {
                        var stopName = stop.RouteDestinationId?.ToString() ?? "the new stop";
                        Console.WriteLine($"     {stopName} at position {stop.NewSequenceNo}, late by {stop.LateSeconds} s");
                    }
                }
            }

            RemoveTestOptimizations();
        }
    }
}