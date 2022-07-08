using Route4MeSDK.DataTypes.V5;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle capacity profile 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void CreateVehicleCapacityProfileV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleCapacityProfileParams = new VehicleCapacityProfile()
            {
                Name = "VCPROF_" + R4MeUtils.GenerateRandomString(5),
                MaxVolume = 280,
                MaxWeight = 12.1,
                MaxItemsNumber = 150,
                MaxRevenue = 200.1,
                MaxVolumeUnit = "m3",
                MaxWeightUnit = "kg"
            };

            var result = await route4Me.CreateVehicleCapacityProfileAsync(
                vehicleCapacityProfileParams);

            if (result.Item1 != null && result.Item1.GetType() == typeof(VehicleCapacityProfileResponse))
            {
                vehicleProfilesToRemove = new List<int>();
                vehicleProfilesToRemove.Add((int)result.Item1.Data.VehicleCapacityProfileId);
            }

            PrintTestVehcileCapacityProfilesV5(result.Item1, result.Item2);

            RemoveTestVehicleCapacityProfilesV5();
        }
    }
}
