using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Filter and retrieve the orders by status <see cref="OrderStatuses"/>.
        /// </summary>
        public void FilterOrdersByStatus()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            #region Prepare an array of the order statuses

            var orderStatuses = new List<int>();

            orderStatuses.Add((int)OrderStatuses.Done);
            orderStatuses.Add((int)OrderStatuses.Cancelled);
            orderStatuses.Add((int)OrderStatuses.Damaged);

            #endregion

            // Preapre query parameter with filter
            var orderParams = new OrderFilterParameters()
            {
                Filter = new FilterDetails()
                {
                    Statuses = orderStatuses.ToArray()
                }
            };

            // Send a request to the server
            var orders = route4Me.FilterOrders(orderParams, out string errorString);

            // Print the result on the console
            PrintExampleOrder(orders, errorString);
        }
    }
}
