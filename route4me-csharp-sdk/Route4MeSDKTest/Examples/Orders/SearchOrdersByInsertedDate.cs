using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search the orders by inserted (in the database) date.
        /// </summary>
        public void SearchOrdersByInsertedDate()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            #region Prepare query parameter

            string InsertedDate = DateTime.Now.ToString("yyyy-MM-dd");

            var oParams = new OrderParameters { DayAddedYYMMDD = InsertedDate };

            #endregion

            // Send a request to the server
            var result = route4Me.SearchOrders(oParams, out string errorString);

            // Print the result on the console
            PrintExampleOrder(result, errorString);

            #region Remove test data

            if (result != null && result.GetType() == typeof(GetOrdersResponse))
            {
                OrdersToRemove = new List<string>();

                foreach (Order ord in ((GetOrdersResponse)result).Results)
                    OrdersToRemove.Add(ord.OrderId.ToString());
            }

            RemoveTestOrders();

            #endregion
        }
    }
}