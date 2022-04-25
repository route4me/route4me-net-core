using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Show Orders by tracking numbers.
        /// </summary>
        public void GetOrdersByTrackingNumbers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var trakingNumbers = new List<string>()
            {
                "US123274", "26K2RJ0PX4", "2P8799MRJC"
            };

            var filterParams = new OrderFilterParameters
            {
                Offset = 0,
                Limit = 10,
                Filter = new FilterDetails
                {
                    TrackingNumbers = trakingNumbers.ToArray()
                }
            };

            var orders = route4Me.FilterOrders(filterParams, out var errorString);

            PrintExampleOrder(orders, errorString);
        }
    }
}
