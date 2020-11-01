using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove Territory
        /// </summary>
        public void RemoveTerritory()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            string territoryId = "12ABBFA3B5E4FB007BB0ED73291576C2";

            var territoryQuery = new AvoidanceZoneQuery { TerritoryId = territoryId };

            // Run the query
            route4Me.RemoveTerritory(territoryQuery, out string errorString);

            Console.WriteLine("");

            if (string.IsNullOrEmpty(errorString))
            {
                Console.WriteLine("RemoveTerritory executed successfully");

                Console.WriteLine("Territory ID: {0}", territoryId);
            }
            else
            {
                Console.WriteLine("RemoveTerritory error: {0}", errorString);
            }
        }
    }
}
