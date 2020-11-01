using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Visited
        /// </summary>
        /// <returns> status </returns>
        public void MarkAddressVisited(AddressParameters aParams)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Run the query
            object oResult = route4Me.MarkAddressVisited(aParams, out string errorString);

            Console.WriteLine("");

            if (oResult != null)
            {
                int result = Convert.ToInt32(oResult);
                if (result == 1)
                {
                    Console.WriteLine("MarkAddressVisited executed successfully");
                }
                else
                {
                    Console.WriteLine("MarkAddressVisited error: {0}", errorString);
                }
            }
            else
            {
                Console.WriteLine("MarkAddressVisited error: {0}", errorString);
            }
        }
    }
}
