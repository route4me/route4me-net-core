using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle profile by ID 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehicleProfileByIdV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                VehicleProfileId = 123107
            };

            var result = await route4Me.GetVehicleProfileByIdAsync(profileParams);

            PrintTestVehcileProfilesV5(result.Item1, result.Item2);
        }
    }
}
