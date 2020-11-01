using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void DeleteRoutes(string[] routeIds)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            string[] deletedRouteIds = route4Me.DeleteRoutes(routeIds, out string errorString);

            Console.WriteLine("");

            if (deletedRouteIds != null)
            {
                Console.WriteLine("DeleteRoutes executed successfully, {0} routes deleted", deletedRouteIds.Length);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("DeleteRoutes error {0}", errorString);
            }
        }
    }
}
