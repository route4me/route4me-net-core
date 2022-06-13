using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class TelematicsGateWayAPITests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private static string apiToken;

        private string _firstMemberId;
        private List<TelematicsConnection> _lsCreatedConnections;
        private TelematicsVendors _tomtomVendor;

        [OneTimeSetUp]
        public void TelematicsGateWayAPIInitialize()
        {
            if (ApiKeys.ActualApiKey == ApiKeys.DemoApiKey)
                Assert.Inconclusive("The test cannot done with demo API key");

            var route4Me = new Route4MeManager(c_ApiKey);

            //lsMembers = new List<string>();
            _lsCreatedConnections = new List<TelematicsConnection>();

            var members = route4Me.GetUsers(new GenericParameters(), out var errString);

            Assert.IsNotNull((members?.Results?.Length ?? 0) > 0,
                "Cannot retrieve the account members." + Environment.NewLine + errString);

            _firstMemberId = members.Results[0].MemberId;

            var memberParameters = new TelematicsVendorParameters
            {
                MemberID = Convert.ToUInt32(_firstMemberId),
                ApiKey = c_ApiKey
            };

            var result = route4Me.RegisterTelematicsMember(memberParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test registerMemberTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsRegisterMemberResponse>());

            apiToken = result.ApiToken;

            var vendParams = new TelematicsVendorParameters {Search = "tomtom"};

            var vendors = route4Me.SearchTelematicsVendors(vendParams, out var errorString2);

            Assert.IsNotNull(
                vendors?.Vendors ?? null,
                "Cannot retrieve tomtom vendor. " + errorString);

            Assert.That(vendors.Vendors, Is.InstanceOf<TelematicsVendors[]>());

            Assert.IsTrue(vendors.Vendors.Length > 0);

            _tomtomVendor = vendors.Vendors[0];

            #region Test Connection

            var conParams = new TelematicsConnectionParameters
            {
                Vendor = TelematicsVendorType.Geotab.Description(),
                AccountId = "54321",
                UserName = "John Doe 0",
                Password = "password0",
                VehiclePositionRefreshRate = 60,
                Name = "Test Geotab Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result0 = route4Me.CreateTelematicsConnection(apiToken, conParams, out var errorString0);

            Assert.IsNotNull(result0,
                "The test createTelematicsConnectionTest failed. " + errorString);

            Assert.That(result0, Is.InstanceOf<TelematicsConnection>());

            _lsCreatedConnections.Add(result0);

            #endregion
        }

        [Test]
        public void GetAllVendorsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters();

            var vendors = route4Me.GetAllTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(vendors, "The test getAllVendorsTest failed. " + errorString);

            Assert.That(vendors, Is.InstanceOf<TelematicsVendorsResponse>(), "The test GetAllVendorsTest failed. " + errorString);
        }

        [Test]
        public void GetVendorTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendors = route4Me.GetAllTelematicsVendors(
                new TelematicsVendorParameters(),
                out var errorString);

            var randomNumber = new Random().Next(0, vendors.Vendors.Count() - 1);
            var randomVendorID = vendors.Vendors[randomNumber].ID;

            var vendorParameters = new TelematicsVendorParameters
            {
                VendorID = Convert.ToUInt32(randomVendorID)
            };

            var vendor = route4Me.GetTelematicsVendor(vendorParameters, out errorString);

            Assert.IsNotNull(vendor, "The test getVendorTest failed. " + errorString);

            Assert.That(vendor, Is.InstanceOf<TelematicsVendorResponse>(), "The test GetVendorTest failed. " + errorString);
        }

        [Test]
        public void SearchVendorsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                //Country = "GB",  // uncomment this line for searching by Country
                IsIntegrated = 1,
                //Feature = "Satelite",  // uncomment this line for searching by Feature
                Search = "Fleet",
                Page = 1,
                PerPage = 15
            };

            var vendors = route4Me.SearchTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(vendors, "The test searchVendorsTest failed. " + errorString);

            Assert.That(vendors, Is.InstanceOf<TelematicsVendorsResponse>(), "The test SearchVendorsTest failed. " + errorString);
        }

        [Test]
        public void VendorsComparisonTest()
        {
            var route4Me = new Route4MeManager(ApiKeys.DemoApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                Vendors = "55,56,57"
            };

            var vendors = route4Me.SearchTelematicsVendors(vendorParameters, out var errorString);

            Assert.IsNotNull(
                vendors,
                "The test vendorsComparisonTest failed. " + errorString);

            Assert.That(vendors, Is.InstanceOf<TelematicsVendorsResponse>(), "The test VendorsComparisonTest failed. " + errorString);
        }

        [Test]
        public void RegisterMemberTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                MemberID = Convert.ToUInt32(_firstMemberId),
                ApiKey = c_ApiKey
            };

            var result = route4Me.RegisterTelematicsMember(vendorParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test RegisterMemberTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsRegisterMemberResponse>());
        }

        [Test]
        public void GetTelematicsConnectionsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vendorParameters = new TelematicsVendorParameters
            {
                ApiToken = apiToken
            };

            var result = route4Me.GetTelematicsConnections(vendorParameters, out var errorString);

            Assert.IsNotNull(
                result,
                "The test GetTelematicsConnectionsTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsConnection[]>());
        }

        [Test]
        public void CreateTelematicsConnectionTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(_tomtomVendor.ID),
                Vendor = _tomtomVendor.Slug,
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };

            var result = route4Me.CreateTelematicsConnection(apiToken, conParams, out var errorString);

            Assert.IsNotNull(
                result,
                "The test CreateTelematicsConnectionTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsConnection>());

            _lsCreatedConnections.Add(result);
        }

        [Test]
        public void DeleteTelematicsConnectionTest()
        {
            if (_lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.DeleteTelematicsConnection(
                apiToken,
                _lsCreatedConnections[_lsCreatedConnections.Count - 1].ConnectionToken,
                out var errorString);

            Assert.IsNotNull(
                result,
                "The test DeleteTelematicsConnectionTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsConnection>());

            _lsCreatedConnections.RemoveAt(_lsCreatedConnections.Count - 1);
        }

        [Test]
        public void UpdateTelematicsConnectionTest()
        {
            if (_lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var conParams = new TelematicsConnectionParameters
            {
                VendorID = Convert.ToUInt32(_tomtomVendor.ID),
                AccountId = "12345",
                UserName = "John Doe",
                Password = "password",
                VehiclePositionRefreshRate = 60,
                Name = "Test Telematics Connection from c# SDK",
                ValidateRemoteCredentials = false
            };
        }

        [Test]
        public void GetTelematicsConnectionTest()
        {
            if (_lsCreatedConnections.Count < 1) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.GetTelematicsConnection(
                apiToken,
                _lsCreatedConnections[0].ConnectionToken,
                out var errorString);

            Assert.IsNotNull(
                result,
                "The test getTelematicsConnectionTest failed. " + errorString);

            Assert.That(result, Is.InstanceOf<TelematicsConnection>());
        }

        [OneTimeTearDown]
        public void TelematicsGateWayAPICleanup()
        {
            if (_lsCreatedConnections.Count > 0)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                foreach (var conn in _lsCreatedConnections)
                {
                    var result =
                        route4Me.DeleteTelematicsConnection(apiToken, conn.ConnectionToken, out var errorString);
                }
            }
        }
    }
}