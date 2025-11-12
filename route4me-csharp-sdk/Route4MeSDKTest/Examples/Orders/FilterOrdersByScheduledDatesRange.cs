using System;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Filter and retrieve the orders using filter parameters <see cref="OrderFilterParameters"/>
        /// </summary>
        public void FilterOrdersByScheduledDatesRange()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            // Set the start and end dates for the filter
            string startDate = (DateTime.Now - (new TimeSpan(1, 0, 0, 0)))
                .ToString("yyyy-MM-dd");
            string endDate = (DateTime.Now + (new TimeSpan(31, 0, 0, 0)))
                .ToString("yyyy-MM-dd");

            // Preapre the filter parameters
            var oParams = new OrderFilterParameters()
            {
                Limit = 10,
                Filter = new FilterDetails()
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new string[] { startDate, endDate }
                }
            };

            // Send a request to the server
            Order[] orders = route4Me.FilterOrders(oParams, out string errorString);

            // Print the result on the console
            PrintExampleOrder(orders, errorString);

            // Remove test data
            RemoveTestOrders();
        }
    }
}