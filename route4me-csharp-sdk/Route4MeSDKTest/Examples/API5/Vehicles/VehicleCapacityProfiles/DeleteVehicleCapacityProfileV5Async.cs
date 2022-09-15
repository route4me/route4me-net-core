using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of removing a vehicle capacity profile 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void DeleteVehicleCapacityProfileV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                VehicleCapacityProfileId = 626
            };

            var result = await route4Me.DeleteVehicleCapacityProfileAsync(capacityProfileParams);

            PrintTestVehcileCapacityProfilesV5(result.Item1, result.Item2);
        }
    }
}