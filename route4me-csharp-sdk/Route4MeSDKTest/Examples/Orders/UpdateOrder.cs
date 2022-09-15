using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an order
        /// </summary>
        /// <param name="order1"> Order with updated attributes </param>
        public void UpdateOrder(Order order1 = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Prepare query parameters

            bool isInnerExample = (order1 == null ? true : false);

            if (isInnerExample) CreateExampleOrder();

            string orderId = isInnerExample
                ? OrdersToRemove[OrdersToRemove.Count - 1]
                : order1.OrderId.ToString();

            var orderParameters = new OrderParameters()
            {
                order_id = orderId
            };

            #endregion

            Order order = route4Me.GetOrderByID(
                orderParameters,
                out string errorString);

            order.ExtFieldLastName = "Updated " + (new Random()).Next().ToString();

            order.Address1 += " Updated";

            order.ExtFieldCustomData = new Dictionary<string, string>
            {
                { "order_type", "scheduled order" }
            };

            // Send a request to the server
            var updatedOrder = route4Me.UpdateOrder(order, out errorString);

            // Print the result on the console
            PrintExampleOrder(updatedOrder, errorString);

            // Remove test data
            if (isInnerExample) RemoveTestOrders();
        }
    }
}