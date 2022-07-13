using System;
using System.Collections.Generic;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using NUnit.Framework;

namespace Route4MeSdkV5UnitTest.V5.Vehicles
{
    [TestFixture]
    public class VehicleCapacityProfiles
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        
        public bool skip { get; set; } = false;
        public List<VehicleCapacityProfile> lsCreatedVehicleCapacityProfiles { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            #region Create Testing Vehicle Capacity Profiles

            lsCreatedVehicleCapacityProfiles = new List<VehicleCapacityProfile>();

            var vehicleCapacityProfileParams1 = new VehicleCapacityProfile()
            {
                Name = "VCPROF_" + R4MeUtils.GenerateRandomString(5),
                MaxVolume = 280,
                MaxWeight = 12.1,
                MaxItemsNumber = 150,
                MaxRevenue = 200.1,
                MaxVolumeUnit = "m3",
                MaxWeightUnit = "kg"
            };

            var vehicleCapacityProfile1 = route4Me.CreateVehicleCapacityProfile(vehicleCapacityProfileParams1, out ResultResponse resultResponse);

            if (vehicleCapacityProfile1 != null && vehicleCapacityProfile1.GetType() == typeof(VehicleCapacityProfileResponse))
            {
                lsCreatedVehicleCapacityProfiles.Add(vehicleCapacityProfile1.Data);
            }
            else
            {
                skip = true;
                return;
            }

            var vehicleCapacityProfileParams2 = new VehicleCapacityProfile()
            {
                Name = "VCPROF_" + R4MeUtils.GenerateRandomString(5),
                MaxVolume = 275,
                MaxWeight = 11.5,
                MaxItemsNumber = 145,
                MaxRevenue = 3101,
                MaxVolumeUnit = "m3",
                MaxWeightUnit = "kg"
            };

            var vehicleCapacityProfile2 = route4Me.CreateVehicleCapacityProfile(vehicleCapacityProfileParams2, out resultResponse);

            if (vehicleCapacityProfile2 != null && vehicleCapacityProfile2.GetType() == typeof(VehicleCapacityProfileResponse))
            {
                lsCreatedVehicleCapacityProfiles.Add(vehicleCapacityProfile2.Data);
            }
            else
            {
                skip = true;
                return;
            }

            #endregion

            if (lsCreatedVehicleCapacityProfiles.Count != 2)
            {
                Console.WriteLine("Cannot create 2 initial test vehicle capacity profiles. Some tests will be skipped.");
                skip = true;
            }
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsCreatedVehicleCapacityProfiles.Count > 0)
            {
                foreach (var vehicleCapacityProfile in lsCreatedVehicleCapacityProfiles)
                {

                    var capacityProfileParams = new VehicleCapacityProfileParameters()
                    {
                        VehicleCapacityProfileId = vehicleCapacityProfile.VehicleCapacityProfileId
                    };

                    var removed = route4Me.DeleteVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

                    Console.WriteLine(
                        (removed != null)
                        ? $"The vehicle capacity profile {vehicleCapacityProfile.VehicleCapacityProfileId} removed"
                        : $"Cannot remove the vehicle capacity profile {vehicleCapacityProfile.VehicleCapacityProfileId}"
                     );
                }
            }
        }

        [Test, Order(1)]
        public void CreateVehicleCapacityProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

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

            var vehicleCapacityProfile = route4me.CreateVehicleCapacityProfile(vehicleCapacityProfileParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicleCapacityProfile);
            Assert.That(vehicleCapacityProfile.GetType(), Is.EqualTo(typeof(VehicleCapacityProfileResponse)));

            lsCreatedVehicleCapacityProfiles.Add(vehicleCapacityProfile.Data);

        }

        [Test, Order(2)]
        public void GetVehicleCapacityProfilesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                Page = 1,
                PerPage = 10,
                MergePage = false
            };

            var profiles = route4me.GetVehicleCapacityProfiles(capacityProfileParams, out ResultResponse resultResponse);

            Assert.NotNull(profiles);
            Assert.That(profiles.GetType(), Is.EqualTo(typeof(VehicleCapacityProfilesResponse)));

        }

        [Test, Order(3)]
        public void GetVehicleCapacityProfileByIdTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                VehicleCapacityProfileId = lsCreatedVehicleCapacityProfiles[0].VehicleCapacityProfileId
            };

            var capacityProfile = route4me.GetVehicleCapacityProfileById(capacityProfileParams, out ResultResponse resultResponse);

            Assert.NotNull(capacityProfile);
            Assert.That(capacityProfile.GetType(), Is.EqualTo(typeof(VehicleCapacityProfileResponse)));

        }

        [Test, Order(4)]
        public void UpdateCapacityVehicleProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfile()
            {
                VehicleCapacityProfileId = lsCreatedVehicleCapacityProfiles[0].VehicleCapacityProfileId,
                MaxVolume = 279,
                MaxWeight = 12.0,
                MaxItemsNumber = 145
            };

            var capacityProfile = route4me.UpdateVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

            Assert.NotNull(capacityProfile);
            Assert.That(capacityProfile.GetType(), Is.EqualTo(typeof(VehicleCapacityProfileResponse)));

        }

        [Test, Order(5)]
        public void DeleteVehicleCapacityProfileTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var capacityProfileParams = new VehicleCapacityProfileParameters()
            {
                VehicleCapacityProfileId = lsCreatedVehicleCapacityProfiles[1].VehicleCapacityProfileId
            };

            var result = route4me.DeleteVehicleCapacityProfile(capacityProfileParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(VehicleCapacityProfileResponse)));
        }

    }
}
