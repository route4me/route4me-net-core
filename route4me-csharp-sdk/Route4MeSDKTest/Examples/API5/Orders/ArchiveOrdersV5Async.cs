using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of archiving the orders 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void ArchiveOrdersV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };

            var result = await route4Me.ArchiveOrdersAsync(parameters);

            PrintTestOrders(result.Item1);
        }
    }
}