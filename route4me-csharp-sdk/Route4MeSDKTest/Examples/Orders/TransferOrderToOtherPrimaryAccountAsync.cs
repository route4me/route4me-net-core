using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates how to transfer an order to other primary account asynchronously.
        /// </summary>
        public async void TransferOrderToOtherPrimaryAccountAsync()
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

            var orderResult = await route4Me.GetOrderByIDAsync(orderParameters);

            #endregion

            // Replace the destination primary API key with a real primery API key
            string anotherPrimeryApiKey = "22222222222222222222222222222222";

            long destinationRootMemberId = (long)GetOwnerMemberId(anotherPrimeryApiKey);

            var orderToTransfer = new Order()
            {
                RootMemberId = destinationRootMemberId, // Route member ID of the destination account,
                OrderId = orderResult.Item1.OrderId,
                Address1 = orderResult.Item1.Address1
            };

            // Send a request to the server
            var result = await route4Me.TransferOrderToOtherPrimaryAccountAsync(orderToTransfer, anotherPrimeryApiKey);

            // Print the result on the console
            PrintExampleOrder(result.Item1, result.Item2);
        }
    }
}