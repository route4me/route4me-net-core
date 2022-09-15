using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of restoring the vehicles 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void RestoreVehiclesV5Async()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare the query parameters
            string[] vehicleIDs = new string[]
            {
                "2001114BAE4E861642455771FEF9E0F1"
            };

            // Send a request to the server
            var result = await route4Me.RestoreVehiclesAsync(vehicleIDs);

            Console.WriteLine("Restore result: " +
                              (result?.Item1?.IsSuccessStatusCode ?? false));
        }
    }
}