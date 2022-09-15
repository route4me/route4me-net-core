using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Show the orders with the selected fields
        /// </summary>
        public void ShowOrdersWithSelectedFields(string CustomFields = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Preapre query parameters
            var oParams = new OrderParameters()
            {
                Fields = CustomFields == null
                    ? "order_id,member_id"
                    : CustomFields,
                Offset = 0,
                Limit = 20
            };

            // Send a request to the server
            var result = route4Me.SearchOrders(oParams, out string errorString);

            // Print the result on the console
            PrintExampleOrder(result, errorString);
        }
    }
}