using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a job result using the API 5 endpoint.
        /// </summary>
        public async void GetJobResultV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);



            var result = route4Me.GetVehicleJobResult("50CE911542B397C7EBCDDA13CDE5580A", out ResultResponse resultResponse);

            System.Console.WriteLine($"Job result: {result.status}");

        }
    }
}
