using System;
using System.Collections.Generic;
using System.Linq;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Ask where a route needs refuelling, then insert the suggested fuel station
        ///     into the route as a REFUEL stop. The account needs the Fueling API feature.
        /// </summary>
        public void InsertSuggestedRefuelStop()
        {
            var route4MeV5 = new Route4MeManagerV5(ActualApiKey);
            var route4Me = new Route4MeManager(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var refuelRequest = new RouteRefuelSuggestionsRequest()
            {
                // The test route has no vehicle, so the fuel values are given here.
                // The low starting level makes the route need a refuel.
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

            var suggestions = route4MeV5.SuggestRouteRefuels(refuelRequest, out var resultResponse);

            if (suggestions == null)
            {
                PrintFailResponse(resultResponse, "InsertSuggestedRefuelStop");
                RemoveTestOptimizations();
                return;
            }

            var route = suggestions.Routes[0];

            // Stations are ordered by detour, so the first one placed on the route is the cheapest to visit.
            var station = route.Suggestions
                .SelectMany(suggestion => suggestion.Stations)
                .FirstOrDefault(candidate => candidate.Position?.Latitude != null
                                             && candidate.Position.Longitude != null
                                             && candidate.InsertAfterDestination?.SequenceNo != null);

            if (station == null)
            {
                Console.WriteLine($"No station to insert. Route status: {route.Status} {route.Reason}, " +
                                  $"warnings: {string.Join(", ", route.Warnings)}");
                RemoveTestOptimizations();
                return;
            }

            var refuelStop = new Address()
            {
                AddressString = station.Address?.Label ?? station.Name,
                Alias = station.Name,
                Latitude = station.Position.Latitude.Value,
                Longitude = station.Position.Longitude.Value,
                // The access point is where the vehicle enters the station.
                CurbsideLatitude = station.Access?.Latitude,
                CurbsideLongitude = station.Access?.Longitude,
                AddressStopType = AddressStopType.Refuel.Description(),
                // The station lies on the leg that starts at this stop, so the refuel goes right after it.
                SequenceNo = station.InsertAfterDestination.SequenceNo + 1,
                // Time spent at the station, seconds.
                Time = 600
            };

            var destinationIds = route4Me.AddRouteDestinations(
                route.RouteId,
                new[] { refuelStop },
                false,
                out string errorString);

            PrintExampleDestination(destinationIds, errorString);

            RemoveTestOptimizations();
        }
    }
}