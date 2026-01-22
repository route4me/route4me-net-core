using System;
using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class RouteParametersQueryTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        [Test]
        public void Serialize_WithUseTimezoneTrue_IncludesUseTimezoneAsOne()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("api_key=TEST_API_KEY"));
        }

        [Test]
        public void Serialize_WithUseTimezoneFalse_IncludesUseTimezoneAsZero()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = false
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=0"));
        }

        [Test]
        public void Serialize_WithUseTimezoneNull_DoesNotIncludeUseTimezone()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = null
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Not.Contain("use_timezone"));
        }

        [Test]
        public void Serialize_WithTimezone_IncludesTimezone()
        {
            var routeParameters = new RouteParametersQuery
            {
                Timezone = "Australia/Melbourne"
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("timezone=Australia%2FMelbourne"));
        }

        [Test]
        public void Serialize_WithTimezoneNull_DoesNotIncludeTimezone()
        {
            var routeParameters = new RouteParametersQuery
            {
                Timezone = null
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Not.Contain("timezone"));
        }

        [Test]
        public void Serialize_WithUseTimezoneAndTimezone_IncludesBoth()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "America/New_York"
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=America%2FNew_York"));
        }

        [Test]
        public void Serialize_WithUseCombinedTimestampTrue_IncludesUseCombinedTimestampAsOne()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseCombinedTimestamp = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_combined_timestamp=1"));
            Assert.That(serialized, Does.Contain("api_key=TEST_API_KEY"));
        }

        [Test]
        public void Serialize_WithUseCombinedTimestampFalse_IncludesUseCombinedTimestampAsZero()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseCombinedTimestamp = false
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_combined_timestamp=0"));
        }

        [Test]
        public void Serialize_WithUseCombinedTimestampNull_DoesNotIncludeUseCombinedTimestamp()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseCombinedTimestamp = null
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Not.Contain("use_combined_timestamp"));
        }

        [Test]
        public void Serialize_WithCompressPathPointsTrue_IncludesCompressPathPointsAsOne()
        {
            var routeParameters = new RouteParametersQuery
            {
                CompressPathPoints = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("compress_path_points=1"));
        }

        [Test]
        public void Serialize_WithCompressPathPointsFalse_IncludesCompressPathPointsAsZero()
        {
            var routeParameters = new RouteParametersQuery
            {
                CompressPathPoints = false
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("compress_path_points=0"));
        }

        [Test]
        public void Serialize_WithCompressPathPointsNull_DoesNotIncludeCompressPathPoints()
        {
            var routeParameters = new RouteParametersQuery
            {
                CompressPathPoints = null
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Not.Contain("compress_path_points"));
        }

        [Test]
        public void Serialize_WithAllTimezoneParameters_IncludesAll()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "Europe/London",
                CompressPathPoints = true,
                StartDate = "2025-01-01",
                EndDate = "2025-01-31"
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=Europe%2FLondon"));
            Assert.That(serialized, Does.Contain("compress_path_points=1"));
            Assert.That(serialized, Does.Contain("start_date=2025-01-01"));
            Assert.That(serialized, Does.Contain("end_date=2025-01-31"));
        }

        [Test]
        public void Serialize_WithAllDateFilteringParameters_IncludesAll()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "Europe/London",
                UseCombinedTimestamp = true,
                StartDate = "2025-01-01",
                EndDate = "2025-01-31"
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=Europe%2FLondon"));
            Assert.That(serialized, Does.Contain("use_combined_timestamp=1"));
            Assert.That(serialized, Does.Contain("start_date=2025-01-01"));
            Assert.That(serialized, Does.Contain("end_date=2025-01-31"));
        }

        [Test]
        public void Serialize_WithoutApiKey_DoesNotIncludeApiKey()
        {
            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "Australia/Melbourne"
            };

            var serialized = routeParameters.Serialize();

            Assert.That(serialized, Does.Not.Contain("api_key"));
            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=Australia%2FMelbourne"));
        }

        // V5 Tests
        [Test]
        public void SerializeV5_WithUseTimezoneTrue_IncludesUseTimezoneAsOne()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseTimezone = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
        }

        [Test]
        public void SerializeV5_WithTimezone_IncludesTimezone()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                Timezone = "Asia/Tokyo"
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("timezone=Asia%2FTokyo"));
        }

        [Test]
        public void SerializeV5_WithCompressPathPointsTrue_IncludesCompressPathPointsAsOne()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                CompressPathPoints = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("compress_path_points=1"));
        }

        [Test]
        public void SerializeV5_WithAllTimezoneParameters_IncludesAll()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "America/Los_Angeles",
                CompressPathPoints = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=America%2FLos_Angeles"));
            Assert.That(serialized, Does.Contain("compress_path_points=1"));
        }

        [Test]
        public void SerializeV5_WithUseCombinedTimestampTrue_IncludesUseCombinedTimestampAsOne()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseCombinedTimestamp = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_combined_timestamp=1"));
        }

        [Test]
        public void SerializeV5_WithUseCombinedTimestampFalse_IncludesUseCombinedTimestampAsZero()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseCombinedTimestamp = false
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_combined_timestamp=0"));
        }

        [Test]
        public void SerializeV5_WithUseCombinedTimestampNull_DoesNotIncludeUseCombinedTimestamp()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseCombinedTimestamp = null
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Not.Contain("use_combined_timestamp"));
        }

        [Test]
        public void SerializeV5_WithAllDateFilteringParameters_IncludesAll()
        {
            var routeParameters = new Route4MeSDK.QueryTypes.V5.RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "America/Los_Angeles",
                UseCombinedTimestamp = true,
                CompressPathPoints = true
            };

            var serialized = routeParameters.Serialize("TEST_API_KEY");

            Assert.That(serialized, Does.Contain("use_timezone=1"));
            Assert.That(serialized, Does.Contain("timezone=America%2FLos_Angeles"));
            Assert.That(serialized, Does.Contain("use_combined_timestamp=1"));
            Assert.That(serialized, Does.Contain("compress_path_points=1"));
        }

        // Integration tests (require API key)
        [Test]
        public void GetRoutes_WithTimezoneParameters_ReturnsRoutes()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                UseTimezone = true,
                Timezone = "America/New_York",
                StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Limit = 5
            };

            var routes = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsNull(errorString, $"GetRoutes with timezone parameters failed: {errorString}");
            Assert.IsNotNull(routes, "GetRoutes with timezone parameters returned null");
        }

        [Test]
        public void GetRoutes_WithCompressPathPoints_ReturnsRoutes()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                CompressPathPoints = true,
                StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Limit = 5
            };

            var routes = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsNull(errorString, $"GetRoutes with compress_path_points failed: {errorString}");
            Assert.IsNotNull(routes, "GetRoutes with compress_path_points returned null");
        }

        [Test]
        public void GetRoute_WithTimezoneParameters_ReturnsRoute()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            // First, get a route ID
            var allRoutesParameters = new RouteParametersQuery
            {
                Limit = 1
            };

            var routes = route4Me.GetRoutes(allRoutesParameters, out var errorString);
            if (routes == null || routes.Length == 0)
            {
                Assert.Inconclusive("No routes available for testing");
                return;
            }

            var routeId = routes[0].RouteId;

            // Now test with timezone parameters
            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                UseTimezone = true,
                Timezone = "Europe/London"
            };

            var route = route4Me.GetRoute(routeParameters, out errorString);

            Assert.IsNull(errorString, $"GetRoute with timezone parameters failed: {errorString}");
            Assert.IsNotNull(route, "GetRoute with timezone parameters returned null");
        }

        [Test]
        public void GetRoutes_WithUseCombinedTimestamp_ReturnsRoutes()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                UseCombinedTimestamp = true,
                StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Limit = 5
            };

            var routes = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsNull(errorString, $"GetRoutes with use_combined_timestamp failed: {errorString}");
            Assert.IsNotNull(routes, "GetRoutes with use_combined_timestamp returned null");
        }

        [Test]
        public void GetRoutes_WithUseCombinedTimestampFalse_ReturnsRoutes()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                UseCombinedTimestamp = false,
                StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Limit = 5
            };

            var routes = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.IsNull(errorString, $"GetRoutes with use_combined_timestamp=false failed: {errorString}");
            Assert.IsNotNull(routes, "GetRoutes with use_combined_timestamp=false returned null");
        }
    }
}