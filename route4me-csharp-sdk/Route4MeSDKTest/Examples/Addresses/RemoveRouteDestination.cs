using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void RemoveRouteDestination(string routeId, int destinationId)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            bool deleted = route4Me.RemoveRouteDestination(routeId, destinationId, out string errorString);

            Console.WriteLine("");

            if (deleted)
            {
                Console.WriteLine("RemoveRouteDestination executed successfully");

                Console.WriteLine("Destination ID: {0}", destinationId);
            }
            else
            {
                Console.WriteLine("RemoveRouteDestination error: {0}", errorString);
            }
        }
    }
}
