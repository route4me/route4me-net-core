using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of removing a vehicle capacity profile using the API 5 endpoint.
        /// </summary>
        public void DeleteVehicleCapacityProfileV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                VehicleCapacityProfileId = 566
            };

            var capacityProfile = route4Me.DeleteVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

            PrintTestVehcileCapacityProfilesV5(capacityProfile, resultResponse);
        }
    }
}
