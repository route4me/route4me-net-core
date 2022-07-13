using Route4MeSDK.DataTypes.V5;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of restoring the vehicles 
        /// using the API 5 endpoint.
        /// </summary>
        public void RestoreVehiclesV5()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare the query parameters
            string[] vehicleIDs = new string[]
            {
                "2001114BAE4E861642455771FEF9E0F1"
            };

            // Send a request to the server
            var result = route4Me.RestoreVehicles(vehicleIDs, out ResultResponse resultResponse);

            Console.WriteLine("Restore result: "+
                (result?.IsSuccessStatusCode ?? false));
        }
    }
}
