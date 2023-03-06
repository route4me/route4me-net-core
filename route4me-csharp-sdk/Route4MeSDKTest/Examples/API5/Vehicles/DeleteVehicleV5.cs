using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of removing a vehicle using the API 5 endpoint.
        /// </summary>
        public void DeleteVehicleV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleData = route4Me.DeleteVehicle(
                "FFBBFE7960518F7B4A79C5767C9B10CF",
                out ResultResponse resultResponse
            );

            PrintTestVehiclesV5(vehicleData, resultResponse);
        }
    }
}