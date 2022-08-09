using System.Collections.Generic;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class TerritoriesGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private readonly List<string> _lsTerritories = new List<string>();

        [OneTimeSetUp]
        public void TerritoriesGroupInitialize()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var circleTerritoryParameters = new TerritoryZoneParameters
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

            var circleTerritory = route4Me.CreateTerritory(
                circleTerritoryParameters,
                out var errorString);

            if (circleTerritory != null) _lsTerritories.Add(circleTerritory.TerritoryId);

            Assert.IsNotNull(circleTerritory, "Add Circle Territory test failed. " + errorString);

            var polyTerritoryParameters = new TerritoryZoneParameters
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

            var polyTerritory = route4Me.CreateTerritory(polyTerritoryParameters, out errorString);

            Assert.IsNotNull(polyTerritory, "Add Polygon Territory test failed. " + errorString);

            if (polyTerritory != null) _lsTerritories.Add(polyTerritory.TerritoryId);

            var rectTerritoryParameters = new TerritoryZoneParameters
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

            var rectTerritory = route4Me.CreateTerritory(
                rectTerritoryParameters,
                out errorString);

            Assert.IsNotNull(
                rectTerritory,
                "Add Rectangular Territory Zone test failed. " + errorString);

            if (_lsTerritories != null) _lsTerritories.Add(rectTerritory.TerritoryId);
        }

        [Test]
        public void AddTerritoriesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var circleTerritoryParameters = new TerritoryZoneParameters
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

            var circleTerritory = route4Me.CreateTerritory(
                circleTerritoryParameters,
                out var errorString);

            if (circleTerritory != null) _lsTerritories.Add(circleTerritory.TerritoryId);

            Assert.IsNotNull(circleTerritory, "Add Circle Territory test failed. " + errorString);
        }

        [Test]
        public void GetTerritoriesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryQuery = new TerritoryQuery();

            // Run the query
            var territories = route4Me.GetTerritories(
                territoryQuery,
                out var errorString);

            Assert.That(territories, Is.InstanceOf<TerritoryZone[]>(), "GetTerritoriesTest failed. " + errorString);
        }

        [Test]
        public void GetTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = _lsTerritories.Count > 1 ? _lsTerritories[1] : "";

            var territoryQuery = new TerritoryQuery
            {
                TerritoryId = territoryId,
                Addresses = 1,
                Orders = 1
            };

            // Run the query
            var territory = route4Me.GetTerritory(territoryQuery, out var errorString);

            Assert.IsNotNull(territory, "GetTerritoryTest failed. " + errorString);
        }

        [Test]
        public void UpdateTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = _lsTerritories.Count > 1 ? _lsTerritories[1] : "";

            var territoryParameters = new TerritoryZoneParameters
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
            var territory = route4Me.UpdateTerritory(territoryParameters, out var errorString);

            Assert.IsNotNull(territory, "UpdateTerritoryTest failed. " + errorString);
        }

        [Test]
        public void RemoveTerritoryTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var territoryId = _lsTerritories.Count > 0 ? _lsTerritories[0] : "";

            var territoryQuery = new TerritoryQuery
            {
                TerritoryId = territoryId
            };

            // Run the query
            var result = route4Me.RemoveTerritory(territoryQuery, out var errorString);

            Assert.IsTrue(result, "RemoveTerritoriesTest failed. " + errorString);

            if (result) _lsTerritories.RemoveAt(0);
        }

        [OneTimeTearDown]
        public void TerritoriesGroupCleanup()
        {
            foreach (var territoryId in _lsTerritories)
            {
                var route4Me = new Route4MeManager(c_ApiKey);

                var territoryQuery = new TerritoryQuery
                {
                    TerritoryId = territoryId
                };

                // Run the query
                var result = route4Me.RemoveTerritory(territoryQuery, out var errorString);

                Assert.IsTrue(result, "RemoveTerritoriesTest failed. " + errorString);
            }
        }
    }
}