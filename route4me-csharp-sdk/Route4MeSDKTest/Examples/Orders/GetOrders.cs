using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get limited list of the Orders
        /// </summary>
        public void GetOrders()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Preapre query parameter
            var orderParameters = new OrderParameters()
            {
                Limit = 10
            };

            // Send a request to the server
            Order[] orders = route4Me.GetOrders(
                orderParameters,
                out uint total,
                out string errorString);

            // Print the result on the console
            PrintExampleOrder(orders, errorString);
        }
    }
}
