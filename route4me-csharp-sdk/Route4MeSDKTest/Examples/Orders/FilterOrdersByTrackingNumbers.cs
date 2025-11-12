using System.Collections.Generic;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        ///     Filter and show the orders by tracking numbers.
        /// </summary>
        public void FilterOrdersByTrackingNumbers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Prepare filter parameters

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

            #endregion

            // Send a request to the server
            var orders = route4Me.FilterOrders(filterParams, out var errorString);

            // Print the result on the console
            PrintExampleOrder(orders, errorString);
        }
    }
}