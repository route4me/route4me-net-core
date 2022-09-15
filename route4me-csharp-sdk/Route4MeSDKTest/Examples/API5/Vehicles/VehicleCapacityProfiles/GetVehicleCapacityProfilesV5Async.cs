using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of gettting a list of the vehicle capacity profiles 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehicleCapacityProfilesV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                MergePage = false
            };

            var result = await route4Me.GetVehicleCapacityProfilesAsync(capacityProfileParams);

            PrintTestVehcileCapacityProfilesV5(result.Item1, result.Item2);
        }
    }
}