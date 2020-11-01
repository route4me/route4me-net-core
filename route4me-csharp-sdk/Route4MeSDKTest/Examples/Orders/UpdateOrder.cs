using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="order"> Order with updated attributes </param>
        public void UpdateOrder(Order order)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            Order updatedOrder = route4Me.UpdateOrder(order, out string errorString);

            Console.WriteLine("");

            if (updatedOrder != null)
            {
                Console.WriteLine("UpdateOrder executed successfully");
            }
            else
            {
                Console.WriteLine("UpdateOrder error: {0}", errorString);
            }
        }
    }
}
