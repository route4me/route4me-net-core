using System.Collections.Generic;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class VehiclesGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private List<string> _lsVehicleIDs;

        [OneTimeSetUp]
        [System.Obsolete]
        public void VehiclesGroupInitialize()
        {
            _lsVehicleIDs = new List<string>();

            var vehicleGroup = new VehiclesGroupTests();

            var vehicles = vehicleGroup.GetVehiclesList();

            if ((vehicles?.Length ?? 0) < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleName = "Ford Transit Test 6",
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = vehicleGroup.CreateVehicle(newVehicle);
                _lsVehicleIDs.Add(vehicle.VehicleGuid);
            }
            else
            {
                foreach (var veh1 in vehicles) _lsVehicleIDs.Add(veh1.VehicleId);
            }
        }

        [Test]
        [System.Obsolete]
        public void GetVehiclesListTest()
        {
            GetVehiclesList();
        }

        [System.Obsolete]
        public Vehicle[] GetVehiclesList()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParameters = new VehicleParameters
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            var vehicles = route4Me.GetVehicles(vehicleParameters, out var errorString);

            return vehicles;
        }

        [Test]
        [System.Obsolete]
        public void CreateVehicleTest()
        {
            // Create common vehicle
            var commonVehicleParams = new VehicleV4Parameters
            {
                VehicleName = "Ford Transit Test 6",
                VehicleAlias = "Ford Transit Test 6"
            };

            var commonVehicle = CreateVehicle(commonVehicleParams);

            if (commonVehicle != null && commonVehicle.GetType() == typeof(VehicleV4CreateResponse))
                _lsVehicleIDs.Add(commonVehicle.VehicleGuid);

            // Create a truck belonging to the class 6
            var class6TruckParams = new VehicleV4Parameters
            {
                VehicleName = "GMC TopKick C5500",
                VehicleAlias = "GMC TopKick C5500",
                VehicleVin = "SAJXA01A06FN08012",
                VehicleLicensePlate = "CVH4561",
                VehicleModel = "TopKick C5500",
                VehicleModelYear = 1995,
                VehicleYearAcquired = 2008,
                VehicleRegCountryId = 223,
                VehicleMake = "GMC",
                VehicleTypeID = "pickup_truck",
                VehicleAxleCount = 2,
                MpgCity = 7,
                MpgHighway = 14,
                FuelType = "diesel",
                HeightInches = 97,
                HeightMetric = 243,
                WeightLb = 19000,
                MaxWeightPerAxleGroupInPounds = 9500,
                MaxWeightPerAxleGroupMetric = 4300,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 244,
                LengthMetric = 610,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "NO",
                DividedHighwayAvoidPreference = "NEUTRAL",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "FULLSIZEVAN"
            };

            var class6Truck = CreateVehicle(class6TruckParams);

            if (class6Truck != null && class6Truck.GetType() == typeof(VehicleV4CreateResponse))
                _lsVehicleIDs.Add(class6Truck.VehicleGuid);

            // Create a truck belonging to the class 7
            var class7TruckParams = new VehicleV4Parameters
            {
                VehicleName = "FORD F750",
                VehicleAlias = "FORD F750",
                VehicleVin = "1NPAX6EX2YD550743",
                VehicleLicensePlate = "FFV9547",
                VehicleModel = "F-750",
                VehicleModelYear = 2010,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleTypeID = "livestock_carrier",
                VehicleAxleCount = 2,
                MpgCity = 8,
                MpgHighway = 15,
                FuelType = "diesel",
                HeightInches = 96,
                HeightMetric = 244,
                WeightLb = 26000,
                MaxWeightPerAxleGroupInPounds = 15000,
                MaxWeightPerAxleGroupMetric = 6800,
                WidthInInches = 96,
                WidthMetric = 240,
                LengthInInches = 312,
                LengthMetric = 793,
                Use53FootTrailerRouting = "NO",
                UseTruckRestrictions = "YES",
                DividedHighwayAvoidPreference = "FAVOR",
                FreewayAvoidPreference = "NEUTRAL",
                TruckConfig = "26_STRAIGHT_TRUCK",
                TollRoadUsage = "ALWAYS_AVOID",
                InternationalBordersOpen = "NO",
                PurchasedNew = true
            };

            var class7Truck = CreateVehicle(class7TruckParams);

            if (class7Truck != null && class7Truck.GetType() == typeof(VehicleV4CreateResponse))
                _lsVehicleIDs.Add(class7Truck.VehicleGuid);

            // Create a truck belonging to the class 8
            var class8TruckParams = new VehicleV4Parameters
            {
                VehicleName = "Peterbilt 579",
                VehicleAlias = "Peterbilt 579",
                VehicleVin = "1NP5DB9X93N507873",
                VehicleLicensePlate = "PPV7516",
                VehicleModel = "579",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Peterbilt",
                VehicleTypeID = "tractor_trailer",
                VehicleAxleCount = 4,
                MpgCity = 6,
                MpgHighway = 12,
                FuelType = "diesel",
                HeightInches = 114,
                HeightMetric = 290,
                WeightLb = 50350,
                MaxWeightPerAxleGroupInPounds = 40000,
                MaxWeightPerAxleGroupMetric = 18000,
                WidthInInches = 102,
                WidthMetric = 258,
                LengthInInches = 640,
                LengthMetric = 1625,
                Use53FootTrailerRouting = "YES",
                UseTruckRestrictions = "YES",
                DividedHighwayAvoidPreference = "STRONG_FAVOR",
                FreewayAvoidPreference = "STRONG_AVOID",
                TruckConfig = "53_SEMI_TRAILER",
                TollRoadUsage = "ALWAYS_AVOID",
                InternationalBordersOpen = "YES",
                PurchasedNew = true
            };

            var class8Truck = CreateVehicle(class8TruckParams);

            if (class8Truck != null && class8Truck.GetType() == typeof(VehicleV4CreateResponse))
                _lsVehicleIDs.Add(class8Truck.VehicleGuid);
        }

        [System.Obsolete]
        public VehicleV4CreateResponse CreateVehicle(VehicleV4Parameters vehicleParams)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var errorString = "";
            var result = route4Me.CreateVehicle(vehicleParams, out errorString);

            Assert.IsNotNull(result, "CreatetVehiclTest failed. " + errorString);

            return result;
        }

        [Test]
        [System.Obsolete]
        public void GetVehicleTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParameters = new VehicleParameters
            {
                VehicleId = _lsVehicleIDs[_lsVehicleIDs.Count - 1]
            };

            // Run the query
            var vehicles = route4Me.GetVehicle(vehicleParameters, out var errorString);

            Assert.That(vehicles, Is.InstanceOf<VehicleV4Response>(), "GetVehicleTest failed. " + errorString);
        }

        [Test]
        [System.Obsolete]
        public void UpdateVehicleTest()
        {
            if (c_ApiKey == ApiKeys.DemoApiKey) return;

            if (_lsVehicleIDs.Count < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = CreateVehicle(newVehicle);
                _lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var route4Me = new Route4MeManager(c_ApiKey);

            // TO DO: on this stage specifying of the parameter vehicle_alias is mandatory. 
            // Will be checked later
            var vehicleParams = new VehicleV4Parameters
            {
                VehicleModelYear = 1995,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                VehicleAxleCount = 2,
                FuelType = "unleaded 93",
                HeightInches = 72,
                WeightLb = 2000
            };

            // Run the query
            var vehicles = route4Me.UpdateVehicle(
                vehicleParams,
                _lsVehicleIDs[_lsVehicleIDs.Count - 1],
                out var errorString);

            Assert.That(vehicles, Is.InstanceOf<VehicleV4Response>(), "UpdateVehicleTest failed. " + errorString);
        }

        [Test]
        [System.Obsolete]
        public void DeleteVehicleTest()
        {
            if (_lsVehicleIDs.Count < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = CreateVehicle(newVehicle);
                _lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParams = new VehicleV4Parameters
            {
                VehicleId = _lsVehicleIDs[_lsVehicleIDs.Count - 1]
            };

            // Run the query
            var vehicles = route4Me.DeleteVehicle(vehicleParams, out var errorString);

            Assert.That(vehicles, Is.InstanceOf<VehicleV4Response>(), "DeleteVehicleTest failed. " + errorString);

            _lsVehicleIDs.RemoveAt(_lsVehicleIDs.Count - 1);
        }

        [OneTimeTearDown]
        [System.Obsolete]
        public void VehiclesGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var vehicleId in _lsVehicleIDs)
            {
                var vehicleParams = new VehicleV4Parameters
                {
                    VehicleId = vehicleId
                };

                // Run the query
                var vehicles = route4Me.DeleteVehicle(vehicleParams, out var errorString);
            }
        }
    }
}