using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OptimizationByOrderTerritories()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Optimization by order territories, " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 5 * 3600 + 30 * 60
            };

            var orderTerritories = new OrderTerritories()
            {
                SplitTerritories = true,
                TerritoriesId = new string[] { "015F1568818C0AEB63E2B63ADE7F819F", "01F8572D0965E4C90879996DFED5B58D" },
                filters = new FilterDetails()
                {
                    Display = "unrouted",
                    Scheduled_for_YYYYMMDD = new string[] { "2021-09-01" }
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                OrderTerritories = orderTerritories,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunOptimization(optimizationParameters, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }

}
