namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle by ID 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehicleByIdV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var result = await route4Me.GetVehicleByIdAsync("68F3545D3DA82DBF007B20FA9A1875EB");

            PrintTestVehcilesV5(result.Item1, result.Item2);
        }
    }
}
