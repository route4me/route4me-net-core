using System;
using System.Linq;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting an orders history using the API 5 endpoint.
        /// </summary>
        public void GetOrdersHistoryV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var route4me = new Route4MeManager(ActualApiKey);

            var orders = route4me.GetOrders(new QueryTypes.OrderParameters() { Limit = 1 }, out _, out _);

            if ((orders?.Length ?? 0) < 1)
            {
                Console.WriteLine("Cannot retrieve the orders from the account.");
                return;
            }

            var parameters = new OrderHistoryParameters()
            {
                OrderId = orders.Single().OrderId,
                TrackingNumber = orders.Single().TrackingNumber
            };

            var result = route4Me.GetOrderHistory(parameters, out var resultResponse);

            PrintTestOrders(result);
        }
    }
}