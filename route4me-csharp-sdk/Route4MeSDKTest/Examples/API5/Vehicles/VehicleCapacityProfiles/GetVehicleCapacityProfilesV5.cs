using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of gettting a list of the vehicle capacity profiles using the API 5 endpoint.
        /// </summary>
        public void GetVehicleCapacityProfilesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                MergePage = false
            };

            var profiles = route4Me.GetVehicleCapacityProfiles(capacityProfileParams, out ResultResponse resultResponse);

            PrintTestVehcileCapacityProfilesV5(profiles, resultResponse);
        }
    }
}
