using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Departed
        /// </summary>
        /// <returns> status </returns>
        public void MarkAddressDeparted(AddressParameters aParams)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query

            int result = route4Me.MarkAddressVisited(aParams, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                if (result == 1)
                {
                    Console.WriteLine("MarkAddressDeparted executed successfully");
                }
                else
                {
                    Console.WriteLine("MarkAddressDeparted error: {0}", errorString);
                }
            }
            else
            {
                Console.WriteLine("MarkAddressVisited error: {0}", errorString);
            }
        }
    }
}
