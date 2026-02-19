using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void GetSingleRouteWithExtendedDetailsTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            // Get route ID from test data
            var routeId = _tdr.SD10Stops_route.RouteID;

            // Run the query to get extended route details
            var response = route4Me.GetRoute(routeId, out ResultResponse resultResponse);

            Assert.NotNull(response);
            Assert.That(response.GetType(), Is.EqualTo(typeof(GetRouteResponse)));
            Assert.NotNull(response.Data);
            Assert.That(response.Data.GetType(), Is.EqualTo(typeof(DataObjectRouteExtended)));
            Assert.That(response.Data.RouteID, Is.EqualTo(routeId));

            // Verify extended fields are populated
            Assert.NotNull(response.Data.Addresses, "Addresses should not be null");
            Assert.True(response.Data.Addresses.Length > 0, "Addresses array should not be empty");
            Assert.NotNull(response.Data.Parameters, "Parameters should not be null");
        }

        [Test]
        public async Task GetSingleRouteWithExtendedDetailsAsyncTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            // Get route ID from test data
            var routeId = _tdr.SD10Stops_route.RouteID;

            // Run the async query to get extended route details
            var result = await route4Me.GetRouteAsync(routeId);

            Assert.NotNull(result);
            Assert.NotNull(result.Item1);
            Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(GetRouteResponse)));
            Assert.NotNull(result.Item1.Data);
            Assert.That(result.Item1.Data.GetType(), Is.EqualTo(typeof(DataObjectRouteExtended)));
            Assert.That(result.Item1.Data.RouteID, Is.EqualTo(routeId));

            // Verify extended fields are populated
            Assert.NotNull(result.Item1.Data.Addresses, "Addresses should not be null");
            Assert.True(result.Item1.Data.Addresses.Length > 0, "Addresses array should not be empty");
            Assert.NotNull(result.Item1.Data.Parameters, "Parameters should not be null");
        }

        [Test]
        [Ignore("This test uses deprecated methods and will be removed in a future version.")]
        [Obsolete]
        public void GetAllRoutesWithPaginationTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Page = 1,
                PerPage = 20
            };

            var dataObjects = route4Me.GetAllRoutesWithPagination(routeParameters, out _);

            Assert.That(dataObjects.Data.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        [Ignore("This test uses deprecated methods and will be removed in a future version.")]
        [Obsolete]
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

            Assert.That(dataObjects.Data.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        [Ignore("This test uses deprecated methods and will be removed in a future version.")]
        [Obsolete]
        public void GetRouteDataTableWithoutElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] { "2021-02-01", "2021-02-01" }
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

            Assert.That(dataObjects.Data.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        [Ignore("This test uses deprecated methods and will be removed in a future version.")]
        [Obsolete]
        public void GetRouteDatatableWithElasticSearchTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeParameters = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 20,
                Filters = new RouteFilterParametersFilters
                {
                    ScheduleDate = new[] { "2021-02-01", "2021-02-01" }
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

            Assert.That(dataObjects.Data.GetType(), Is.EqualTo(typeof(DataObjectRoute[])));
        }

        [Test]
        [Ignore("This test uses deprecated methods and will be removed in a future version.")]
        [Obsolete]
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

        [Test]
        public void UpdateRouteCustomDataTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeId = _tdr.SD10Stops_route.RouteID;

            var customData = new Dictionary<string, string>
            {
                { "priority", "high" },
                { "region", "west" },
                { "notes", "Handle with care" }
            };

            var result = route4Me.UpdateRouteCustomData(routeId, customData, out ResultResponse resultResponse);

            Assert.NotNull(result, "UpdateRouteCustomData returned null. " +
                                   (resultResponse?.Messages != null
                                       ? string.Join("; ", resultResponse.Messages)
                                       : ""));
            Assert.That(result.GetType(), Is.EqualTo(typeof(Dictionary<string, string>)));
        }

        [Test]
        public async Task UpdateRouteCustomDataAsyncTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeId = _tdr.SD10Stops_route.RouteID;

            var customData = new Dictionary<string, string>
            {
                { "priority", "high" },
                { "region", "west" }
            };

            var result = await route4Me.UpdateRouteCustomDataAsync(routeId, customData);

            Assert.NotNull(result);
            Assert.NotNull(result.Item1, "UpdateRouteCustomDataAsync returned null.");
            Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(Dictionary<string, string>)));
        }

        [Test]
        [Obsolete]
        public void GetRouteCustomDataTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeId = _tdr.SD10Stops_route.RouteID;

            // First, set custom data on the route using the dedicated endpoint
            var customData = new Dictionary<string, string>
            {
                { "priority", "high" },
                { "region", "west" }
            };

            route4Me.UpdateRouteCustomData(routeId, customData, out _);

            // Now, retrieve the custom data via the dedicated endpoint
            var retrievedCustomData = route4Me.GetRouteCustomData(routeId, out ResultResponse resultResponse);

            Assert.NotNull(retrievedCustomData, "GetRouteCustomData returned null. " +
                                                (resultResponse?.Messages != null
                                                    ? string.Join("; ", resultResponse.Messages)
                                                    : ""));
            Assert.That(retrievedCustomData.Count, Is.GreaterThan(0));
        }

        [Test]
        [Obsolete]
        public async Task GetRouteCustomDataAsyncTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var routeId = _tdr.SD10Stops_route.RouteID;

            // First, set custom data on the route using the dedicated endpoint
            var customData = new Dictionary<string, string>
            {
                { "priority", "high" },
                { "region", "west" }
            };

            await route4Me.UpdateRouteCustomDataAsync(routeId, customData);

            // Now, retrieve the custom data via the dedicated endpoint
            var result = await route4Me.GetRouteCustomDataAsync(routeId);

            Assert.NotNull(result);
            Assert.NotNull(result.Item1, "GetRouteCustomDataAsync returned null custom data.");
            Assert.That(result.Item1.Count, Is.GreaterThan(0));
        }
    }
}