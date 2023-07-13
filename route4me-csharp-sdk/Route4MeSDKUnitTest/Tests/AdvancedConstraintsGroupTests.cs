using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;
using Address = Route4MeSDK.DataTypes.Address;
using AlgorithmType = Route4MeSDK.DataTypes.AlgorithmType;
using DeviceType = Route4MeSDK.DataTypes.DeviceType;
using DistanceUnit = Route4MeSDK.DataTypes.DistanceUnit;
using Metric = Route4MeSDK.DataTypes.Metric;
using Optimize = Route4MeSDK.DataTypes.Optimize;
using RouteParameters = Route4MeSDK.DataTypes.RouteParameters;
using TravelMode = Route4MeSDK.DataTypes.TravelMode;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AdvancedConstraintsGroupTests
    {
        private static string skip;

        private static readonly string
            c_ApiKey = ApiKeys
                .ActualApiKey;

        private static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        private readonly List<string> _createdOptimizationIDs = new List<string>();
        private int? _rootMemberId;
        private List<int> _driverIDs;

        [OneTimeSetUp]
        public void AdvancedConstraintsGroupInitialize()
        {
            skip = c_ApiKey_1 == c_ApiKey ? "yes" : "no";

            var route4Me = new Route4MeManager(c_ApiKey);

            var users = route4Me.GetUsers(new GenericParameters(), out _);

            if ((users?.Results.Length ?? 0) > 0)
            {
                _rootMemberId = Convert.ToInt32(users.Results[0].MemberId);
            }

            _driverIDs = new List<int>();

            foreach (var user in users.Results)
            {
                if (user.MemberType == Route4MeSDK.DataTypes.V5.MemberTypes.Driver.Description())
                {
                    _driverIDs.Add(Convert.ToInt32(user.MemberId));
                    if (_driverIDs.Count > 2) break;
                }
            }
        }

        /// <summary>
        /// TEST CASE: Tags and Different Time Windows Fleets
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample1()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Fleet Example - Single Depot, Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var tags1 = new List<string>() { "TAG001", "TAG002" };
            var tags2 = new List<string>() { "TAG003" };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                Tags = tags1.ToArray(),
                AvailableTimeWindows = new List<int[]> { new int[] { 25200, 75000 } }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                Tags = tags2.ToArray(),
                AvailableTimeWindows = new List<int[]> { new int[] { 45200, 95000 } }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    Tags = tags2.ToArray()
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);

            Assert.IsTrue(
                (response?.Routes?.Length ?? 0) > 0,
                $"AdvancedConstraintsExample1 failed. {errorString}");
        }

        /// <summary>
        /// TEST CASE: Some addresses without Tags
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample2()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Fleet Example 2 - Single Depot, Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var tags1 = new List<string>() { "TAG001", "TAG002" };
            var tags2 = new List<string>() { "TAG003" };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                Tags = tags1.ToArray(),
                AvailableTimeWindows = new List<int[]> { new int[] { 25200, 75000 } }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 500,
                MembersCount = 6,
                Tags = tags2.ToArray(),
                AvailableTimeWindows = new List<int[]> { new int[] { 45200, 95000 } }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);

            Assert.IsTrue(
                (response?.Routes?.Length ?? 0) > 0,
                $"AdvancedConstraintsExample1 failed. {errorString}");
        }

        /// <summary>
        /// TEST CASE: Driver's Shift
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample3()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Driver's Shift Example - Single Depot, Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var tags1 = new List<string>() { "TAG001", "TAG002" };
            var tags2 = new List<string>() { "TAG003" };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                Tags = tags1.ToArray(),
                AvailableTimeWindows = new List<int[]> { new int[] { 25200, 75000 } }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 500,
                MembersCount = 6,
                Tags = tags2.ToArray(),
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 55000 },
                    new int[] { 62000, 85000 }
                }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Tags = tags1.ToArray()
                },

                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Tags = tags1.ToArray()
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = tags2.ToArray()
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = tags2.ToArray()
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);

            Assert.IsTrue(
                (response?.Routes?.Length ?? 0) > 0,
                $"AdvancedConstraintsExample1 failed. {errorString}");
        }

        /// <summary>
        /// TEST CASE: Driver's Skills
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample4()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Automatic Driver's Skills Example - Single Depot, Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var skills1 = new List<string>() { "Class A CDL" };
            var skills2 = new List<string>() { "Class B CDL" };

            var route4meMembersID = _driverIDs.Count > 0
                ? _driverIDs.ToArray()
                : new int[] { (int)_rootMemberId };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                Route4meMembersId = route4meMembersID,
                AvailableTimeWindows = new List<int[]> { new int[] { 25200, 75000 } }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 500,
                Route4meMembersId = route4meMembersID,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 85000 }
                }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Tags = skills1.ToArray()
                },

                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = skills2.ToArray()
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Drivers Schedules with Territories
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample5()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                RT = true,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zone1 = new List<string>() { "ZONE 01" };
            var zone2 = new List<string>() { "ZONE 02" };
            var zone3 = new List<string>() { "ZONE 03" };

            var route4meMembersID = new List<int>() { 1459842, 1481916, 1481918 };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                Tags = zone1.ToArray(),
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (11 + 5) * 3600 } }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                Tags = zone2.ToArray(),
                MembersCount = 4,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (12 + 5) * 3600 } }
            };

            var advancedConstraints3 = new RouteAdvancedConstraints()
            {
                Tags = zone3.ToArray(),
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (13 + 5) * 3600 } }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2,
                advancedConstraints3
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            int serviceTime = 300;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "2158",
                    IsDepot = false,
                    Latitude = 25.603049,
                    Longitude = -80.348022,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "253",
                    IsDepot = false,
                    Latitude = 25.618737,
                    Longitude = -80.329138,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1527",
                    IsDepot = false,
                    Latitude = 25.660645,
                    Longitude = -80.284289,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1889",
                    IsDepot = false,
                    Latitude = 25.816771,
                    Longitude = -80.265282,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3998",
                    IsDepot = false,
                    Latitude = 25.830834,
                    Longitude = -80.336474,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1988",
                    IsDepot = false,
                    Latitude = 25.934509,
                    Longitude = -80.216283,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3566",
                    IsDepot = false,
                    Latitude = 25.826221,
                    Longitude = -80.247753,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2659",
                    IsDepot = false,
                    Latitude = 25.60218,
                    Longitude = -80.384538,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2477",
                    IsDepot = false,
                    Latitude = 25.679245,
                    Longitude = -80.281254,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3968",
                    IsDepot = false,
                    Latitude = 25.655636,
                    Longitude = -80.350484,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1700",
                    IsDepot = false,
                    Latitude = 25.871786,
                    Longitude = -80.341298,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1804",
                    IsDepot = false,
                    Latitude = 25.690688,
                    Longitude = -80.318216,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "209",
                    IsDepot = false,
                    Latitude = 25.893571,
                    Longitude = -80.20119,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "834",
                    IsDepot = false,
                    Latitude = 25.951618,
                    Longitude = -80.29993,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1530",
                    IsDepot = false,
                    Latitude = 25.818694,
                    Longitude = -80.354931,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1696",
                    IsDepot = false,
                    Latitude = 25.748019,
                    Longitude = -80.243968,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1596",
                    IsDepot = false,
                    Latitude = 25.834085,
                    Longitude = -80.193554,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3563",
                    IsDepot = false,
                    Latitude = 25.690451,
                    Longitude = -80.272227,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3622",
                    IsDepot = false,
                    Latitude = 25.602187,
                    Longitude = -80.41193,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1805",
                    IsDepot = false,
                    Latitude = 25.780564,
                    Longitude = -80.415264,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1655",
                    IsDepot = false,
                    Latitude = 25.779567,
                    Longitude = -80.356258,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1533",
                    IsDepot = false,
                    Latitude = 25.459839,
                    Longitude = -80.44416,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "269",
                    IsDepot = false,
                    Latitude = 25.777716,
                    Longitude = -80.25451,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1238",
                    IsDepot = false,
                    Latitude = 25.821602,
                    Longitude = -80.12694,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3312",
                    IsDepot = false,
                    Latitude = 25.894716,
                    Longitude = -80.33056,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3989",
                    IsDepot = false,
                    Latitude = 25.553594,
                    Longitude = -80.374832,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "457",
                    IsDepot = false,
                    Latitude = 25.636562,
                    Longitude = -80.451262,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3105",
                    IsDepot = false,
                    Latitude = 25.737308,
                    Longitude = -80.43438,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3317",
                    IsDepot = false,
                    Latitude = 25.752353,
                    Longitude = -80.215284,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3589",
                    IsDepot = false,
                    Latitude = 25.877066,
                    Longitude = -80.22757,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3313",
                    IsDepot = false,
                    Latitude = 25.93445,
                    Longitude = -80.257547,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "385",
                    IsDepot = false,
                    Latitude = 25.902516,
                    Longitude = -80.254678,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2601",
                    IsDepot = false,
                    Latitude = 25.85515,
                    Longitude = -80.219475,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "602",
                    IsDepot = false,
                    Latitude = 25.513304,
                    Longitude = -80.387233,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2710",
                    IsDepot = false,
                    Latitude = 25.626475,
                    Longitude = -80.428484,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1187",
                    IsDepot = false,
                    Latitude = 25.781259,
                    Longitude = -80.402599,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2155",
                    IsDepot = false,
                    Latitude = 25.760206,
                    Longitude = -80.330144,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "990",
                    IsDepot = false,
                    Latitude = 25.9182,
                    Longitude = -80.352967,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2810",
                    IsDepot = false,
                    Latitude = 25.763907,
                    Longitude = -80.293502,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3294",
                    IsDepot = false,
                    Latitude = 25.576745,
                    Longitude = -80.380201,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3578",
                    IsDepot = false,
                    Latitude = 25.441741,
                    Longitude = -80.454027,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1040",
                    IsDepot = false,
                    Latitude = 25.741545,
                    Longitude = -80.320633,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2184",
                    IsDepot = false,
                    Latitude = 25.769002,
                    Longitude = -80.404676,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "842",
                    IsDepot = false,
                    Latitude = 25.705431,
                    Longitude = -80.398938,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1668",
                    IsDepot = false,
                    Latitude = 25.660366,
                    Longitude = -80.376896,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2603",
                    IsDepot = false,
                    Latitude = 25.660645,
                    Longitude = -80.284289,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1304",
                    IsDepot = false,
                    Latitude = 25.935256,
                    Longitude = -80.176192,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3281",
                    IsDepot = false,
                    Latitude = 25.962562,
                    Longitude = -80.250286,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2855",
                    IsDepot = false,
                    Latitude = 25.781819,
                    Longitude = -80.235649,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2518",
                    IsDepot = false,
                    Latitude = 25.632515,
                    Longitude = -80.368998,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "46",
                    IsDepot = false,
                    Latitude = 25.741641,
                    Longitude = -80.221332,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3185",
                    IsDepot = false,
                    Latitude = 25.945872,
                    Longitude = -80.310623,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3309",
                    IsDepot = false,
                    Latitude = 25.761921,
                    Longitude = -80.368253,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2586",
                    IsDepot = false,
                    Latitude = 25.792323,
                    Longitude = -80.336024,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "237",
                    IsDepot = false,
                    Latitude = 25.749872,
                    Longitude = -80.393815,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2192",
                    IsDepot = false,
                    Latitude = 25.94228,
                    Longitude = -80.174436,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2887",
                    IsDepot = false,
                    Latitude = 25.753024,
                    Longitude = -80.232491,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3485",
                    IsDepot = false,
                    Latitude = 25.547749,
                    Longitude = -80.375777,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3832",
                    IsDepot = false,
                    Latitude = 25.489671,
                    Longitude = -80.419657,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1393",
                    IsDepot = false,
                    Latitude = 25.872401,
                    Longitude = -80.295227,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "781",
                    IsDepot = false,
                    Latitude = 25.912158,
                    Longitude = -80.204096,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2165",
                    IsDepot = false,
                    Latitude = 25.745813,
                    Longitude = -80.275891,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "537",
                    IsDepot = false,
                    Latitude = 25.843267,
                    Longitude = -80.373141,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "235",
                    IsDepot = false,
                    Latitude = 25.877239,
                    Longitude = -80.222824,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1175",
                    IsDepot = false,
                    Latitude = 25.924446,
                    Longitude = -80.162018,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2235",
                    IsDepot = false,
                    Latitude = 25.850434,
                    Longitude = -80.183362,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2775",
                    IsDepot = false,
                    Latitude = 25.647769,
                    Longitude = -80.410684,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1556",
                    IsDepot = false,
                    Latitude = 25.457798,
                    Longitude = -80.483813,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3233",
                    IsDepot = false,
                    Latitude = 25.593026,
                    Longitude = -80.382412,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3534",
                    IsDepot = false,
                    Latitude = 25.867923,
                    Longitude = -80.24087,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3205",
                    IsDepot = false,
                    Latitude = 25.656392,
                    Longitude = -80.291358,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2893",
                    IsDepot = false,
                    Latitude = 25.867024,
                    Longitude = -80.201303,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1555",
                    IsDepot = false,
                    Latitude = 25.776622,
                    Longitude = -80.415111,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3218",
                    IsDepot = false,
                    Latitude = 25.832436,
                    Longitude = -80.280374,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "899",
                    IsDepot = false,
                    Latitude = 25.855764,
                    Longitude = -80.187256,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1027",
                    IsDepot = false,
                    Latitude = 25.735087,
                    Longitude = -80.259917,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3448",
                    IsDepot = false,
                    Latitude = 25.84728,
                    Longitude = -80.266024,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "884",
                    IsDepot = false,
                    Latitude = 25.480335,
                    Longitude = -80.458004,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "448",
                    IsDepot = false,
                    Latitude = 25.684473,
                    Longitude = -80.451831,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3643",
                    IsDepot = false,
                    Latitude = 25.677524,
                    Longitude = -80.425454,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1952",
                    IsDepot = false,
                    Latitude = 25.754493,
                    Longitude = -80.342664,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3507",
                    IsDepot = false,
                    Latitude = 25.874399,
                    Longitude = -80.345727,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3520",
                    IsDepot = false,
                    Latitude = 25.483806,
                    Longitude = -80.428498,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1611",
                    IsDepot = false,
                    Latitude = 25.713302,
                    Longitude = -80.440269,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1402",
                    IsDepot = false,
                    Latitude = 25.72308,
                    Longitude = -80.444812,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1211",
                    IsDepot = false,
                    Latitude = 25.699226,
                    Longitude = -80.422237,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1161",
                    IsDepot = false,
                    Latitude = 25.835215,
                    Longitude = -80.252216,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1274",
                    IsDepot = false,
                    Latitude = 25.888309,
                    Longitude = -80.344764,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1341",
                    IsDepot = false,
                    Latitude = 25.716966,
                    Longitude = -80.438179,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2946",
                    IsDepot = false,
                    Latitude = 25.530972,
                    Longitude = -80.448924,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "813",
                    IsDepot = false,
                    Latitude = 25.488095,
                    Longitude = -80.450334,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3862",
                    IsDepot = false,
                    Latitude = 25.954786,
                    Longitude = -80.16335,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "759",
                    IsDepot = false,
                    Latitude = 25.555122,
                    Longitude = -80.335284,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "52",
                    IsDepot = false,
                    Latitude = 25.927916,
                    Longitude = -80.268189,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "909",
                    IsDepot = false,
                    Latitude = 25.832815,
                    Longitude = -80.217156,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2768",
                    IsDepot = false,
                    Latitude = 25.835259,
                    Longitude = -80.223997,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3967",
                    IsDepot = false,
                    Latitude = 25.630732,
                    Longitude = -80.366065,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1974",
                    IsDepot = false,
                    Latitude = 25.931024,
                    Longitude = -80.217991,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3147",
                    IsDepot = false,
                    Latitude = 25.921095,
                    Longitude = -80.261839,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE:  Drivers Schedules with Territories
        /// </summary>
        [Test]
        public async Task AdvancedConstraintsExample6()
        {
            // Route generation with more than 1000 addresses needs special permission.
            //if (2 > 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RT = true,
                Parts = 30,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zone1 = new List<string>() { "ZONE 01" };
            var zone2 = new List<string>() { "ZONE 02" };
            var zone3 = new List<string>() { "ZONE 03" };

            RouteAdvancedConstraints schedule;

            var zone = new List<string>();

            #region Advanced Constraints

            var advancedConstraints = new List<RouteAdvancedConstraints>();

            for (int i = 0; i < 30; i++)
            {
                schedule = new RouteAdvancedConstraints();
                switch (i % 3)
                {
                    case 0:
                        zone = zone1;
                        break;
                    case 1:
                        zone = zone2;
                        break;
                    case 2:
                        zone = zone3;
                        break;
                }

                schedule.Tags = zone.ToArray();
                schedule.MembersCount = 1;
                schedule.AvailableTimeWindows = new List<int[]>
                {
                    new int[] { (8 + 5) * 3600, (11 + 5) * 3600 }
                };

                advancedConstraints.Add(schedule);
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            #region Prepare Addresses

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"Data/CSV/locations_1999.csv";

             var addresses = new List<Address>();

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);
                        int igroup = int.TryParse(group, out int _) ? int.Parse(group) : -1;
                        igroup = igroup >= 0 && igroup <= 2 ? igroup : -1;



                        var curAddress = new Address()
                        {
                            AddressString = address,
                            Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                            Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                            Group = group
                        };

                        if (igroup > -1) curAddress.Tags = new string[] { "ZONE 0"+(igroup+1) };

                        addresses.Add(curAddress);
                    }
                }
            }

            Address depot = new Address()
            {
                AddressString = "Depot",
                IsDepot = true,
                Latitude = 25.723025,
                Longitude = -80.452883,
                Time = 0
            };

            addresses.Add(depot);

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            //var response = route4Me.RunOptimization(optimizationParameters, out string errorString);
            var response = await route4Me.RunOptimizationAsync(optimizationParameters);

            Assert.IsTrue((response?.Item1?.State ?? 0) > 0,
                $"AdvancedConstraintsExample6 failed. {Environment.NewLine}" +
                $"OptimizationErrors: {response.Item1.OptimizationErrors}{Environment.NewLine}" +
                $"UserErrors: {response.Item1.UserErrors}");

            _createdOptimizationIDs.Add(response.Item1.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Drivers Schedules with Territories
        /// </summary>
        [Test]
        public async Task AdvancedConstraintsExample7()
        {
            // Route generation with more than 1000 addresses needs special permission.
            if (2 > 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RT = true,
                Parts = 50,
                RouteName = "50 Drivers Schedules - 5 Zones",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zone1 = new List<string>() { "ZONE 01" };
            var zone2 = new List<string>() { "ZONE 02" };
            var zone3 = new List<string>() { "ZONE 03" };
            var zone4 = new List<string>() { "ZONE 04" };
            var zone5 = new List<string>() { "ZONE 05" };

            RouteAdvancedConstraints schedule;

            var zone = new List<string>();

            #region Advanced Constraints

            var advancedConstraints = new List<RouteAdvancedConstraints>();

            var sScheduleFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/schedule.csv";

            using (TextReader reader = File.OpenText(sScheduleFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var start = csv.GetField(0);
                        var end = csv.GetField(1);
                        var group = csv.GetField(2);

                        int igroup = int.TryParse(group.ToString(), out _) ? int.Parse(group.ToString()) : -1;

                        schedule = new RouteAdvancedConstraints();

                        switch (igroup)
                        {
                            case 0:
                                schedule.Tags = zone1.ToArray();
                                break;
                            case 1:
                                schedule.Tags = zone2.ToArray();
                                break;
                            case 2:
                                schedule.Tags = zone3.ToArray();
                                break;
                            case 3:
                                schedule.Tags = zone4.ToArray();
                                break;
                            case 4:
                                schedule.Tags = zone5.ToArray();
                                break;
                        }

                        int timeStart = int.TryParse(start.ToString(), out _) ? int.Parse(start.ToString()) : -1;
                        int timeEnd = int.TryParse(end.ToString(), out _) ? int.Parse(end.ToString()) : -1;

                        schedule.MembersCount = 1;

                        schedule.AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { timeStart, timeEnd }
                        };

                        advancedConstraints.Add(schedule);
                    }
                }
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            #region Prepare Addresses

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations_1999.csv";

            var addresses = new List<Address>();

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Group = group
                            }
                        );
                    }
                }
            }

            Address depot = new Address()
            {
                AddressString = "Depot",
                IsDepot = true,
                Latitude = 25.694341,
                Longitude = -80.166036,
                Time = 0
            };

            addresses.Add(depot);

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = await route4Me.RunOptimizationAsync(optimizationParameters);

            Assert.IsTrue((response?.Item1?.State ?? 0) > 0,
                $"AdvancedConstraintsExample6 failed. {Environment.NewLine}" +
                $"OptimizationErrors: {response.Item1.OptimizationErrors}{Environment.NewLine}" +
                $"UserErrors: {response.Item1.UserErrors}");

            _createdOptimizationIDs.Add(response.Item1.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Drivers Schedules with Territories
        /// </summary>
        [Test]
        public async Task AdvancedConstraintsExample8()
        {
            // Route generation with more than 1000 addresses needs special permission.
            if (2 > 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RT = true,
                Parts = 50,
                RouteName = "50 Drivers Schedules",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            RouteAdvancedConstraints schedule;

            #region Advanced Constraints

            var advancedConstraints = new List<RouteAdvancedConstraints>();

            var sScheduleFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/schedule.csv";

            using (TextReader reader = File.OpenText(sScheduleFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var start = csv.GetField(0);
                        var end = csv.GetField(1);
                        var group = csv.GetField(2);

                        //int igroup = int.TryParse(group.ToString(), out _) ? int.Parse(group.ToString()) : -1;

                        schedule = new RouteAdvancedConstraints();

                        int timeStart = int.TryParse(start.ToString(), out _) ? int.Parse(start.ToString()) : -1;
                        int timeEnd = int.TryParse(end.ToString(), out _) ? int.Parse(end.ToString()) : -1;

                        schedule.MembersCount = 1;

                        schedule.AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { timeStart, timeEnd }
                        };

                        advancedConstraints.Add(schedule);
                    }
                }
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            #region Prepare Addresses

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations_1999.csv";

            var addresses = new List<Address>();
            int serviceTime = 120;

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Alias = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Time = serviceTime,
                                Group = group
                            }
                        );
                    }
                }
            }

            Address depot = new Address()
            {
                AddressString = "Depot",
                IsDepot = true,
                Latitude = 25.694341,
                Longitude = -80.166036,
                Time = 0
            };

            addresses.Add(depot);

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            //var response = route4Me.RunOptimization(optimizationParameters, out string errorString);
            var response = await route4Me.RunOptimizationAsync(optimizationParameters);

            Assert.IsTrue((response?.Item1?.State ?? 0) > 0,
                $"AdvancedConstraintsExample6 failed. {Environment.NewLine}" +
                $"OptimizationErrors: {response.Item1.OptimizationErrors}{Environment.NewLine}" +
                $"UserErrors: {response.Item1.UserErrors}");

            _createdOptimizationIDs.Add(response.Item1.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Retail Location based of address position id
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample9()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                MemberId = _rootMemberId,
                Metric = Metric.Matrix,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Retail Location - Single Depot - Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Depots = new object[]
                {
                    new Address()
                    {
                        AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                        Latitude = 38.141598,
                        Longitude = -85.793846
                    }
                }
            };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 25200, 75000 }
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        AddressString = "1407 MCCOY, Louisville, KY, 40215",
                        Latitude = 38.202496,
                        Longitude = -85.786514,
                        Time = 300
                    }
                }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 85000 }
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        AddressString = "1407 MCCOY, Louisville, KY, 40215",
                        Latitude = 38.202496,
                        Longitude = -85.786514,
                        Time = 300
                    }
                }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Retail Location based of address position id
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample10()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 0,
                Parts = 20,
                Metric = Metric.Matrix,
                MemberId = _rootMemberId,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Retail Location - Single Depot - Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            #region Advanced Constraints

            var advancedConstraints0 = new RouteAdvancedConstraints()
            {
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        AddressString = "1407 MCCOY, Louisville, KY, 40215",
                        Latitude = 38.202496,
                        Longitude = -85.786514,
                        Time = 300
                    }
                }
            };

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 25200, 75000 }
                }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 95000 }
                }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints0,
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
                //Depots = new Address[] { depot }
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Retail Location - setting the address in the advanced constraints
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample11()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RouteTime = 7 * 3600,
                RT = false,
                Metric = Metric.Matrix,
                MemberId = _rootMemberId,
                RouteName = "Retail Location - Single Depot - Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            #region Advanced Constraints

            var advancedConstraints1 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 25200, 75000 }
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                        Alias = "DEPOT END LOCATION",
                        Latitude = 38.179806,
                        Longitude = -85.775558,
                        Time = 300
                    }
                }
            };

            var advancedConstraints2 = new RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                MembersCount = 10,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 95000 }
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                        Alias = "DEPOT END LOCATION",
                        Latitude = 38.179806,
                        Longitude = -85.775558,
                        Time = 300
                    }
                }
            };

            var advancedConstraints = new RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Drivers Schedules with Territories and Retail Location
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample12()
        {
            if (skip == "yes") return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                StoreRoute = false,
                RT = true,
                Parts = 30,
                Metric = Metric.Matrix,
                MemberId = _rootMemberId,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zones = new Dictionary<int, List<string>>()
            {
                { 0, new List<string>() { "ZONE 01" } },
                { 1, new List<string>() { "ZONE 02" } },
                { 2, new List<string>() { "ZONE 03" } }
            };

            #region Advanced Constraints

            var advancedConstraints = new List<RouteAdvancedConstraints>();

            for (int i = 0; i < 30; i++)
            {
                advancedConstraints.Add(
                    new RouteAdvancedConstraints()
                    {
                        Tags = zones[i % 3].ToArray(),
                        MembersCount = 1,
                        AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (11 + 5) * 3600 } },
                        LocationSequencePattern = new object[]
                        {
                            "",
                            new Address()
                            {
                                AddressString = "RETAIL LOCATION",
                                Alias = "RETAIL LOCATION",
                                Latitude = 25.8741751,
                                Longitude = -80.1288583,
                                Time = 300
                            }
                        }
                    }
                );
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations.csv";

            #region Prepare Addresses

            int serviceTime = 300;

            var addresses = new List<Address>();

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);

                        int igroup = int.TryParse(group.ToString(), out _) ? Convert.ToInt32(group) : 0;

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Alias = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Group = group,
                                Time = serviceTime,
                                Tags = zones[igroup].ToArray()
                            }
                        );
                    }
                }
            }

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample1 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        /// <summary>
        /// TEST CASE: Drivers Schedules with Address from Territories created in Route4Me
        /// Note: This test needs an account with special permissions and the areas that cover hundreds of addresses.
        /// </summary>
        [Test]
        public void AdvancedConstraintsExample13()
        {
            //if (2 > 1) return; // Comment this line if you have an account with special permission

            var route4Me = new Route4MeManager(c_ApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = (8 + 5) * 3600,
                Metric = Metric.Matrix,
                MemberId = _rootMemberId,
                RouteName = "Single Depot, Multiple Driver - 3 Territories IDs",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
            };

            var depots = new Address[]
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Latitude = 38.141598,
                    Longitude = -85.793846
                }
            };

            routeParameters.Depots = depots;

            var territory1 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "A34BA30C717D1194FC0230252DF0C45C",
                    Addresses = 1
                },
                out _
            );

            var territory2 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "5D418084790C59C2D42B1C067094A459",
                    Addresses = 1
                },
                out _
            );

            var territory3 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "0B40480FD15C5F85A26AD759007834F4",
                    Addresses = 1
                },
                out _
            );

            #region Get Addresses from territories

            var addresses = new List<Address>();

            foreach (var addressID in territory1.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory1.TerritoryId }
                    }
                );
            }

            foreach (var addressID in territory2.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory2.TerritoryId }
                    }
                );
            }

            foreach (var addressID in territory3.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory3.TerritoryId }
                    }
                );
            }

            #endregion

            #region Advanced Constraints

            var advancedConstraints = new List<RouteAdvancedConstraints>()
            {
                new RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 3,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (11 + 5) * 3600 }
                    }
                },
                new RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 4,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (12 + 5) * 3600 }
                    }
                },
                new RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 3,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (13 + 5) * 3600 }
                    }
                }
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var response = route4Me.RunOptimization(optimizationParameters, out string errorString);

            Assert.IsTrue((response?.OptimizationProblemId?.Length ?? 0) == 32,
                $"AdvancedConstraintsExample13 failed. {errorString}");

            _createdOptimizationIDs.Add(response.OptimizationProblemId);
        }

        [OneTimeTearDown]
        public void AdvancedConstraintsGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.RemoveOptimization(_createdOptimizationIDs.ToArray(), out _);

            Assert.IsTrue(result, "Removing of the optimizations with advanced constraints failed.");
        }
    }
}