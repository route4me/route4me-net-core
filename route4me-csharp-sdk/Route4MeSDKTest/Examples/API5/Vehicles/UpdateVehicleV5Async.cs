using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void UpdateVehicleV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleParams = new Vehicle()
            {
                VehicleId = "68F3545D3DA82DBF007B20FA9A1875EB",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018
            };

            var result = await route4Me.UpdateVehicleAsync(vehicleParams);

            PrintTestVehiclesV5(result.Item1, result.Item2);
        }
    }
}