using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle by its license plate state using the API 5 endpoint.
        /// </summary>
        public void GetVehiclesByLicensePlateV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicles = route4Me.GetVehicleByLicensePlate("CVH4561", out ResultResponse resultResponse);

            PrintTestVehcilesV5(vehicles.Data.Vehicle, resultResponse);
        }
    }
}