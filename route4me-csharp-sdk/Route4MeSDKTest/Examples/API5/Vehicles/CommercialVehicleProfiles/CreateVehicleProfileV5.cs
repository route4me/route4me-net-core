using Route4MeSDK.DataTypes.V5;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle profile using the API 5 endpoint.
        /// </summary>
        public void CreateVehicleProfileV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleProfileParams1 = new VehicleProfile()
            {
                Name = "VPROF_" + R4MeUtils.GenerateRandomString(5),
                Height = 2.3,
                Width = 2.4,
                Length = 6,
                Weight = 9050,
                MaxWeightPerAxle = 11050,
                FuelConsumptionCity = 11,
                FuelConsumptionHighway = 5,
                IsPredefined = false,
                IsDefault = false,
                HeightUnit = "m",
                WidthUnit = "m",
                LengthUnit = "m",
                WeightUnit = "lb",
                MaxWeightPerAxleUnit = "lb",
                FuelConsumptionCityUnit = "mpg",
                FuelConsumptionHighwayUnit = "mpg",
                HeightUfValue = "3 m",
                WidthUfValue = "5'",
                LengthUfValue = "10'7''",
                WeightUfValue = "26,000lb 3oz",
                MaxWeightPerAxleUfValue = "900lb",
                FuelConsumptionCityUfValue = "20.01 mi/l",
                FuelConsumptionHighwayUfValue = "2,000.01 mpg",
                VehicleProfileCode = "VPC853"
            };

            var vehicleProfile = route4Me.CreateVehicleProfile(vehicleProfileParams1, out ResultResponse resultResponse);

            if (vehicleProfile != null && vehicleProfile.GetType() == typeof(VehicleProfile))
            {
                vehicleProfilesToRemove = new List<int>();
                vehicleProfilesToRemove.Add((int)vehicleProfile.VehicleProfileId);
            }

            PrintTestVehcileProfilesV5(vehicleProfile, resultResponse);

            //RemoveTestVehicleProfilesV5();


        }
    }
}
