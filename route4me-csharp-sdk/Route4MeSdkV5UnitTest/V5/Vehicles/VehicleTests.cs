using System;
using System.Collections.Generic;
using System.Linq;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using NUnit.Framework;

namespace Route4MeSdkV5UnitTest.V5.Vehicles
{
    [TestFixture]
    public class VehicleTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static List<Vehicle> lsCreatedVehicles;

        bool skip = false;

        TeamResponse[] memebrs;

        [OneTimeSetUp]
        public void Setup()
        {
            #region Create Test Vehicles

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var tdr = new TestDataRepository();

            memebrs = tdr.GetTeamMembers(1, 0, c_ApiKey);

            if ((memebrs?.Length ?? 0) == 0)
            {
                skip = true;
                return;
            }

            lsCreatedVehicles = new List<Vehicle>();

            var restoreResult1 = tdr.RestoreTestVehicle("TopKick C5500 TST", c_ApiKey);

            Vehicle vehicle1 = null;
            ResultResponse result1 = null;

            if (restoreResult1 == null)
            {
                var vehicleParam1 = new Vehicle()
                {
                    VehicleAlias = "TopKick C5500 TST",
                    VehicleVin = "SAJXA01A06FN08012",
                    VehicleMake = "GMC",
                    VehicleModel = "TopKick C5500",
                    VehicleTypeId = "pickup_truck",
                    FuelType = "diesel",
                    VehicleLicensePlate = "CVH4561",
                    VehicleModelYear = 1995,
                    VehicleYearAcquired = 2008,
                    VehicleRegCountryId = 223
                };

                vehicle1 = route4Me.CreateVehicle(vehicleParam1, out result1);
            }
            else
            {
                vehicle1 = restoreResult1.Item1;
            }


            if ((vehicle1?.VehicleId ?? null) != null)
            {
                lsCreatedVehicles.Add(vehicle1);
            }
            else
            {
                Console.WriteLine($"Cannot create TopKick C5500 TST. Code={result1.Code}");
            }

            Vehicle vehicle2 = null;
            ResultResponse result2 = null;

            var restoreResult2 = tdr.RestoreTestVehicle("FORD F750 TST", c_ApiKey);

            if (restoreResult2 == null)
            {
                var vehicleParam2 = new Vehicle()
                {
                    VehicleAlias = "FORD F750 TST",
                    VehicleVin = "1NPAX6EX2YD550743",
                    VehicleLicensePlate = "FFV9547",
                    VehicleModel = "F-750",
                    VehicleModelYear = 2010,
                    VehicleYearAcquired = 2018,
                    VehicleRegCountryId = 223,
                    VehicleMake = "Ford",
                    VehicleTypeId = "livestock_carrier",
                    FuelType = "diesel"
                };

                vehicle2 = route4Me.CreateVehicle(vehicleParam2, out result2);
            }
            else
            {
                vehicle2 = restoreResult2.Item1;
            }

            if ((vehicle2?.VehicleId ?? null) != null)
            {
                lsCreatedVehicles.Add(vehicle2);
            }
            else
            {
                Console.WriteLine($"Cannot create TopKick C5500. Code={result2.Code}");
            }

            if (lsCreatedVehicles.Count != 2)
            {
                Console.WriteLine("Cannot create 2 initial test vehicles. Some tests will be skipped.");
                skip = true;

            }

            #endregion
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var lsRemLocations = new List<string>();

            if (lsCreatedVehicles.Count > 0)
            {
                var vehicleIDs = lsCreatedVehicles.Where(x => x != null && x.VehicleId != null)
                    .Select(x => x.VehicleId);

                foreach (var vehicleId in vehicleIDs)
                {
                    var removed = route4Me.DeleteVehicle(vehicleId, out ResultResponse resultResponse);

                    Console.WriteLine(
                        ((removed?.VehicleId ?? null) != null)
                        ? $"The vehicle {vehicleId} removed"
                        : $"Cannot remove the vehicle {vehicleId}"
                     );
                }
            }
        }

        [Test, Order(2)]
        public void ExecuteVehicleOrder()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new VehicleOrderParameters()
            {
                VehicleId = lsCreatedVehicles[0].VehicleId,
                Latitude = 40.777213,
                Longitude = -73.9669
            };

            var result = route4me.ExecuteVehicleOrder(vehicleParams, out ResultResponse resultREsonse);

            Assert.True((result?.OrderId ?? null) != null, "Cannot execute the vehicle order");
        }

        [Test, Order(1)]
        public void CreateVehicleTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new Vehicle()
            {
                VehicleAlias = "Peterbilt 579",
                VehicleVin = "1NP5DB9X93N507873",
                VehicleLicensePlate = "PPV7516",
                VehicleModel = "579",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Peterbilt",
                VehicleTypeId = "tractor_trailer",
                FuelType = "diesel"
            };

            var vehicle = route4me.CreateVehicle(vehicleParams, out ResultResponse resultResponse);

            if ((vehicle?.VehicleId ?? null) != null) lsCreatedVehicles.Add(vehicle);

            Assert.NotNull(vehicle);
        }

        [Test, Order(8)]
        public void UpdateVehicleTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new Vehicle()
            {
                VehicleId = lsCreatedVehicles[0].VehicleId, // Required
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018
            };

            var vehicle = route4me.UpdateVehicle(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicle);
            Assert.That(vehicle.GetType(), Is.EqualTo(typeof(Vehicle)));
            Assert.AreEqual(2015, (int)vehicle.VehicleModelYear);
            Assert.AreEqual(2018, (int)vehicle.VehicleYearAcquired);
        }

        [Test, Order(3)]
        public void AssignUserToVehicleTest()
        {
            if (skip) return;

            // The test requires an API key with special feature.

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new VehicleTemporary()
            {
                VehicleId = lsCreatedVehicles[0].VehicleId,
                ForceAssignment = false,
                AssignedMemberId = memebrs[0].MemberId.ToString(),
                ExpiresAt = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now + (new TimeSpan(2, 0, 0, 0))).ToString()
            };

            var result = route4me.CreateTemporaryVehicle(vehicleParams, out ResultResponse resultREsonse);

            Assert.True((result?.VehicleId ?? null) != null, "Cannot assign a member to the vehicle");
        }

        [Test, Order(4)]
        public void GetVehicelsByStateTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicles = route4me.GetVehiclesByState(VehicleStates.ACTIVE, out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
        }

        [Test, Order(5)]
        public void GetVehicelsByLicensePlateTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicles = route4me.GetVehicleByLicensePlate("FFV9547", out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
        }

        [Test, Order(6)]
        public void GetVehicleByIdTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicle = route4me.GetVehicleById(lsCreatedVehicles[0].VehicleId, out ResultResponse resultResponse);

            Assert.NotNull(vehicle);
            Assert.That(vehicle.GetType(), Is.EqualTo(typeof(Vehicle)));
            Assert.AreEqual(lsCreatedVehicles[0].VehicleId, vehicle.VehicleId);
        }

        [Test, Order(9)]
        public void DeleteVehicleTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicle = route4me.DeleteVehicle(lsCreatedVehicles[1].VehicleId, out ResultResponse resultResponse);

            Assert.NotNull(vehicle);
            Assert.That(vehicle.GetType(), Is.EqualTo(typeof(Vehicle)));

            lsCreatedVehicles.RemoveAt(1);
        }

        [Test, Order(7)]
        public void GetPaginatedVehicles()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new VehicleParameters()
            {
                Page = 1,
                PerPage = 10,
                FieldToOrderBy = "vehicle_alias",
                OrderDirection = "asc",
                Show = VehicleStates.ALL.Description(),
                SearchQuery = "TopKick C5500 TST"
            };

            var vehicleData = route4me.GetPaginatedVehiclesList(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicleData);
            Assert.That(vehicleData.GetType(), Is.EqualTo(typeof(VehiclesResponse)));
        }

        [Test, Order(8)]
        public void GetVehicles()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new GetVehicleParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var vehicleData = route4me.GetVehicles(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicleData);
            Assert.That(vehicleData.GetType(), Is.EqualTo(typeof(Vehicle[])));
        }

    }
}
