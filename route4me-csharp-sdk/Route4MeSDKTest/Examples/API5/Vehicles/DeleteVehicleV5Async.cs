namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of removing a vehicle 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void DeleteVehicleV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var result = await route4Me.DeleteVehicleAsync("68F3545D3DA82DBF007B20FA9A1875EB");

            PrintTestVehcilesV5(result.Item1, result.Item2);
        }
    }
}