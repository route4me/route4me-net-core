using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Marked as Visited
        /// </summary>
        /// <returns> status </returns>
        public void MarkAddressAsMarkedAsVisited(AddressParameters aParams)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            Address resultAddress = route4Me.MarkAddressAsMarkedAsVisited(aParams, out string errorString);

            Console.WriteLine("");

            if (resultAddress != null)
            {
                Console.WriteLine("MarkAddressAsMarkedAsVisited executed successfully");

                Console.WriteLine("Marked Address ID: {0}", resultAddress.RouteDestinationId);
            }
            else
            {
                Console.WriteLine("MarkAddressAsMarkedAsVisited error: {0}", errorString);
            }
        }
    }
}
