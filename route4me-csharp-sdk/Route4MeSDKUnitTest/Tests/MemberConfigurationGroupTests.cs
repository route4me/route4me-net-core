using System.Collections.Generic;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class MemberConfigurationGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private List<string> _lsConfigurationKeys;

        [OneTimeSetUp]
        public void MemberConfigurationGroupInitialize()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            _lsConfigurationKeys = new List<string>();

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My height",
                ConfigValue = "180"
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(@params, out var errorString);
            Assert.IsNotNull(
                result,
                "AddNewConfigurationKeyTest failed. " + errorString);

            _lsConfigurationKeys.Add("Test My height");

            var keyrParams = new MemberConfigurationParameters
            {
                ConfigKey = "Test Remove Key",
                ConfigValue = "remove"
            };

            var result2 = route4Me.CreateNewConfigurationKey(keyrParams, out errorString);
            Assert.IsNotNull(
                result2,
                "AddNewConfigurationKeyTest failed... " + errorString);

            _lsConfigurationKeys.Add("Test Remove Key");
        }

        [Test]
        public void AddNewConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My weight",
                ConfigValue = "100"
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(@params, out var errorString);

            Assert.IsNotNull(result, "AddNewConfigurationKeyTest failed. " + errorString);

            _lsConfigurationKeys.Add("Test My weight");
        }

        [Test]
        public void AddConfigurationKeyArrayTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var parametersArray = new[]
            {
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test My Height",
                    ConfigValue = "185"
                },
                new MemberConfigurationParameters
                {
                    ConfigKey = "Test My Weight",
                    ConfigValue = "110"
                }
            };

            // Run the query
            var result = route4Me.CreateNewConfigurationKey(
                parametersArray,
                out var errorString);

            Assert.IsNotNull(
                result,
                "AddNewConfigurationKeyTest failed. " + errorString);

            _lsConfigurationKeys.Add("Test My Height");
            _lsConfigurationKeys.Add("Test My Weight");
        }

        [Test]
        public void GetAllConfigurationDataTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters();

            // Run the query
            var result = route4Me.GetConfigurationData(@params, out var errorString);

            Assert.IsNotNull(
                result,
                "GetAllConfigurationDataTest failed. " + errorString);
        }

        [Test]
        public void GetSpecificConfigurationKeyDataTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters { ConfigKey = "Test My height" };

            // Run the query
            var result = route4Me.GetConfigurationData(@params, out var errorString);

            Assert.IsNotNull(
                result,
                "GetSpecificConfigurationKeyDataTest failed. " + errorString);
        }

        [Test]
        public void UpdateConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test My height",
                ConfigValue = "190"
            };

            // Run the query
            var result = route4Me.UpdateConfigurationKey(
                @params,
                out var errorString);

            Assert.IsNotNull(
                result,
                "UpdateConfigurationKeyTest failed. " + errorString);
        }

        [Test]
        public void RemoveConfigurationKeyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var @params = new MemberConfigurationParameters
            {
                ConfigKey = "Test Remove Key"
            };

            // Run the query
            var result = route4Me.RemoveConfigurationKey(@params, out var errorString);

            Assert.IsNotNull(result, "RemoveConfigurationKeyTest failed. " + errorString);

            _lsConfigurationKeys.Remove("Test Remove Key");
        }

        [OneTimeTearDown]
        public void MemberConfigurationGroupCleanup()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var testKey in _lsConfigurationKeys)
            {
                var @params = new MemberConfigurationParameters { ConfigKey = testKey };

                var result = route4Me.RemoveConfigurationKey(@params, out var errorString);

                Assert.IsNotNull(result, "MemberConfigurationGroupCleanup failed.");
            }
        }
    }
}