using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    internal class CustomerImportanceTests
    {
        [Test]
        public void SerializeOptimizationParameters_WithImportance_IncludesItInPayload()
        {
            var optimizationParameters = new OptimizationParameters
            {
                Addresses = new[]
                {
                    new Address
                    {
                        AddressString = "754 5th Ave New York, NY 10019",
                        Latitude = 40.7636197,
                        Longitude = -73.9744388,
                        Importance = 2
                    }
                },
                Parameters = new RouteParameters { UseCustomerImportance = true }
            };

            var json = R4MeUtils.SerializeObjectToJson(optimizationParameters);

            Assert.That(json, Does.Contain("\"use_customer_importance\":true"));
            Assert.That(json, Does.Contain("\"importance\":2"));
        }

        [Test]
        public void SerializeV5RouteParameters_SendsFalseAndOmitsNull()
        {
            var off = R4MeUtils.SerializeObjectToJson(
                new Route4MeSDK.DataTypes.V5.RouteParameters { UseCustomerImportance = false });
            var notSet = R4MeUtils.SerializeObjectToJson(new Route4MeSDK.DataTypes.V5.RouteParameters());
            var address = R4MeUtils.SerializeObjectToJson(new Route4MeSDK.DataTypes.V5.Address { Importance = 3 });

            Assert.That(off, Does.Contain("\"use_customer_importance\":false"));
            Assert.That(notSet, Does.Not.Contain("use_customer_importance"));
            Assert.That(address, Does.Contain("\"importance\":3"));
        }
    }
}