using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a list of the vehicle profiles using the API 5 endpoint.
        /// </summary>
        public void GetVehicleProfilesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                WithPagination = true
            };

            var vehicleProfiles = route4Me.GetVehicleProfiles(profileParams, out ResultResponse resultResponse);

            PrintTestVehcileProfilesV5(vehicleProfiles, resultResponse);
        }
    }
}