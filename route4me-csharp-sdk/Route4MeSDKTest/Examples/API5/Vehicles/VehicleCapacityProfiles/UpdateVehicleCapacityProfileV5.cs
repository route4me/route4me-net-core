using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle capacity profile using the API 5 endpoint.
        /// </summary>
        public void UpdateVehicleCapacityProfileV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfile()
            {
                VehicleCapacityProfileId = 566,
                MaxVolume = 279,
                MaxWeight = 12.0,
                MaxItemsNumber = 145
            };

            var capacityProfile = route4Me.UpdateVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

            PrintTestVehcileCapacityProfilesV5(capacityProfile, resultResponse);
        }
    }
}
