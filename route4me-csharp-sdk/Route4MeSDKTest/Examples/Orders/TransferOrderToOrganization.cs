using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

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

            // Replace the destination primary API key with a real primery API key
            string anotherPrimeryApiKey = "22222222222222222222222222222222";

            long destinationRootMemberId = (long)GetOwnerMemberId(anotherPrimeryApiKey);

            var orderToTransfer = new Order()
            {
                RootMemberId = destinationRootMemberId, // Route member ID of the destination account
                OrderId = order.OrderId,
                Address1 = order.Address1
            };

            // Send a request to the server
            var transferedOrder = route4Me.TransferOrderToOrganization(orderToTransfer, anotherPrimeryApiKey, out errorString);

            // Print the result on the console
            PrintExampleOrder(transferedOrder, errorString);

        }
    }
}
