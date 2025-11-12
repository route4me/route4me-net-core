using System.Collections.Generic;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AvoidanseZonesGroupTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private readonly List<string> _lsAvoidanceZones = new List<string>();

        [OneTimeSetUp]
        public void AvoidanseZonesGroupInitialize()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var circleAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleAvoidanceZone = route4Me.AddAvoidanceZone(
                circleAvoidanceZoneParameters,
                out var errorString);

            if (circleAvoidanceZone != null) _lsAvoidanceZones.Add(circleAvoidanceZone.TerritoryId);

            Assert.IsNotNull(
                circleAvoidanceZone,
                "Add Circle Avoidance Zone test failed. " + errorString);

            var polyAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Poly Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Poly.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "37.75886716305343,-77.68974800109863",
                        "37.74763966054455,-77.6917221069336",
                        "37.74655084306813,-77.68863220214844",
                        "37.7502255383101,-77.68125076293945",
                        "37.74797991274437,-77.67498512268066",
                        "37.73327960206065,-77.6411678314209",
                        "37.74430510679532,-77.63172645568848",
                        "37.76641925847049,-77.66846199035645"
                    }
                }
            };

            var polyAvoidanceZone = route4Me.AddAvoidanceZone(
                polyAvoidanceZoneParameters,
                out errorString);

            Assert.IsNotNull(
                polyAvoidanceZone,
                "Add Polygon Avoidance Zone test failed. " + errorString);

            if (polyAvoidanceZone != null) _lsAvoidanceZones.Add(polyAvoidanceZone.TerritoryId);

            var rectAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Rect Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Rect.Description(),
                    Data = new[]
                    {
                        "43.51668853502909,-109.3798828125",
                        "46.98025235521883,-101.865234375"
                    }
                }
            };

            var rectAvoidanceZone = route4Me.AddAvoidanceZone(
                rectAvoidanceZoneParameters,
                out errorString);

            Assert.IsNotNull(
                rectAvoidanceZone,
                "Add Rectangular Avoidance Zone test failed. " + errorString);

            if (_lsAvoidanceZones != null) _lsAvoidanceZones.Add(rectAvoidanceZone.TerritoryId);
        }

        [Test]
        public void AddAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var circleAvoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryName = "Test Circle Territory",
                TerritoryColor = "ff0000",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "37.569752822786455,-77.47833251953125",
                        "5000"
                    }
                }
            };

            var circleAvoidanceZone = route4Me.AddAvoidanceZone(
                circleAvoidanceZoneParameters,
                out var errorString);

            if (circleAvoidanceZone != null) _lsAvoidanceZones.Add(circleAvoidanceZone.TerritoryId);

            Assert.IsNotNull(
                circleAvoidanceZone,
                "Add Circle Avoidance Zone test failed. " + errorString);
        }

        [Test]
        public void GetAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var avoidanceZoneQuery = new AvoidanceZoneQuery();

            // Run the query
            var avoidanceZones = route4Me.GetAvoidanceZones(
                avoidanceZoneQuery,
                out var errorString);

            Assert.That(avoidanceZones, Is.InstanceOf<AvoidanceZone[]>(), "GetAvoidanceZonesTest failed. " + errorString);
        }

        [Test]
        public void GetAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var territoryId = _lsAvoidanceZones.Count > 1 ? _lsAvoidanceZones[1] : "";

            var avoidanceZoneQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var avoidanceZone = route4Me.GetAvoidanceZone(
                avoidanceZoneQuery,
                out var errorString);

            Assert.IsNotNull(
                avoidanceZone,
                "GetAvoidanceZonesTest failed. " + errorString);
        }

        [Test]
        public void UpdateAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var territoryId = _lsAvoidanceZones.Count > 1 ? _lsAvoidanceZones[1] : "";

            var avoidanceZoneParameters = new AvoidanceZoneParameters
            {
                TerritoryId = territoryId,
                TerritoryName = "Test Territory Updated",
                TerritoryColor = "ff00ff",
                Territory = new Territory
                {
                    Type = TerritoryType.Circle.Description(),
                    Data = new[]
                    {
                        "38.41322259056806,-78.501953234",
                        "3000"
                    }
                }
            };

            // Run the query
            var avoidanceZone = route4Me.UpdateAvoidanceZone(
                avoidanceZoneParameters,
                out var errorString);

            Assert.IsNotNull(
                avoidanceZone,
                "UpdateAvoidanceZoneTest failed. " + errorString);
        }

        [Test]
        public void RemoveAvoidanceZoneTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var territoryId = _lsAvoidanceZones.Count > 0 ? _lsAvoidanceZones[0] : "";

            var avoidanceZoneQuery = new AvoidanceZoneQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var result = route4Me.DeleteAvoidanceZone(
                avoidanceZoneQuery,
                out var errorString);

            Assert.IsTrue(result, "RemoveAvoidanceZoneTest failed. " + errorString);

            if (result) _lsAvoidanceZones.RemoveAt(0);
        }

        [OneTimeTearDown]
        public void AvoidanseZonesGroupCleanup()
        {
            foreach (var territoryId in _lsAvoidanceZones)
            {
                var route4Me = new Route4MeManager(CApiKey);

                var avoidanceZoneQuery = new AvoidanceZoneQuery
                {
                    TerritoryId = territoryId
                };

                // Run the query
                var result = route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, out var errorString);

                Assert.IsTrue(result, "RemoveAvoidanceZoneTest failed. " + errorString);
            }
        }
    }
}