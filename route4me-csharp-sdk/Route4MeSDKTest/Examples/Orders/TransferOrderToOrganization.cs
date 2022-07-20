using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates how to transfer an order to other organization.
        /// </summary>
        public void TransferOrderToOrganization()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Prepare query parameters

            CreateExampleOrder();

            string orderId = OrdersToRemove[OrdersToRemove.Count - 1];

            var orderParameters = new OrderParameters()
            {
                order_id = orderId
            };

            Order order = route4Me.GetOrderByID(
                orderParameters,
                out string errorString);

            #endregion

            // Demo 128-chars API key of a destination organization
            // Replace it with a real organization API key
            string organizationApiKey = new String('1', 128); 

            // Send a request to the server
            var transferedOrder = route4Me.TransferOrderToOrganization(order, organizationApiKey, out errorString);

            // Print the result on the console
            PrintExampleOrder(transferedOrder, errorString);

        }
    }
}
