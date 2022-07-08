using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle using the API 5 endpoint.
        /// </summary>
        public void UpdateVehicleV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleParams = new Vehicle()
            {
                VehicleId = "68F3545D3DA82DBF007B20FA9A1875EB",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2019
            };

            var vehicle = route4Me.UpdateVehicle(vehicleParams, out ResultResponse resultResponse);

            PrintTestVehcilesV5(vehicle, resultResponse);
        }
    }
}
