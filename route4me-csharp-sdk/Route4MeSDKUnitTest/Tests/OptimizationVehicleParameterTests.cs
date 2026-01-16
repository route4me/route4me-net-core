using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    internal class OptimizationVehicleParameterTests
    {
        [Test]
        public void SerializeOptimizationParameters_WithVehicleObject_IncludesVehicleInPayload()
        {
            var optimizationParameters = new OptimizationParameters
            {
                Addresses = new[]
                {
                    new Address
                    {
                        AddressString = "754 5th Ave New York, NY 10019",
                        IsDepot = true,
                        Latitude = 40.7636197,
                        Longitude = -73.9744388
                    }
                },
                Parameters = new RouteParameters
                {
                    RouteName = "Vehicle object payload test",
                    TravelMode = "Trucking",
                    Vehicle = new VehicleV4Parameters
                    {
                        HeightInches = 120,
                        WeightLb = 26000,
                        HazmatType = "NONE",
                        UseTruckRestrictions = "YES"
                    }
                }
            };

            var json = R4MeUtils.SerializeObjectToJson(optimizationParameters, true);

            Assert.That(json, Does.Contain("\"vehicle\""));
            // VehicleV4Parameters uses DataMember names (e.g. "height_inches", "weight_lb", etc.)
            Assert.That(json, Does.Contain("\"height_inches\""));
            Assert.That(json, Does.Contain("\"weight_lb\""));
            Assert.That(json, Does.Contain("\"HazmatType\""));
            Assert.That(json, Does.Contain("\"UseTruckRestrictions\""));
        }
    }
}

