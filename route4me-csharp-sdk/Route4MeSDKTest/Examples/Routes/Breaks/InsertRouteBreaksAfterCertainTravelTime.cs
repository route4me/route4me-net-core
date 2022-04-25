using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Insert Route Breaks After Certain Travel Time.
        /// </summary>
        public void InsertRouteBreaksAfterCertainTravelTime()
        {
            // Use an API key, which enables you to insert the route breaks. 
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            RunSingleDriverRoundTrip();

            OptimizationsToRemove.Add(SDRT_optimization_problem_id);

            var routeBreaks = new RouteBreaks()
            {
                RouteId = new string[] { SD10Stops_route_id, SDRT_route_id },
                Breaks = new RouteBreak[]
                {
                    new RouteBreak()
                    {
                        Type =  RouteBreakTypes.CERTAIN_NUMBER_OF_TRAVEL_TIME.Description(),
                        Duration = 900,
                        Params = new int[] { 2700, 2700 }
                    }
                },
                ReplaceExistingBreaks = true

            };


            // Run the query
           var result = route4Me.InsertRouteBreaks(routeBreaks, out ResultResponse resultResponse);

            if (result == null)
            {
                Console.WriteLine("InsertRouteBreaksAfterCertainTravelTime failed.");
                return;
            }


            Console.WriteLine($"The route breaks " + (result.status ? "" : "not") + " inserted");

            RemoveTestOptimizations();
        }
    }
}
