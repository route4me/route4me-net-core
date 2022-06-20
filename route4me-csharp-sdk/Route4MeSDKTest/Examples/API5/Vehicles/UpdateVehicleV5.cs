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
                VehicleId = "FFBBFE7960518F7B4A79C5767C9B10CF",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018
            };

            var vehicle = route4Me.UpdateVehicle(vehicleParams, out ResultResponse resultResponse);

            PrintTestVehcilesV5(vehicle, resultResponse);
        }
    }
}
