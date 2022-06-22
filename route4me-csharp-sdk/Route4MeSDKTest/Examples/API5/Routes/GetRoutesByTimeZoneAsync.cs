using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // Retrieve the routes by timezone asynchronously.
        public async void GetRoutesByTimeZoneAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Timezone = "America/New_York"
            };

            var result = await route4Me.GetRoutesByFilterAsync(
                routeParameters);

            
            PrintExampleRouteResult(result.Item1, null, result.Item2);
        }
    }
}
