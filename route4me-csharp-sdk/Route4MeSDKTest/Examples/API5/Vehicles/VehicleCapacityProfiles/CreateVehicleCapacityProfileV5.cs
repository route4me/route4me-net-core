using Route4MeSDK.DataTypes.V5;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle capacity profile using the API 5 endpoint.
        /// </summary>
        public void CreateVehicleCapacityProfileV5()
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

            var vehicleCapacityProfile = route4Me.CreateVehicleCapacityProfile(
                vehicleCapacityProfileParams,
                out ResultResponse resultResponse);

            if (vehicleCapacityProfile != null &&
                vehicleCapacityProfile.GetType() == typeof(VehicleCapacityProfileResponse))
            {
                vehicleProfilesToRemove = new List<int>();
                vehicleProfilesToRemove.Add((int)vehicleCapacityProfile.Data.VehicleCapacityProfileId);
            }

            PrintTestVehcileCapacityProfilesV5(vehicleCapacityProfile, resultResponse);

            RemoveTestVehicleCapacityProfilesV5();
        }
    }
}