using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// The example refers to the process of retrieving a route list asynchronously (API 5 endpoint).
        public async void GetAllRoutesV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            // Run the query
            var result = await route4Me.GetRoutesAsync(routeParameters);

            PrintExampleRouteResult(result.Item1, null, result.Item2);
        }
    }
}