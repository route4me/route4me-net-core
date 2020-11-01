using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove Orders
        /// </summary>
        /// <param name="orderIds"> Order Ids </param>
        public void RemoveOrders(string[] orderIds)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            bool removed = route4Me.RemoveOrders(orderIds, out string errorString);

            Console.WriteLine("");

            if (removed)
            {
                Console.WriteLine("RemoveOrders executed successfully, {0} orders removed", orderIds.Length);
            }
            else
            {
                Console.WriteLine("RemoveOrders error: {0}", errorString);
            }
        }
    }
}
