using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // Retrieve the routes by timezone 
        public void GetRoutesByRouteName()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters()
                {
                    RouteName = "my route name"
                }
            };

            var result = route4Me.GetRoutesByFilter(
                routeParameters,
                out ResultResponse resultResponse);

            PrintExampleRouteResult(result, null, resultResponse);
        }
    }
}