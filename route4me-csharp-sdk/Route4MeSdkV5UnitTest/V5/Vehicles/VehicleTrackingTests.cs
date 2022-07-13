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
    public class VehicleTrackingTest
    {
        public string JobId { get; set; }
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        TeamResponse[] memebrs;
        public bool skip { get; set; } = false;
        public List<Vehicle> lsCreatedVehicles { get; set; }

        TestDataRepository tdr;

        [OneTimeSetUp]
        public void Setup()
        {
            JobId = "00000000000000000000000000000000";

            #region Create Test Vehicles

            var route4Me = new Route4MeManagerV5(c_ApiKey);

            tdr = new TestDataRepository();

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

            tdr.VehicleId = lsCreatedVehicles[0].VehicleId;

            tdr.RunSingleDriverRoundTrip();

            tdr.SetGpsPositionToRouteAndVehicle(
                tdr.SDRT_route,
                lsCreatedVehicles[0].VehicleId,
                out string errorString
                );

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

            var optimizationResult = tdr.RemoveOptimization(new string[] { tdr.SDRT_optimization_problem_id });
        }


        [Test, Order(1)]
        public void SyncPendingTelematicsDataTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                lsCreatedVehicles[0].VehicleId,
                lsCreatedVehicles[1].VehicleId
            };

            var vehicleSyncParams = new VehicleTelematicsSync()
            {
                VehicleAlias = "Commercial Vehicle Medium Duty 4",
                VehicleVin = "WBADE6322VBW51984",
                VehicleRegStateId = 4,
                VehicleRegCountryId = 223,
                VehicleLicensePlate = "CRL8474",
                VehicleTypeId = VehicleTypes.PICKUP_TRUCK.Description(),
                VehicleMake = VehicleMakes.Ford.Description(),
                VehicleModelYear = 2018,
                VehicleYearAcquired = 2020,
                FuelType = FuelTypes.UNLEADED_87.Description(),
                TelematicsGatewayCnnectionId = 1,
                TelematicsGatewayVehicleId = 2,
                ExternalTelematicsVehicleIDs = 2


            };

            var vehicle = route4me.SyncPendingTelematicsData(vehicleSyncParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicle);
            Assert.That(vehicle.GetType(), Is.EqualTo(typeof(Vehicle)));

        }

        [Test, Order(2)]
        public void SearchForNearesVehiclesTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new VehicleSearchParameters()
            {
                VehicleIDs = new string[]
                {
                    lsCreatedVehicles[0].VehicleId,
                    lsCreatedVehicles[1].VehicleId
                },
                Latitude = 40.777213,
                Longitude = -73.9669
            };

            var vehicles = route4me.SearchVehicles(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
            Assert.That(vehicles.GetType(), Is.EqualTo(typeof(Vehicle[])));
        }


        [Test, Order(3)]
        public void GetVehicleTrackingHistoryTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleParams = new VehicleParameters()
            {
                VehicleId = lsCreatedVehicles[0].VehicleId,
                Start = (DateTime.Now - new TimeSpan(60, 0, 0, 0)).ToString("yyyy-MM-dd"),
                End = (DateTime.Now + new TimeSpan(10, 0, 0, 0)).ToString("yyyy-MM-dd")
            };

            var result = route4me.GetVehicleTrack(vehicleParams, out ResultResponse resultResponse);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(VehicleTrackResponse)));
        }

        [Test, Order(4)]
        public void GetLatestVehicleLocationsTest()
        {
            if (skip) return;

            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var vehicleIDs = new string[]
            {
                    lsCreatedVehicles[0].VehicleId,
                    lsCreatedVehicles[1].VehicleId
            };

            var vehicles = route4me.GetVehicleLocations(vehicleIDs, out ResultResponse resultResponse);

            Assert.NotNull(vehicles);
            Assert.That(vehicles.GetType(), Is.EqualTo(typeof(VehicleLocationResponse)));
        }

    }
}
