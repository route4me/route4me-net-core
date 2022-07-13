using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of gettting a vehicle capacity profile by ID 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehicleCapacityProfileByIdV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                VehicleCapacityProfileId = 626
            };

            var result = await route4Me.GetVehicleCapacityProfileByIdAsync(capacityProfileParams);

            PrintTestVehcileCapacityProfilesV5(result.Item1, result.Item2);
        }
    }
}
