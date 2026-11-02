using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Suggest where a route needs refuelling, with fuel stations along the way.
        ///     The account needs the Fueling API feature. The route itself is not changed:
        ///     <see cref="InsertSuggestedRefuelStop"/> shows how to insert a suggested station.
        /// </summary>
        public void SuggestRouteRefuels()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var refuelRequest = new RouteRefuelSuggestionsRequest()
            {
                // The test route has no vehicle, so the fuel values are given here.
                Routes = new[]
                {
                    new RouteRefuelRouteRequest()
                    {
                        RouteId = SD10Stops_route_id,
                        StartingFuelLevelPercent = 15,
                        TankCapacity = 40,
                        TankCapacityUnit = "L",
                        FuelConsumption = 9,
                        FuelConsumptionUnit = "l/100km"
                    }
                },
                MaxStations = 3
            };

            var result = route4Me.SuggestRouteRefuels(refuelRequest, out ResultResponse resultResponse);

            if (result == null)
            {
                PrintFailResponse(resultResponse, "SuggestRouteRefuels");
            }
            else
            {
                foreach (var route in result.Routes)
                {
                    if (route.Status != "ok")
                    {
                        Console.WriteLine($"Route {route.RouteId} skipped: {route.Reason}");
                        continue;
                    }

                    Console.WriteLine($"Route {route.RouteId}: {route.Suggestions.Length} refuel(s), " +
                                      $"{route.EstimatedLevelAtFinishPercent}% left at the finish");

                    foreach (var suggestion in route.Suggestions)
                    {
                        Console.WriteLine($"     Refuel before stop {suggestion.RefuelBeforeDestination?.SequenceNo}, " +
                                          $"{suggestion.EstimatedLevelOnArrivalPercent}% on arrival");

                        foreach (var station in suggestion.Stations)
                        {
                            Console.WriteLine($"     {station.Name}, {station.Address?.Label}: " +
                                              $"insert after stop {station.InsertAfterDestination?.SequenceNo}, " +
                                              $"detour {station.DetourDistanceMeters} m");
                        }
                    }
                }
            }

            RemoveTestOptimizations();
        }
    }
}