namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle by its license plate 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehiclesByLicensePlateV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var result = await route4Me.GetVehicleByLicensePlateAsync("CVH4561");

            PrintTestVehcilesV5(result.Item1?.Data?.Vehicle ?? null, result.Item2);
        }
    }
}