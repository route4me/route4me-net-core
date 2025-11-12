using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Update Route Driver
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        [System.Obsolete]
        public void UpdateRouteDriver()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var users = route4Me.GetUsers(new GenericParameters(), out _);

            if (users.Results.Length < 1)
            {
                System.Console.WriteLine("Cannot retrieve the users");
                return;
            }

            var userId = long.Parse(users.Results[0].MemberId);

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            var parametersNew = new RouteParameters()
            {
                RouteName = "Renaming and Assining Driver to an Existing Route",
                MemberId = userId
            };

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_id,
                Parameters = parametersNew
            };

            // Run the query
            DataObjectRoute dataObject = route4Me.UpdateRoute(
                routeParameters,
                out string errorString
            );

            PrintExampleRouteResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}