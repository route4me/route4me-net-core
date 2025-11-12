using System;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // Get the job status of an asynchronous process related to the address book contacts asynchronously.
        public async void GetContactJobStatusAsync()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var job_id = "806";

            // Run the query
            var result = await route4Me.GetContactsJobStatusAsync(job_id);

            Console.WriteLine($"Job status: {result.Item1?.IsSuccessStatusCode ?? false}");
        }
    }
}