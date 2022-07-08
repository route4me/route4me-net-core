using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a vehicle profile 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void UpdateVehicleProfileV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var profileParams = new VehicleProfile()
            {
                VehicleProfileId = 123107,
                Weight = 9999,
                MaxWeightPerAxle = 11999
            };

            var result = await route4Me.UpdateVehicleProfileAsync(profileParams);

            PrintTestVehcileProfilesV5(result.Item1, result.Item2);
        }
    }
}
