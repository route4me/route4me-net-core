using System;
using System.Collections.Generic;
using System.Linq;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Route4MeSdkV5UnitTest.V5.Vehicles
{
    [TestFixture]
    public class VehicleBulkOperationTests
    {
        public string JobId { get; set; }
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        TeamResponse[] memebrs;
        public bool skip { get; set; } = false;
        public List<Vehicle> lsCreatedVehicles { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            JobId = "00000000000000000000000000000000";

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

            Vehicle vehicle1;
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

            Vehicle vehicle2;
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
            // ... clean up test data from the database ...
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

        [Test, Order(1)]
        public void DeactivateVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                lsCreatedVehicles[0].VehicleId,
                lsCreatedVehicles[1].VehicleId
            };

            var vehicles = route4me.DeactivateVehicles(vehicleIDs, out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
            Assert.That(vehicles.GetType(), Is.EqualTo(typeof(Vehicle[])));
        }

        [Test, Order(2)]
        public void ActivateVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                lsCreatedVehicles[0].VehicleId,
                lsCreatedVehicles[1].VehicleId
            };

            var vehicles = route4me.ActivateVehicles(vehicleIDs, out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
            Assert.That(vehicles.GetType(), Is.EqualTo(typeof(VehiclesResults)));
        }

        [Test, Order(3)]
        public async Task UpdateVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            Random rnd = new Random();
            var costNew1 = (double)rnd.Next(10000, 20000);
            var costNew2 = (double)rnd.Next(10000, 20000);

            var vehicleArray = new Route4MeSDK.DataTypes.V5.Vehicles()
            {
                VehicleArray = new Vehicle[]
                {
                    new Vehicle()
                    {
                        VehicleId = lsCreatedVehicles[0].VehicleId,
                        VehicleCostNew = costNew1
                    },
                    new Vehicle()
                    {
                        VehicleId = lsCreatedVehicles[1].VehicleId,
                        VehicleCostNew = costNew2
                    }
                }
            };


            var result = await route4me.UpdateVehiclesAsync(vehicleArray);

            JobId = result.Item3;

            Assert.True(Guid.TryParse(result.Item3, out _));
        }

        [Test, Order(4)]
        public async Task DeleteVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                lsCreatedVehicles[0].VehicleId,
                lsCreatedVehicles[1].VehicleId
            };

            var result = await route4me.DeleteVehiclesAsync(vehicleIDs);

            JobId = result.Item3;

            Assert.True(Guid.TryParse(result.Item3, out _));
        }

        [Test, Order(5)]
        public async Task RestoreVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                lsCreatedVehicles[0].VehicleId,
                lsCreatedVehicles[1].VehicleId
            };

            var result = await route4me.RestoreVehiclesAsync(vehicleIDs);

            JobId = result.Item3;

            Assert.True(Guid.TryParse(result.Item3, out _));
        }

        [Test, Order(6)]
        public void GetJobResultTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var result = route4me.GetVehicleJobResult(JobId, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(StatusResponse)));
        }

    }
}
