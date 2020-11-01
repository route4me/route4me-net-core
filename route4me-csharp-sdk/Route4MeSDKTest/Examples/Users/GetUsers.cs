using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetUsers()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var parameters = new GenericParameters()
            {
            };

            // Run the query
            Route4MeManager.GetUsersResponse dataObjects = route4Me.GetUsers(parameters, out string errorString);

            Console.WriteLine("");

            if (dataObjects != null)
            {
                if (dataObjects.Results != null)
                {
                    Console.WriteLine("GetUsers executed successfully, {0} users returned", dataObjects.Results.Length);
                    Console.WriteLine("");
                }
                else Console.WriteLine("GetUsers error: {0}", errorString);

            }
            else
            {
                Console.WriteLine("GetUsers error: {0}", errorString);
            }
        }
    }
}
