using Route4MeSDK.QueryTypes.V5;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // The example refers to the process of retrieving a route by ID asynchronously (API 5 endpoint).
        public async void GetAllRoutesByIdV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var routeParameters = new RouteParametersQuery
            {
                RouteId = SD10Stops_route.RouteId
            };

            // Run the query
            var result = await route4Me.GetRouteByIdAsync(routeParameters);

            PrintExampleRouteResult(result.Item1, null, result.Item2);

            RemoveTestOptimizations();
        }
    }
}
