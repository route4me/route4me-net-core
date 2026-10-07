using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class RouteParametersTruckSpeedCapTests
    {
        [Test]
        public void SerializeOptimizationRequest_WithTruckSpeedCap_SendsTruckSpeedCapInParameters()
        {
            var optimizationParameters = new OptimizationParameters
            {
                Parameters = new RouteParameters
                {
                    TruckSpeedCap = 80
                }
            };

            // Same serializer the SDK uses for the request body
            var json = R4MeUtils.SerializeObjectToJson(optimizationParameters);

            Assert.That(json, Does.Contain("\"parameters\":{\"truck_speed_cap\":80}"));
        }

        [Test]
        public void SerializeRouteParameters_WithTruckSpeedCap_IncludesTruckSpeedCapInPayload()
        {
            var routeParameters = new RouteParameters
            {
                TravelMode = "Trucking",
                TruckSpeedCap = 55.5
            };

            var json = R4MeUtils.SerializeObjectToJson(routeParameters, true);

            Assert.That(json, Does.Contain("\"truck_speed_cap\":55.5"));
        }

        [Test]
        public void SerializeRouteParameters_WithoutTruckSpeedCap_OmitsTruckSpeedCap()
        {
            var routeParameters = new RouteParameters
            {
                TravelMode = "Trucking"
            };

            var json = R4MeUtils.SerializeObjectToJson(routeParameters, true);

            Assert.That(json, Does.Not.Contain("truck_speed_cap"));
        }

        [Test]
        public void DeserializeRouteParameters_WithTruckSpeedCap_ReadsTruckSpeedCap()
        {
            var json = "{\"travel_mode\":\"Trucking\",\"truck_speed_cap\":80}";

            var routeParameters = R4MeUtils.ReadObjectNew<RouteParameters>(json);

            Assert.IsNotNull(routeParameters);
            Assert.That(routeParameters.TruckSpeedCap, Is.EqualTo(80));
        }
    }
}