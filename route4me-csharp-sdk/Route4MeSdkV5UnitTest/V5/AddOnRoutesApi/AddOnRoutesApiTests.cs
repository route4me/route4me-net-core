using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteStatus;

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
            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);

            Assert.That(dataObjects.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        public void DuplicateRoutesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeIDs = new[] { _tdr.SD10Stops_route.RouteID };

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

            var routeIDs = new[] { _tdr2.MDMD24_route_id };

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
        public void UpdateRouteTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var _tdr = new TestDataRepository();

            _tdr.RunOptimizationSingleDriverRoute10Stops();

            /*
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
            */

            var routeParams = new RouteParametersQuery()
            {
                RouteId = _tdr.SD10Stops_route.RouteID,
                Parameters = new RouteParameters()
                {
                    RouteName = _tdr.SD10Stops_route.Parameters.RouteName + " Updated"
                },
                Addresses = new Address[]
                {
                    new Address()
                    {
                        RouteDestinationId = _tdr.SD10Stops_route.Addresses[2].RouteDestinationId,
                        Alias = "Address 2",
                        AddressStopType = AddressStopType.Delivery.Description(),
                        SequenceNo = 4
                    },
                    new Address()
                    {
                        RouteDestinationId = _tdr.SD10Stops_route.Addresses[3].RouteDestinationId,
                        Alias = "Address 3",
                        AddressStopType = AddressStopType.PickUp.Description(),
                        SequenceNo = 3
                    }
                }
            };

            var updatedRoute = route4Me.UpdateRoute(routeParams, out ResultResponse resultResponse);

            Assert.NotNull(updatedRoute);
            Assert.That(updatedRoute.GetType(), Is.EqualTo(typeof(DataObjectRoute)));
        }

        [Test]
        [Ignore("Run explicitly")]
        public void GetRouteStatusTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);

            var routeStatusResponse = route4Me.GetRouteStatus(dataObjects.First().RouteID, out var _);

            Assert.That(routeStatusResponse, Is.Not.Null);
            Assert.That(routeStatusResponse.GetType(), Is.EqualTo(typeof(RouteStatusResponse)));
            Assert.That(routeStatusResponse.Status, Is.EqualTo(RouteStatus.Planned.Description()));
        }

        [Test]
        [Ignore("Run explicitly")]
        public void UpdateRouteStatusTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);
            var routeId = dataObjects.First().RouteID;

            RouteStatusResponse updatedRouteStatusResponse = route4Me.UpdateRouteStatus(routeId, new RouteStatusRequest()
            {
                Latitude = 50,
                Longitude = 60,
                Status = RouteStatus.Started.Description(),
                EventTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            }, out var err2);

            Assert.That(updatedRouteStatusResponse.Latitude, Is.EqualTo(50));
            Assert.That(updatedRouteStatusResponse.Longitude, Is.EqualTo(60));
            Assert.That(updatedRouteStatusResponse.Status, Is.EqualTo(RouteStatus.Started.Description()));
        }

        [Test]
        [Ignore("Run explicitly")]
        public void GetRouteStatusHistoryTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse err1);
            var routeId = dataObjects.First().RouteID;

            RouteStatusResponse updatedRouteStatusResponse = route4Me.UpdateRouteStatus(routeId, new RouteStatusRequest()
            {
                Latitude = 50,
                Longitude = 60,
                Status = RouteStatus.Started.Description(),
                EventTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            }, out var err2);

            var routeStatusHistoryResponse = route4Me.GetRouteStatusHistory(new RouteStatusHistoryParameters()
            {
                RouteId = routeId,
                OrderBy = "asc",
                Start = 0,
                End = long.MaxValue
            }, out var err);

            Assert.That(routeStatusHistoryResponse, Is.Not.Null);
            Assert.That(routeStatusHistoryResponse.GetType(), Is.EqualTo(typeof(RouteStatusHistoryResponse)));
            Assert.That(routeStatusHistoryResponse.Data.Length, Is.EqualTo(2));
        }

        [Test]
        [Ignore("Run explicitly")]
        public void RollbackRouteStatusTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);
            var routeId = dataObjects.First().RouteID;

            var initialRouteStatusResponse = route4Me.GetRouteStatus(routeId, out var err1);

            RouteStatusResponse updatedRouteStatusResponse = route4Me.UpdateRouteStatus(routeId, new RouteStatusRequest()
            {
                Latitude = 50,
                Longitude = 60,
                Status = RouteStatus.Started.Description(),
                EventTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            }, out var err2);

            RouteStatusResponse updatedRouteStatusResponse2 = route4Me.UpdateRouteStatus(routeId, new RouteStatusRequest()
            {
                Latitude = 50,
                Longitude = 60,
                Status = RouteStatus.Paused.Description(),
                EventTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            }, out var err3);

            var routeStatusResponseWithRollback = route4Me.RollbackRouteStatus(routeId, out var err4);

            Assert.That(updatedRouteStatusResponse.Status, Is.EqualTo(routeStatusResponseWithRollback.Status));
        }

        [Test]
        [Ignore("Does not work yet")]
        public void UpdateRouteStatusAsPlannedTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var dataObjects = route4Me.GetRoutes(routeParameters, out ResultResponse resultResponse);
            var routeId = dataObjects.First().RouteID;

            var initialRouteStatusResponse = route4Me.GetRouteStatus(routeId, out var err1);

            RouteStatusResponse updatedRouteStatusResponse = route4Me.UpdateRouteStatus(routeId, new RouteStatusRequest()
            {
                Latitude = 50,
                Longitude = 60,
                Status = RouteStatus.Started.Description(),
                EventTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            }, out var err2);

            var updatedRouteStatusResponse2 = route4Me.UpdateRouteStatusAsPlanned(new UpdateRouteStatusAsPlannedParameters()
            {
                RouteIds = new string[] { routeId }
            }, out var err3);

            var routeStatusResponse = route4Me.GetRouteStatus(routeId, out var err4);

            Assert.That(updatedRouteStatusResponse2.GetType(), Is.EqualTo(typeof(UpdateRouteStatusAsPlannedResponse)));
        }

        [Test]
        public void SetRouteStopStatusTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 1,
                Offset = 15
            };

            var routeDestinationId = _tdr.SD10Stops_route.Addresses.First().RouteDestinationId;
            var result = route4Me.SetRouteStopStatus(new SetRouteStopStatusParameters()
            {
                DestinationIds = new long[] { routeDestinationId.Value },
                Status = RouteStopStatus.Failed.Description()
            }, out var err3);


            Assert.That(result.GetType(), Is.EqualTo(typeof(StatusResponse)));
        }
    }
}