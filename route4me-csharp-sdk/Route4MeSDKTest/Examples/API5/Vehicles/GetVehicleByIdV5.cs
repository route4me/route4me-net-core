using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle by ID using the API 5 endpoint.
        /// </summary>
        public void GetVehicleByIdV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleData = route4Me.GetVehicleById(
                "FFBBFE7960518F7B4A79C5767C9B10CF", 
                out ResultResponse resultResponse
                );

            PrintTestVehcilesV5(vehicleData, resultResponse);
        }
    }
}
