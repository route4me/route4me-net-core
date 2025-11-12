using System;
using System.Collections.Generic;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.Vehicles
{
    [TestFixture]
    public class CommercialVehicleProfiles
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        public bool skip { get; set; } = false;
        public List<VehicleProfile> lsCreatedVehicleProfiles { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);
            Console.WriteLine("Start Setup");
            #region Create Testing Vehicle Profiles

            lsCreatedVehicleProfiles = new List<VehicleProfile>();

            var vehicleProfileParams1 = new VehicleProfile()
            {
                Name = "VPROF_" + R4MeUtils.GenerateRandomString(5),
                Height = 2.41,
                Width = 2.51,
                Length = 7,
                Weight = 10001,
                MaxWeightPerAxle = 12001,
                FuelConsumptionCity = 12,
                FuelConsumptionHighway = 6,
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
                VehicleProfileCode = R4MeUtils.GenerateRandomString(6)

            };

            var vehicleProfile1 = route4Me.CreateVehicleProfile(vehicleProfileParams1, out ResultResponse resultResponse);

            if (vehicleProfile1 != null && vehicleProfile1.GetType() == typeof(VehicleProfile))
            {
                lsCreatedVehicleProfiles.Add(vehicleProfile1);
            }
            else
            {
                skip = true;
                Console.WriteLine($"skip={skip}");
                return;
            }

            var vehicleProfileParams2 = new VehicleProfile()
            {
                Name = "VPROF_" + R4MeUtils.GenerateRandomString(5),
                Height = 2.36,
                Width = 2.46,
                Length = 6.6,
                Weight = 10051,
                MaxWeightPerAxle = 12051,
                FuelConsumptionCity = 12,
                FuelConsumptionHighway = 6,
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
                FuelConsumptionHighwayUfValue = "2,000.01 mpg"
            };

            var vehicleProfile2 = route4Me.CreateVehicleProfile(vehicleProfileParams2, out resultResponse);

            if (vehicleProfile2 != null && vehicleProfile2.GetType() == typeof(VehicleProfile))
            {
                lsCreatedVehicleProfiles.Add(vehicleProfile2);
            }
            else
            {
                skip = true;
                Console.WriteLine($"skip={skip}");
                return;
            }

            #endregion


            if (lsCreatedVehicleProfiles.Count != 2)
            {
                Console.WriteLine("Cannot create 2 initial test vehicle profiles. Some tests will be skipped.");
                Console.WriteLine($"skip={skip}");
                skip = true;

            }
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsCreatedVehicleProfiles.Count > 0)
            {
                foreach (var vehicleProfile in lsCreatedVehicleProfiles)
                {

                    var profileParams = new VehicleProfileParameters()
                    {
                        VehicleProfileId = vehicleProfile.VehicleProfileId
                    };

                    var removed = route4Me.DeleteVehicleProfile(profileParams, out ResultResponse resultResponse);

                    Console.WriteLine(
                        (removed != null)
                        ? $"The vehicle {vehicleProfile.VehicleProfileId} removed"
                        : $"Cannot remove the vehicle {vehicleProfile.VehicleProfileId}"
                     );
                }
            }
        }

        [Test, Order(1)]
        public void CreateVehicleProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

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
                FuelConsumptionHighwayUfValue = "2,000.01 mpg"
            };

            var vehicleProfile = route4me.CreateVehicleProfile(vehicleProfileParams1, out ResultResponse resultResponse);

            Assert.NotNull(vehicleProfile);
            Assert.That(vehicleProfile.GetType(), Is.EqualTo(typeof(VehicleProfile)));

            lsCreatedVehicleProfiles.Add(vehicleProfile);

        }

        [Test, Order(2)]
        public void GetVehicleProfilesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                WithPagination = true
            };

            var profiles = route4me.GetVehicleProfiles(profileParams, out ResultResponse resultResponse);

            Assert.NotNull(profiles);
            Assert.That(profiles.GetType(), Is.EqualTo(typeof(VehicleProfilesResponse)));

        }

        [Test, Order(3)]
        public void GetVehicleProfileByIdTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                VehicleProfileId = lsCreatedVehicleProfiles[0].VehicleProfileId
            };

            var profile = route4me.GetVehicleProfileById(profileParams, out ResultResponse resultResponse);

            Assert.NotNull(profile);
            Assert.That(profile.GetType(), Is.EqualTo(typeof(VehicleProfile)));

        }

        [Test, Order(4)]
        public void UpdateVehicleProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var profileParams = new VehicleProfile()
            {
                VehicleProfileId = lsCreatedVehicleProfiles[0].VehicleProfileId, // Required
                Weight = 9999,
                MaxWeightPerAxle = 11999
            };

            var profile = route4me.UpdateVehicleProfile(profileParams, out ResultResponse resultResponse);

            Assert.NotNull(profile);
            Assert.That(profile.GetType(), Is.EqualTo(typeof(VehicleProfile)));
        }

        [Test, Order(5)]
        public void DeleteVehicleProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var profileParams = new VehicleProfileParameters()
            {
                VehicleProfileId = lsCreatedVehicleProfiles[1].VehicleProfileId
            };

            var result = route4me.DeleteVehicleProfile(profileParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
        }

    }
}