using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle capacity profile 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void UpdateVehicleCapacityProfileV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfile()
            {
                VehicleCapacityProfileId = 626,
                MaxVolume = 279,
                MaxWeight = 12.0,
                MaxItemsNumber = 145
            };

            var result = await route4Me.UpdateVehicleCapacityProfileAsync(capacityProfileParams);

            PrintTestVehcileCapacityProfilesV5(result.Item1, result.Item2);
        }
    }
}