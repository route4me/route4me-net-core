using System;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a job result 
        /// using the API 5 endpoint.
        /// </summary>
        public void GetJobResultV5()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Send a request to the server
            var result =
                route4Me.GetVehicleJobResult("50CE911542B397C7EBCDDA13CDE5580A", out ResultResponse resultResponse);

            Console.WriteLine("Job result: " +
                              (result?.IsSuccessStatusCode ?? false));
        }
    }
}