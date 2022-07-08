using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // The example refers to the process of retrieving a route list (API 5 endpoint).
        public void GetAllRoutesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            // Run the query
            var result = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);

            PrintExampleRouteResult(result, null, resultResponse);
        }
    }
}
