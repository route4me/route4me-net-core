using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of executing a vehicle order using the API 5 endpoint.
        /// </summary>
        public void ExecuteVehicleOrderV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var randomMember = GetRandomTeamMember();

            var vehicleParams = new VehicleOrderParameters()
            {
                VehicleId = "FFBBFE7960518F7B4A79C5767C9B10CF",
                Latitude = 40.777213,
                Longitude = -73.9669
            };

            var result = route4Me.ExecuteVehicleOrder(vehicleParams, out ResultResponse resultResonse);

            PrintTestVehiclesV5(result, resultResonse);
        }
    }
}