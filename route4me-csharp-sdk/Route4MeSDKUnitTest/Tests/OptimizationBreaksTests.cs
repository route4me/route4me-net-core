using NUnit.Framework;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    internal class OptimizationBreaksTests
    {
        [Test]
        public void SerializeOptimizationParameters_WithBreaks_IncludesBreaksInPayload()
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
                    RouteName = "Optimization breaks test",
                    Breaks = new[]
                    {
                        new OptimizationBreak
                        {
                            Mode = 1,
                            ModeParams = new[] { 3200 },
                            RepeatsNumber = 1,
                            Duration = 600
                        },
                        new OptimizationBreak
                        {
                            Mode = 1,
                            ModeParams = new[] { 7200 },
                            RepeatsNumber = 1,
                            Duration = 1800
                        }
                    }
                }
            };

            var json = R4MeUtils.SerializeObjectToJson(optimizationParameters);

            Assert.That(json, Does.Contain("\"breaks\":["));
            Assert.That(json, Does.Contain("\"mode\":1"));
            Assert.That(json, Does.Contain("\"mode_params\":[3200]"));
            Assert.That(json, Does.Contain("\"duration\":600"));
            Assert.That(json, Does.Contain("\"duration\":1800"));
        }

        [Test]
        public void SerializeV5RouteParameters_WithBreaks_IncludesBreaksInPayload()
        {
            var routeParameters = new Route4MeSDK.DataTypes.V5.RouteParameters
            {
                RouteName = "V5 Optimization breaks test",
                Breaks = new[]
                {
                    new OptimizationBreak
                    {
                        Mode = 1,
                        ModeParams = new[] { 3200 },
                        RepeatsNumber = 1,
                        Duration = 600
                    }
                }
            };

            var json = R4MeUtils.SerializeObjectToJson(routeParameters);

            Assert.That(json, Does.Contain("\"breaks\":["));
            Assert.That(json, Does.Contain("\"mode\":1"));
            Assert.That(json, Does.Contain("\"mode_params\":[3200]"));
            Assert.That(json, Does.Contain("\"duration\":600"));
        }
    }
}
