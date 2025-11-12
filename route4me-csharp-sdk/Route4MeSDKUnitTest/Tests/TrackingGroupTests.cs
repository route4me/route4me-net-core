using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class TrackingGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;

        [OneTimeSetUp]
        public void TrackingGroupInitialize()
        {
            _lsOptimizationIDs = new List<string>();

            _tdr = new TestDataRepository();

            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue
            (result,
                "Single Driver Round Trip generation failed.");

            Assert.IsTrue(
                _tdr.SDRT_route.Addresses.Length > 0,
                "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SDRT_optimization_problem_id);

            var recorded = SetAddressGPSPosition(_tdr.SDRT_route.Addresses[1]);

            Assert.IsTrue(recorded, "Cannot record GPS position of the address");

            recorded = SetAddressGPSPosition(_tdr.SDRT_route.Addresses[2]);

            Assert.IsTrue(recorded, "Cannot record GPS position of the address");
        }

        [Test]
        public void FindAssetTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var tracking = _tdr.SDRT_route != null
                ? _tdr.SDRT_route.Addresses.Length > 1
                    ? _tdr.SDRT_route.Addresses[1].TrackingNumber != null
                        ? _tdr.SDRT_route.Addresses[1].TrackingNumber
                        : ""
                    : ""
                : "";

            Assert.IsTrue(
                tracking != "",
                "Can not find valid tracking number in the newly generated route's second destination."
            );

            // Run the query
            var result = route4Me.FindAsset(tracking, out var errorString);

            Assert.That(result, Is.InstanceOf<FindAssetResponse>(), "FindAssetTest failed. " + errorString);
        }

        [Test]
        public void SetGPSPositionTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.14384;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.22466;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = _tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = (int)_tdr.SDRT_route.Addresses[1].MemberId,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = route4Me.SetGPS(gpsParameters, out var errorString);

            Assert.IsNotNull(
                response,
                "SetGPSPositionTest failed. " + errorString
            );
            Assert.IsTrue(
                response.Status,
                "SetGPSPositionTest failed. " + errorString
            );
        }

        private SetGpsResponse SetGpsPosition(out string errorString)
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.14384;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.22466;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = _tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = 725205,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return route4Me.SetGPS(gpsParameters, out errorString);
        }

        /// <summary>
        ///     Set GPS postion record by route address
        /// </summary>
        /// <param name="address">Route address</param>
        /// <returns>True if the GPS position recorded successfully.</returns>
        private bool SetAddressGPSPosition(Address address)
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var lat = address.Latitude;
            var lng = address.Longitude;

            // Create the gps parametes
            var gpsParameters = new GPSParameters
            {
                Format = Format.Csv.Description(),
                RouteId = _tdr.SDRT_route_id,
                Latitude = lat,
                Longitude = lng,
                Course = 1,
                Speed = 120,
                DeviceType = DeviceType.IPhone.Description(),
                MemberId = (int)address.MemberId,
                DeviceGuid = "TEST_GPS",
                DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = route4Me.SetGPS(gpsParameters, out var _);

            return response != null && response.GetType() == typeof(SetGpsResponse)
                ? true
                : false;
        }

        [Test]
        public void GetDeviceHistoryTimeRangeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var tsp2days = new TimeSpan(2, 0, 0, 0);
            var dtNow = DateTime.Now;

            var gpsParameters = new GPSParameters
            {
                Format = "json",
                RouteId = _tdr.SDRT_route.RouteId,
                TimePeriod = "custom",
                StartDate = R4MeUtils.ConvertToUnixTimestamp(dtNow - tsp2days),
                EndDate = R4MeUtils.ConvertToUnixTimestamp(dtNow + tsp2days)
            };

            var response = route4Me.GetDeviceLocationHistory(gpsParameters, out var errorString);

            Assert.That(response, Is.InstanceOf<DeviceLocationHistoryResponse>(), "GetDeviceHistoryTimeRangeTest failed. " + errorString);
        }

        [Test]
        public void TrackDeviceLastLocationHistoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var trParameters = new RouteParametersQuery
            {
                RouteId = _tdr.SDRT_route_id,
                DeviceTrackingHistory = true
            };

            var dataObject = route4Me.GetLastLocation(trParameters, out var errorString);

            Assert.IsNotNull(
                dataObject,
                "TrackDeviceLastLocationHistoryTest failed. " + errorString);
        }

        [Test]
        public void GetAllUserLocationsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(
                genericParameters,
                out var errorString);

            Assert.IsNotNull(
                userLocations,
                "GetAllUserLocationsTest failed. " + errorString);
        }

        [Test]
        public void QueryUserLocationsTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.ActualApiKey);

            var genericParameters = new GenericParameters();

            var userLocations = route4Me.GetUserLocations(genericParameters, out var errorString);

            Assert.IsNotNull(
                userLocations,
                "GetAllUserLocationsTest failed. " + errorString
            );

            var userLocation = userLocations.Where(x => x.UserTracking != null).FirstOrDefault();

            var email = userLocation.MemberData.MemberEmail;

            genericParameters.ParametersCollection.Add("query", email);

            var queriedUserLocations = route4Me.GetUserLocations(genericParameters, out errorString);

            Assert.IsNotNull(
                queriedUserLocations,
                "QueryUserLocationsTest failed. " + errorString
            );

            Assert.IsTrue(
                queriedUserLocations.Count() == 1,
                "QueryUserLocationsTest failed. " + errorString
            );
        }

        [OneTimeTearDown]
        public void TrackingGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");
        }
    }
}