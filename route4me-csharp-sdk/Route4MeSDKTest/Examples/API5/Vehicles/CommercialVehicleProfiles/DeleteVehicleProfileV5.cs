using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting a vehicle profile using the API 5 endpoint.
        /// </summary>
        public void DeleteVehicleProfileV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                VehicleProfileId = 154043
            };

            var result = route4Me.DeleteVehicleProfile(profileParams, out ResultResponse resultResponse);

            PrintTestVehcileProfilesV5(result, resultResponse);
        }
    }
}