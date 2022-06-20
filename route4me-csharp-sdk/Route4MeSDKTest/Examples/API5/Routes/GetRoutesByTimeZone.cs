using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetRoutesByTimeZone()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Timezone = "America/New_York"
            };

            var routes = route4Me.GetRoutesByFilter(
                routeParameters,
                out ResultResponse resultResponse);

            PrintExampleRouteResult(routes, null, resultResponse);
        }
    }
}
