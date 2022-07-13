using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a list of the vehicle profiles 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehicleProfilesV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                WithPagination = true
            };

            var result = await route4Me.GetVehicleProfilesAsync(profileParams);

            PrintTestVehcileProfilesV5(result.Item1, result.Item2);
        }
    }
}
