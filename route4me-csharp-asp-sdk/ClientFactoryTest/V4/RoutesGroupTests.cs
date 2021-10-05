using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4
{
    public class RoutesGroupTests : IClassFixture<RoutesGroupFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;

        private Route4MeApi4Service r4mApi4Service;

        private readonly ITestOutputHelper _output;

        private readonly RoutesGroupFixture _fixture;
        private readonly VehicleFixture _vehicleFixture;


        public RoutesGroupTests(RoutesGroupFixture fixture, IServer server, ITestOutputHelper output)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _output = output;

            _fixture = fixture;
            _output = output;

            _vehicleFixture = new VehicleFixture();
        }

        [FactSkipable]
        public async Task GetRoutesTest()
        {
            var routeParameters = new RouteParametersQuery()
            {
                Limit = 5,
                Offset = 0
            };

            var response = await r4mController
                .GetRoutes(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute[]>(response.Item1);
            Assert.True(response.Item1.Length > 0);
        }

        [FactSkipable]
        public async Task GetRouteTest()
        {
            var routeParameters = new RouteParametersQuery()
            {
                RouteId = _fixture.tdr.SD10Stops_route_id
            };

            var response = await r4mController
                .GetRoute(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute>(response.Item1);
            Assert.True(response.Item1.RouteId == _fixture.tdr.SD10Stops_route_id);
        }

        [FactSkipable]
        public async Task GetRoutesByIDsTest()
        {
            var routeParameters = new RouteParametersQuery()
            {
                RouteId = _fixture.tdr.SD10Stops_route_id + "," + _fixture.tdr.SDRT_route_id
            };

            var response = await r4mController
                .GetRoutes(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute[]>(response.Item1);
            Assert.True(response.Item1.Length == 2);
        }

        [FactSkipable]
        public async Task GetRoutesFromDateRangeTest()
        {
            var days10 = new TimeSpan(10, 0, 0, 0);

            var routeParameters = new RouteParametersQuery()
            {
                StartDate = (DateTime.Now - days10).ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd")
            };

            var response = await r4mController
                .GetRoutes(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute[]>(response.Item1);
            Assert.True(response.Item1.Length > 0);
        }

        [FactSkipable]
        public async Task GetRouteDirectionsTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Directions = true
            };

            routeParameters.Directions = true;

            var response = await r4mController
                .GetRoute(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute>(response.Item1);
            Assert.True(response.Item1.RouteId == _fixture.tdr.SD10Stops_route_id);

            Assert.IsType<Direction[]>(response.Item1.Directions);
        }

        [FactSkipable]
        public async Task GetRoutePathPointsTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId
            };

            routeParameters.RoutePathOutput = RoutePathOutput.Points.ToString();

            var response = await r4mController
                .GetRoute(routeParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute>(response.Item1);
            Assert.True(response.Item1.RouteId == _fixture.tdr.SD10Stops_route_id);

            Assert.IsType<DirectionPathPoint[]>(response.Item1.Path);
        }

        [FactSkipable]
        public async Task ResequenceRouteDestinationsTest()
        {
            var route = _fixture.tdr.SD10Stops_route;

            Assert.True(route!=null, "Route for the test Route Destinations Resequence is null.");

            var rParams = new RouteParametersQuery()
            {
                RouteId = route.RouteId
            };

            var lsAddresses = new List<Address>();
            Address address1 = route.Addresses[2];
            Address address2 = route.Addresses[3];

            address1.SequenceNo = 4;
            address2.SequenceNo = 3;

            lsAddresses.Add(address1);
            lsAddresses.Add(address2);

            var response = await r4mController
                .ManuallyResequenceRoute(rParams, lsAddresses.ToArray());

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute>(response.Item1);
            Assert.True(response.Item1.RouteId == _fixture.tdr.SD10Stops_route_id);
        }

        [FactSkipable]
        public async Task ResequenceReoptimizeRouteTest()
        {
            string route_id = _fixture.tdr.SD10Stops_route_id;

            Assert.True(route_id!=null, "rote_id is null.");

            var routeParams = new RouteParametersQuery()
            {
                RouteId = route_id,
                ReOptimize = true,
                Remaining = false,
                DeviceType = DeviceType.Web.Description()
            };

            var response = await r4mController
                .ReoptimizeRoute(routeParams);

            Assert.NotNull(response);
            Assert.IsType<DataObjectRoute>(response.Item1);
            Assert.True(response.Item1.RouteId == _fixture.tdr.SD10Stops_route_id);
        }

        [FactSkipable]
        public async Task ReoptimizeRemainingStopsTest()
        {
            var route = _fixture.tdr2.SDRT_route;

            var visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[1].RouteDestinationId,
                IsVisited = true
            };

            var markVisitedResponse = await r4mController
                .MarkAddressVisited(visitedParams);

            Assert.True(markVisitedResponse != null, "MarkAddressVisitedTest.");

            visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[2].RouteDestinationId,
                IsVisited = true
            };

            var markVisitedResponse2 = await r4mController
                .MarkAddressVisited(visitedParams);

            Assert.True(markVisitedResponse2 != null, "MarkAddressVisitedTest.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = route.RouteId,
                ReOptimize = true,
                Remaining = true
            };

            var updatedRoute = await r4mController
                .UpdateRoute(routeParameters);

            Assert.NotNull(updatedRoute);
            Assert.IsType<DataObjectRoute>(updatedRoute.Item1);
            Assert.True(updatedRoute.Item1.RouteId == _fixture.tdr2.SDRT_route_id);
        }

        [FactSkipable]
        public async Task UnlinkRouteFromOptimizationTest()
        {
            string routeId = _fixture.tdr2.SDRT_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeDuplicateParameters = new RouteParametersQuery()
            {
                DuplicateRoutesId = new string[] { routeId }
            };

            var duplicateResult = await r4mController
                .DuplicateRoute(routeDuplicateParameters);

            Assert.True((duplicateResult?.Item1 ?? null)!=null, $"Cannot duplicate a route {routeId ?? "null"}. ");
            Assert.True(duplicateResult.Item1.Status, $"Cannot duplicate a route {routeId ?? "null"}.");
            Assert.True((duplicateResult?.Item1?.RouteIDs?.Length ?? 0) > 0, $"Cannot duplicate a route {routeId ?? "nll"}.");

            Thread.Sleep(5000);

            var duplicatedRoute = await r4mController.GetRoute(
            new RouteParametersQuery() { RouteId = duplicateResult.Item1.RouteIDs[0] });

            Assert.True(duplicatedRoute!=null, "Cannot retrieve the duplicated route.");
            Assert.IsType<DataObjectRoute>(duplicatedRoute.Item1);

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = duplicateResult.Item1.RouteIDs[0],
                UnlinkFromMasterOptimization = true
            };

            // Run the query
            var unlinkedRoute = await r4mController.UpdateRoute(routeParameters);

            Assert.True(unlinkedRoute!=null, "UnlinkRouteFromOptimizationTest failed.");
            Assert.True((unlinkedRoute?.Item1?.OptimizationProblemId ?? null)==null,
                "Optimization problem ID of the unlinked route is not null.");
        }

        [FactSkipable]
        public async Task UpdateRouteTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var parametersNew = new RouteParameters()
            {
                RouteName = "New name of the route",
                Metric = Metric.Manhattan
            };

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = parametersNew
            };

            var updatedRoute = await r4mController
                .UpdateRoute(routeParameters);
            
            Assert.True(updatedRoute!=null, 
                $"UpdateRouteTest failed. Error: {Environment.NewLine}"+
                r4mApi4Service.ErrorResponseToString(updatedRoute.Item2));
            
        }

        [FactSkipable]
        public async Task UpdateWholeRouteTest()
        {
            string routeId = _fixture.tdr2.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(_fixture.tdr2.SD10Stops_route);

            #region // Notes, Custom Type Notes, Note File Uploading

            var customNotesResponse = await r4mController.GetAllCustomNoteTypes();

            var allCustomNotes = (customNotesResponse?.Item1 ?? null) != null && 
                                 customNotesResponse.Item1.GetType() == typeof(CustomNoteType[])
                                     ? (CustomNoteType[])customNotesResponse.Item1
                                     : null;

            string tempFilePath = null;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ClientFactoryTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            Thread.Sleep(1000);

            _fixture.tdr2.SD10Stops_route.Addresses[1].Notes = new AddressNote[] {
                new AddressNote()
                {
                    NoteId = -1,
                    RouteId = _fixture.tdr2.SD10Stops_route.RouteId,
                    Latitude = _fixture.tdr2.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = _fixture.tdr2.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content",
                    CustomTypes = allCustomNotes.Length>0
                                    ? new AddressCustomNote[]
                                    {
                                        new AddressCustomNote()
                                        {
                                            NoteCustomTypeID = allCustomNotes[0].NoteCustomTypeID.ToString(),
                                            NoteCustomValue = allCustomNotes[0].NoteCustomTypeValues[0]
                                        }
                                    }
                                    : null,
                    UploadUrl = tempFilePath
                }
             };

            var updatedRoute0 = await r4mController
                .UpdateRoute(_fixture.tdr2.SD10Stops_route, initialRoute);

            Assert.True((updatedRoute0?.Item1?.Addresses[1]?.Notes?.Length ?? 0) == 1,
                "UpdateRouteTest failed: cannot create a note");

            if (allCustomNotes.Length > 0)
                Assert.True((updatedRoute0?.Item1.Addresses[1]?.Notes[0]?.CustomTypes?.Length ?? 0) == 1,
                    "UpdateRouteTest failed: cannot create a custom type note");

            Assert.True((updatedRoute0?.Item1?.Addresses[1]?.Notes[0]?.UploadId?.Length ?? 0) == 32,
                "UpdateRouteTest failed: cannot create a custom type note");

            #endregion

            _fixture.tdr2.SD10Stops_route.ApprovedForExecution = true;
            _fixture.tdr2.SD10Stops_route.Parameters.RouteName += " Edited";
            _fixture.tdr2.SD10Stops_route.Parameters.Metric = Metric.Manhattan;

            _fixture.tdr2.SD10Stops_route.Addresses[1].AddressString += " Edited";
            _fixture.tdr2.SD10Stops_route.Addresses[1].Group = "Example Group";
            _fixture.tdr2.SD10Stops_route.Addresses[1].CustomerPo = "CPO 456789";
            _fixture.tdr2.SD10Stops_route.Addresses[1].InvoiceNo = "INO 789654";
            _fixture.tdr2.SD10Stops_route.Addresses[1].ReferenceNo = "RNO 313264";
            _fixture.tdr2.SD10Stops_route.Addresses[1].OrderNo = "ONO 654878";
            _fixture.tdr2.SD10Stops_route.Addresses[1].Notes = new AddressNote[] {
                new AddressNote()
                {
                    RouteDestinationId = -1,
                    RouteId = _fixture.tdr2.SD10Stops_route.RouteId,
                    Latitude = _fixture.tdr2.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = _fixture.tdr2.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content"
                }
             };

            _fixture.tdr2.SD10Stops_route.Addresses[2].SequenceNo = 5;
            var addressID = _fixture.tdr2.SD10Stops_route.Addresses[2].RouteDestinationId;

            var dataObjectResult = await r4mController
                .UpdateRoute(_fixture.tdr2.SD10Stops_route, initialRoute);

            Assert.True((dataObjectResult?.Item1 ?? null) != null, "UpdateWholeRouteTest failed");

            Assert.True(dataObjectResult.Item1.Addresses.Where(x => x.RouteDestinationId == addressID)
                .FirstOrDefault()
                .SequenceNo == 5, "UpdateWholeRouteTest failed  Cannot resequence addresses");

            Assert.True(_fixture.tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to true");
            Assert.True(dataObjectResult.Item1.Parameters.RouteName.Contains("Edited"),
                "UpdateRouteTest failed, the route name not changed.");
            Assert.True(dataObjectResult.Item1.Addresses[1].AddressString.Contains("Edited"),
                "UpdateRouteTest failed, second address name not changed.");

            Assert.True(dataObjectResult.Item1.Addresses[1].Group == "Example Group",
                "UpdateWholeRouteTest failed.");
            Assert.True(dataObjectResult.Item1.Addresses[1].CustomerPo == "CPO 456789",
                "UpdateWholeRouteTest failed.");
            Assert.True(dataObjectResult.Item1.Addresses[1].InvoiceNo == "INO 789654",
                "UpdateWholeRouteTest failed.");
            Assert.True(dataObjectResult.Item1.Addresses[1].ReferenceNo == "RNO 313264",
                "UpdateWholeRouteTest failed.");
            Assert.True(dataObjectResult.Item1.Addresses[1].OrderNo == "ONO 654878",
                "UpdateWholeRouteTest failed.");

            initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(_fixture.tdr2.SD10Stops_route);

            _fixture.tdr2.SD10Stops_route.ApprovedForExecution = false;
            _fixture.tdr2.SD10Stops_route.Addresses[1].Group = null;
            _fixture.tdr2.SD10Stops_route.Addresses[1].CustomerPo = null;
            _fixture.tdr2.SD10Stops_route.Addresses[1].InvoiceNo = null;
            _fixture.tdr2.SD10Stops_route.Addresses[1].ReferenceNo = null;
            _fixture.tdr2.SD10Stops_route.Addresses[1].OrderNo = null;

            dataObjectResult = await r4mController
                .UpdateRoute(_fixture.tdr2.SD10Stops_route, initialRoute);

            Assert.True((dataObjectResult?.Item1 ?? null) != null, "UpdateWholeRouteTest failed");

            Assert.False(
                _fixture.tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to false");
            Assert.True(
                dataObjectResult.Item1.Addresses[1].Group==null,
                "UpdateWholeRouteTest failed.");
            Assert.True(
                dataObjectResult.Item1.Addresses[1].CustomerPo==null,
                "UpdateWholeRouteTest failed.");
            Assert.True(
                dataObjectResult.Item1.Addresses[1].InvoiceNo==null,
                "UpdateWholeRouteTest failed.");
            Assert.True(
                dataObjectResult.Item1.Addresses[1].ReferenceNo==null,
                "UpdateWholeRouteTest failed.");
            Assert.True(
                dataObjectResult.Item1.Addresses[1].OrderNo==null,
                "UpdateWholeRouteTest failed.");

        }

        [FactSkipable]
        public async Task ChangeRouteDepoteTest()
        {
            string routeId = _fixture.tdr2.SDRT_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone<DataObjectRoute>(_fixture.tdr2.SDRT_route);

            Assert.True(_fixture.tdr2.SDRT_route.Addresses[0].IsDepot == true,
                "First address is not depot");
            Assert.True(_fixture.tdr2.SDRT_route.Addresses[1].IsDepot == false,
                "Second address is depot");

            _fixture.tdr2.SDRT_route.Addresses[0].IsDepot = false;
            int? addressId0 = _fixture.tdr2.SDRT_route.Addresses[0].RouteDestinationId;
            _fixture.tdr2.SDRT_route.Addresses[0].Alias = addressId0.ToString();
            initialRoute.Addresses[0].Alias = addressId0.ToString();
            _fixture.tdr2.SDRT_route.Addresses[1].IsDepot = true;
            int? addressId1 = _fixture.tdr2.SDRT_route.Addresses[1].RouteDestinationId;
            _fixture.tdr2.SDRT_route.Addresses[1].Alias = addressId1.ToString();
            initialRoute.Addresses[1].Alias = addressId1.ToString();

            var dataObjectResult = await r4mController.UpdateRoute(
            _fixture.tdr2.SDRT_route, initialRoute);

            Assert.True((dataObjectResult?.Item1 ?? null) != null, "Cannot update the route");

            var address0 = dataObjectResult.Item1.Addresses
            .Where(x => x.Alias == addressId0.ToString())
            .FirstOrDefault();

            var address1 = dataObjectResult.Item1.Addresses
                .Where(x => x.Alias == addressId1.ToString())
                .FirstOrDefault();

            Assert.True(address0.IsDepot == false, "First address is depot");
            Assert.True(address1.IsDepot == true, "Second address is not depot");
        }

        [FactSkipable]
        public async Task AssignVehicleToRouteTest()
        {
            string vehicleId = _vehicleFixture.removeVehicleIDs[0];

            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = new RouteParameters()
                {
                    VehicleId = vehicleId
                }
            };

            var route = await r4mController.UpdateRoute(routeParameters);

            Assert.True((route?.Item1 ?? null) != null, "Cannot update the route");

            Assert.IsType<VehicleV4Response>(route.Item1.Vehilce);

            _vehicleFixture.Dispose();
        }

        [FactSkipable]
        public async Task AssignMemberToRouteTest()
        {
            var membersResult = await r4mController.GetUsers(new GenericParameters());
            Assert.True((membersResult?.Item1 ?? null) != null, "Cannot retrieve the users");

            var members = membersResult.Item1;

            int randomNumber = (new Random()).Next(0, members.Results.Length - 1);
            var memberId = members.Results[randomNumber].MemberId != null
                ? Convert.ToInt32(members.Results[randomNumber].MemberId)
                : -1;

            Assert.True(memberId != -1,
                "AssignMemberToRouteTest failed - cannot retrieve random member ID");

            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null,
                "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = new RouteParameters()
                {
                    MemberId = memberId
                }
            };

            var updateRouteResult = await r4mController.UpdateRoute(routeParameters);
            Assert.True((updateRouteResult?.Item1 ?? null) != null, "Cannot update the route");

            var getRouteResult = await r4mController.GetRoute(
                                    new RouteParametersQuery() { RouteId = routeId });
            Assert.True((getRouteResult?.Item1 ?? null) != null, "Cannot retrieve the route");

            Assert.True(getRouteResult.Item1.MemberId == memberId,
                $"AssignMemberToRouteTest failed.{Environment.NewLine}" + 
                r4mApi4Service.ErrorResponseToString(getRouteResult.Item2));
        }

        [FactSkipable]
        public async Task UpdateRouteCustomDataTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            var routeDestionationId = _fixture.tdr.SD10Stops_route.Addresses[3].RouteDestinationId;

            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                RouteDestinationId = routeDestionationId
            };

            var customData = new Dictionary<string, string>()
            {
                {"animal", "lion" },
                {"bird", "budgie" }
            };

            var result = await r4mController.UpdateRouteCustomData(parameters, customData);

            Assert.True((result?.Item1 ?? null)!=null, 
                $"UpdateRouteCustomDataTest failed.{Environment.NewLine}" + 
                r4mApi4Service.ErrorResponseToString(result.Item2));
        }

        [FactSkipable]
        public async Task UpdateRouteAvoidanceZonesTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;

            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var parameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Parameters = new RouteParameters()
                {
                    AvoidanceZones = new string[]
                    {
                        "FAA49711A0F1144CE4E203DC18ABDFFB",
                        "9C48E8008E9865006336B99D3595E66A"
                    }
                }
            };

            var result = await r4mController.UpdateRoute(parameters);

            Assert.True((result?.Item1 ?? null)!=null,
                "UpdateRouteAvoidanceZonesTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(result.Item2));

            Assert.True((result?.Item1?.Parameters?.AvoidanceZones?.Length ?? 0) == 2,
                $"UpdateRouteAvoidanceZonesTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(result.Item2));
        }

        [FactSkipable]
        public async Task RouteOriginParameterTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                Original = true
            };

            var routeResult = await r4mController.GetRoute(routeParameters);

            Assert.True((routeResult?.Item1?.OriginalRoute ?? null)!=null,
                $"RouteOriginParameterTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(routeResult.Item2));
            
            Assert.IsType<DataObjectRoute>(routeResult.Item1.OriginalRoute);
        }

        [FactSkipable]
        public async Task ReoptimizeRouteTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ReOptimize = true
            };

            // Run the query
            var routeResult = await r4mController.UpdateRoute(routeParameters);

            Assert.True(routeResult!=null, 
                $"ReoptimizeRouteTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(routeResult.Item2));
        }

        [FactSkipable]
        public async Task DuplicateRouteTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId is null.");

            var routeParameters = new RouteParametersQuery()
            {
                DuplicateRoutesId = new string[] { routeId }
            };

            // Run the query
            var duplicateResult = await r4mController.DuplicateRoute(routeParameters);

            Assert.True((duplicateResult?.Item1 ?? null) != null, 
                $"DuplicateRouteTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(duplicateResult.Item2));
            Assert.IsType<DuplicateRouteResponse>(duplicateResult.Item1);
            Assert.True(duplicateResult.Item1.Status, "DuplicateRouteTest failed");

            var routeIdsToDelete = new List<string>();

            foreach (var id in duplicateResult.Item1.RouteIDs) routeIdsToDelete.Add(id);

            await r4mController.DeleteRoutes(routeIdsToDelete.ToArray());
        }

        [FactSkipable]
        public async Task ShareRouteTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId is null.");

            var routeParameters = new RouteParametersQuery()
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            string email = "regression.autotests+testcsharp123@gmail.com";

            // Run the query
            var result = await r4mController.RouteSharing(routeParameters, email);

            Assert.True((result?.Item1 ?? null) != null, 
                $"ShareRouteTest failed.{Environment.NewLine}" + 
                r4mApi4Service.ErrorResponseToString(result.Item2));
            Assert.True(result.Item1.Status, "ShareRouteTest failed.");
        }

        [FactSkipable]
        public async Task DeleteRoutesTest()
        {
            var routeIdsToDelete = new List<string>();

            bool result = _fixture.tdr.RunSingleDriverRoundTrip();

            Assert.True(result, "Single Driver Round Trip generation failed.");

            if (_fixture.tdr.SDRT_route_id != null)
                routeIdsToDelete.Add(_fixture.tdr.SDRT_route_id);

            // Run the query
            var deletedRouteResult = await r4mController
                .DeleteRoutes(routeIdsToDelete.ToArray());

            Assert.True((deletedRouteResult?.Item1 ?? null) != null,
                $"DeleteRoutesTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(deletedRouteResult.Item2));
            Assert.IsType<DeleteRouteResponse>(deletedRouteResult.Item1);
            Assert.True(deletedRouteResult.Item1.Deleted);
        }

        [FactSkipable]
        public async Task RunRouteSlowdownTest()
        {
            // Prepare the addresses
            var addresses = new Address[]
              {
			#region Addresses

			new Address() { AddressString = "754 5th Ave New York, NY 10019",
                            Alias         = "Bergdorf Goodman",
                            IsDepot       = true,
                            Latitude      = 40.7636197,
                            Longitude     = -73.9744388,
                            Time          = 0 },

            new Address() { AddressString = "717 5th Ave New York, NY 10022",
							//Alias         = "Giorgio Armani",
							Latitude      = 40.7669692,
                            Longitude     = -73.9693864,
                            Time          = 0 },

            new Address() { AddressString = "888 Madison Ave New York, NY 10014",
							//Alias         = "Ralph Lauren Women's and Home",
							Latitude      = 40.7715154,
                            Longitude     = -73.9669241,
                            Time          = 0 },

            new Address() { AddressString = "1011 Madison Ave New York, NY 10075",
                            Alias         = "Yigal Azrou'l",
                            Latitude      = 40.7772129,
                            Longitude     = -73.9669,
                            Time          = 0 },

             new Address() { AddressString = "440 Columbus Ave New York, NY 10024",
                            Alias         = "Frank Stella Clothier",
                            Latitude      = 40.7808364,
                            Longitude     = -73.9732729,
                            Time          = 0 },

            new Address() { AddressString = "324 Columbus Ave #1 New York, NY 10023",
                            Alias         = "Liana",
                            Latitude      = 40.7803123,
                            Longitude     = -73.9793079,
                            Time          = 0 },

            new Address() { AddressString = "110 W End Ave New York, NY 10023",
                            Alias         = "Toga Bike Shop",
                            Latitude      = 40.7753077,
                            Longitude     = -73.9861529,
                            Time          = 0 },

            new Address() { AddressString = "555 W 57th St New York, NY 10019",
                            Alias         = "BMW of Manhattan",
                            Latitude      = 40.7718005,
                            Longitude     = -73.9897716,
                            Time          = 0 },

            new Address() { AddressString = "57 W 57th St New York, NY 10019",
                            Alias         = "Verizon Wireless",
                            Latitude      = 40.7558695,
                            Longitude     = -73.9862019,
                            Time          = 0 },

                  #endregion
              };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.TSP,
                RouteName = "Single Driver Round Trip",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 1,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),

                Slowdowns = new SlowdownParams(15, 20)
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObjectResult = await r4mController.RunOptimization(
                                        optimizationParameters);

            Assert.True((dataObjectResult?.Item1 ?? null) !=null,
                $"RunSingleDriverRoundTripfailed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(dataObjectResult.Item2));

            Assert.Equal(
                15, 
                dataObjectResult.Item1.Parameters.RouteServiceTimeMultiplier);
            Assert.Equal(
                20,
                dataObjectResult.Item1.Parameters.RouteTimeMultiplier);

            _fixture.tdr.RemoveOptimization(
                new string[] { dataObjectResult.Item1.OptimizationProblemId }
                );
        }

    }
}
