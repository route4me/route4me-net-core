using System.Collections.Generic;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.AddOnRoutesApi
{
    [TestFixture]
    public class AddOnRoutesApiTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository _tdr;
        private static TestDataRepository _tdr2;
        private static List<string> _lsOptimizationIDs;
        private static List<string> _lsRoutenIDs;


        [SetUp]
        public void Setup()
        {
            _lsOptimizationIDs = new List<string>();
            _lsRoutenIDs = new List<string>();

            _tdr = new TestDataRepository();
            _tdr2 = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();
            var result2 = _tdr2.RunOptimizationSingleDriverRoute10Stops();
            _tdr2.RunSingleDriverRoundTrip();
            var result4 = _tdr2.MultipleDepotMultipleDriverWith24StopsTimeWindowTest();

            Assert.True(result, "Single Driver 10 Stops generation failed.");
            Assert.True(result2, "Single Driver 10 Stops generation failed.");
            Assert.True(result4, "Multi-Depot Multi-Driver 24 Stops generation failed.");

            Assert.True(_tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.True(_tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SD10Stops_optimization_problem_id);
            _lsOptimizationIDs.Add(_tdr2.SD10Stops_optimization_problem_id);
            _lsOptimizationIDs.Add(_tdr2.SDRT_optimization_problem_id);
            _lsOptimizationIDs.Add(_tdr2.MDMD24_optimization_problem_id);
        }

        [TearDown]
        public void TearDown()
        {
            var optimizationResult = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            var route4Me = new Route4MeManagerV5(CApiKey);

            if (_lsRoutenIDs.Count > 0)
            {
                route4Me.DeleteRoutes(
                    _lsRoutenIDs.ToArray(),
                    out _);
            }

            Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Test]
        public void GetAllRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void GetAllRoutesWithPaginationTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Page = 1,
                PerPage = 20
            };

            var dataObjects = route4Me.GetAllRoutesWithPagination(routeParameters, out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void GetPaginatedRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Page = 1,
                PerPage = 20
            };

            var dataObjects =
                route4Me.GetPaginatedRouteListWithoutElasticSearch(routeParameters, out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void GetRouteDataTableWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] {"2021-02-01", "2021-02-01"}
                },
                OrderBy = new List<string[]>
                {
                    new[] {"route_created_unix", "desc"}
                },
                Timezone = "UTC"
            };

            var dataObjects = route4Me.GetRouteDataTableWithElasticSearch(
                routeParameters,
                out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void GetRouteDatatableWithElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] {"2021-02-01", "2021-02-01"}
                },
                OrderBy = new List<string[]>
                {
                    new[] {"route_created_unix", "desc"}
                },
                Timezone = "UTC"
            };

            var dataObjects = route4Me.GetRouteDataTableWithElasticSearch(
                routeParameters,
                out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void GetRouteListWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Offset = 0,
                Limit = 10
            };

            var dataObjects = route4Me.GetRouteListWithoutElasticSearch(routeParameters, out _);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void DuplicateRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeIDs = new[] {_tdr.SD10Stops_route.RouteID};

            var result = route4Me.DuplicateRoute(routeIDs, out _);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(RouteDuplicateResponse)));
            Assert.True(result.Status);

            if (result.RouteIDs.Length > 0)
                foreach (var routeId in result.RouteIDs)
                    _lsRoutenIDs.Add(routeId);
        }

        [Test]
        public void DeleteRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeIDs = new[] {_tdr2.MDMD24_route_id};

            var result = route4Me.DeleteRoutes(routeIDs, out _);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(RoutesDeleteResponse)));
            Assert.True(result.Deleted);
        }

        [Test]
        public void GetRouteDataTableConfig()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.GetRouteDataTableConfig(out _);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(RouteDataTableConfigResponse)));
        }

        [Test]
        public void GetRouteDataTableFallbackConfig()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.GetRouteDataTableFallbackConfig(out _);

            Assert.NotNull(result);
            Assert.That(result.GetType(), Is.EqualTo(typeof(RouteDataTableConfigResponse)));
        }

        [Test]
        [Ignore("Will be finished after implementing Route Destinations API")]
        public void UpdateRouteTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            _tdr.SD10Stops_route.Parameters.DistanceUnit = DistanceUnit.KM.Description();
            _tdr.SD10Stops_route.Parameters.Parts = 2;
            _tdr.SD10Stops_route.Parameters = null;
            var addresses = new List<Address>();

            _tdr.SD10Stops_route.Addresses[2].SequenceNo = 4;
            _tdr.SD10Stops_route.Addresses[2].Alias = "Address 2";
            _tdr.SD10Stops_route.Addresses[2].AddressStopType = AddressStopType.Delivery.Description();
            addresses.Add(_tdr.SD10Stops_route.Addresses[2]);

            _tdr.SD10Stops_route.Addresses[3].SequenceNo = 3;
            _tdr.SD10Stops_route.Addresses[3].Alias = "Address 3";
            _tdr.SD10Stops_route.Addresses[3].AddressStopType = AddressStopType.PickUp.Description();
            addresses.Add(_tdr.SD10Stops_route.Addresses[3]);

            _tdr.SD10Stops_route.Addresses = addresses.ToArray();

            var updatedRoute = route4Me.UpdateRoute(_tdr.SD10Stops_route, out _);

            Assert.NotNull(updatedRoute);
            Assert.That(updatedRoute.GetType(), Is.EqualTo(typeof(RouteDataTableConfigResponse)));
        }
    }
}