using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Assign a team member to a route.
        /// </summary>
        [Obsolete]
        public void AssignMemberToRoute()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var members = route4Me.GetUsers(
                new GenericParameters(),
                out string errorString
            );

            int randomNumber = (new Random()).Next(0, members.Results.Length - 1);

            var memberId = members.Results[randomNumber].MemberId != null
                ? Convert.ToInt32(members.Results[randomNumber].MemberId)
                : -1;

            RunOptimizationSingleDriverRoute10Stops();
            OptimizationsToRemove = new List<string>()
            {
                SD10Stops_optimization_problem_id
            };

            string routeId = SD10Stops_route_id;

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = new RouteParameters()
                {
                    MemberId = memberId
                }
            };

            var updatedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            PrintExampleRouteResult(updatedRoute, errorString);

            RemoveTestOptimizations();
        }
    }
}