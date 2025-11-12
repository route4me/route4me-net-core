using System.Collections.Generic;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle profile 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void CreateVehicleProfileV5Async()
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
                VehicleProfileCode = R4MeUtils.GenerateRandomString(5)
            };

            var result = await route4Me.CreateVehicleProfileAsync(vehicleProfileParams1);

            if (result.Item1 != null && result.Item1.GetType() == typeof(VehicleProfile))
            {
                vehicleProfilesToRemove = new List<int>();
                vehicleProfilesToRemove.Add((int)result.Item1.VehicleProfileId);
            }

            PrintTestVehcileProfilesV5(result.Item1, result.Item2);

            RemoveTestVehicleProfilesV5();
        }
    }
}