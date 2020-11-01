using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Marked as Departed
        /// </summary>
        /// <returns> status </returns>
        public void MarkAddressAsMarkedAsDeparted(AddressParameters aParams)
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

            // Run the query

            Address resultAddress = route4Me.MarkAddressAsMarkedAsDeparted(aParams, out string errorString);

            Console.WriteLine("");

            if (resultAddress != null)
            {
                Console.WriteLine("MarkAddressAsMarkedAsDeparted executed successfully");

                Console.WriteLine("Marked Address ID: {0}", resultAddress.RouteDestinationId);
            }
            else
            {
                Console.WriteLine("MarkAddressAsMarkedAsDeparted error: {0}", errorString);
            }

        }
    }
}
