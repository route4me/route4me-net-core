using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void SearchRoutesForText(string query)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Example refers to the process of searching for the specified text throughout all routes belonging to the user's account.

            var routeParameters = new RouteParametersQuery()
            {
                Query = query
            };

            // Run the query
            DataObjectRoute[] dataObjects = route4Me.GetRoutes(routeParameters, out string errorString);

            Console.WriteLine("");

            if (dataObjects != null)
            {
                Console.WriteLine("SearchRoutesForText executed successfully, {0} routes returned", dataObjects.Length);
                Console.WriteLine("");

                dataObjects.ForEach(dataObject =>
                {
                    Console.WriteLine("RouteID: {0}", dataObject.RouteId);
                    Console.WriteLine("");
                });
            }
            else
            {
                Console.WriteLine("SearchRoutesForText error {0}", errorString);
            }
        }
    }
}
