using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search the orders be containing specified text in any text field
        /// </summary>
        public void SearchOrdersBySpecifiedText(string query = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Preapre query parameters

            bool isInnerExample = query == null ? true : false;

            if (isInnerExample)
            {
                CreateExampleOrder();
                query = "Carol";
            }

            var oParams = new OrderParameters()
            {
                Query = query,
                Offset = 0,
                Limit = 20
            };

            #endregion

            // Send a request to the server
            var result = route4Me.SearchOrders(oParams, out string errorString);

            // Print the result on the console
            PrintExampleOrder(result, errorString);

            // Remove test data
            if (isInnerExample) RemoveTestOrders();
        }
    }
}