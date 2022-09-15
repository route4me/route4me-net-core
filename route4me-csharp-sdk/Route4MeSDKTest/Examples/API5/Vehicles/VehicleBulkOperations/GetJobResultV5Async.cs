using Route4MeSDK.DataTypes.V5;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a job result 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetJobResultV5Async()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Send a request to the server
            var result = await route4Me.GetVehicleJobResultAsync("50CE911542B397C7EBCDDA13CDE5580A");

            Console.WriteLine("Job result: " +
                              (result?.Item1?.IsSuccessStatusCode ?? false));
        }
    }
}