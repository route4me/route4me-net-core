using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of restoring the vehicles using the API 5 endpoint.
        /// </summary>
        public async void RestoreVehiclesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                "2001114BAE4E861642455771FEF9E0F1"
            };

            var result = await route4Me.RestoreVehicles(vehicleIDs);

            string jobId = result.Item3;

            var jobResult = route4Me.GetVehicleJobResult(jobId, out ResultResponse resultResponse);

            System.Console.WriteLine($"Job result: {(jobResult?.status ?? false)}");

            System.Console.WriteLine("Restore result: "+(result.Item2==null ? true : false));
        }
    }
}
