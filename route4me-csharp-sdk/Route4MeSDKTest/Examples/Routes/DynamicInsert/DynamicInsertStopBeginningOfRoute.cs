using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Insert an address dynamically at the route's beginning.
        /// </summary>
        public void DynamicInsertStopBeginningOfRoute()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var addressIsert = new Address()
            {
                AddressString = "181 Pebble Hollow Dr, Milledgeville, GA 31061, USA",
                Latitude = 33.1296514,
                Longitude = -83.2485687
            };


            var dynamicInsertParams = new DynamicInsertRequest()
            {
                InsertMode = DynamicInsertMode.BeginningOfRoute.Description(),
                ScheduledFor = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"),
                Latitude = addressIsert.Latitude,
                Longitude = addressIsert.Longitude,
                LookupResultsLimit = 5,
                RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
                MaxIncreasePercentAllowed = 500
            };


            // Run the query
            var result = route4Me.DynamicInsertRouteAddresses(dynamicInsertParams, out ResultResponse resultResponse);

            if (result == null)
            {
                Console.WriteLine("DynamicInsertStopBeginningOfRoute failed.");
                return;
            }
            else
            {
                Console.WriteLine($"The address can be inserted at the routes: ");

                foreach (var route in result)
                {
                    Console.WriteLine($"     {route.RouteId}");
                }
            }

            RemoveTestOptimizations();
        }
    }
}