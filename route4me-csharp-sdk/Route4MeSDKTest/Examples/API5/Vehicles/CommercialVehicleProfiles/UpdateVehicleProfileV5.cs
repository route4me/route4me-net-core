using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle profile using the API 5 endpoint.
        /// </summary>
        public void UpdateVehicleProfileV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfile()
            {
                VehicleProfileId = 154043,
                Weight = 9999,
                MaxWeightPerAxle = 11999
            };

            var profile = route4Me.UpdateVehicleProfile(profileParams, out ResultResponse resultResponse);

            PrintTestVehcileProfilesV5(profile, resultResponse);
        }
    }
}
