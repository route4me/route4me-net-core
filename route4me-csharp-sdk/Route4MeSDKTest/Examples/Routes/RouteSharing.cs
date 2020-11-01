using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void RouteSharing(string routeId, string Email)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Example refers to the process of sharing a route by email

            // Run the query
            var parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            bool result = route4Me.RouteSharing(parameters, Email, out string errorString);

            Console.WriteLine("");

            if (result)
            {
                Console.WriteLine("RouteSharing executed successfully");
            }
            else
            {
                Console.WriteLine("RouteSharing error {0}", errorString);
            }
        }
    }
}
