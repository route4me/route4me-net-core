using System;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Order
        /// </summary>
        /// <returns> Added Order </returns>
        public void AddOrderWithNoLatAndLngAndUpdateAfterwards()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(0, 1000),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(0, 1000)
            };

            // Run the query
            Order resultOrder = route4Me.AddOrder(order, out string errorString);

            resultOrder.IsValidated = true;

            Order updatedOrder = route4Me.UpdateOrder(resultOrder, out errorString);

            PrintExampleOrder(updatedOrder, errorString);

            RemoveTestOrders();
        }
    }
}