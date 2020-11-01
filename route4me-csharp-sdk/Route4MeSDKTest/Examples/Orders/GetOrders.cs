using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders
        /// </summary>
        public void GetOrders()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderParameters = new OrderParameters()
            {
                Limit = 10
            };

            Order[] orders = route4Me.GetOrders(orderParameters, out uint total, out string errorString);

            Console.WriteLine("");

            if (orders != null)
            {
                Console.WriteLine("GetOrders executed successfully, {0} orders returned, total = {1}", orders.Length, total);
            }
            else
            {
                Console.WriteLine("GetOrders error: {0}", errorString);
            }
        }
    }
}
