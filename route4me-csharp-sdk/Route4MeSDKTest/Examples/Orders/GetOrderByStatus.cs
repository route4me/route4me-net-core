using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Orders by statuses.
        /// </summary>
        public void GetOrderByStatus()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var orderStatuses = new List<int>();

            orderStatuses.Add((int)OrderStatuses.Done);
            orderStatuses.Add((int)OrderStatuses.Cancelled);
            orderStatuses.Add((int)OrderStatuses.Damaged);

            var orderParams = new OrderFilterParameters()
            {
                Filter = new FilterDetails()
                {
                    Statuses = orderStatuses.ToArray()
                }
            };

            var orders = route4Me.FilterOrders(orderParams, out string errorString);

            PrintExampleOrder(orders, errorString);
        }
    }
}
