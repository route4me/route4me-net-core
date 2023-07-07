using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of archiving the orders using the API 5 endpoint.
        /// </summary>
        public void ArchiveOrdersV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };

            var result = route4Me.ArchiveOrders(parameters, out var resultResponse);

            PrintTestOrders(result);
        }
    }
}