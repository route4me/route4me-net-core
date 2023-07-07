using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Update an order with custom field.
        /// </summary>
        public void UpdateOrderWithCustomField()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            // Prepare a custom field
            lastCreatedOrder.CustomUserFields = new OrderCustomField[]
            {
                new OrderCustomField()
                {
                    OrderCustomFieldId = 93,
                    OrderCustomFieldValue = "true"
                }
            };

            // Send a request to the server
            var result = route4Me.UpdateOrder(lastCreatedOrder, out string errorString);

            // Print the result on the console
            PrintExampleOrder(result, errorString);

            // Remove test data
            RemoveTestOrders();
        }
    }
}