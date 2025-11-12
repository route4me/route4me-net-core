using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class RoutesGroupTests
    {
        [OneTimeSetUp]
        public void RoutesGroupInitialize()
        {
            _lsOptimizationIDs = new List<string>();
            _lsVehicleIDs = new List<string>();

            _tdr = new TestDataRepository();
            _tdr2 = new TestDataRepository();
            _tdr3 = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();
            var result2 = _tdr2.RunOptimizationSingleDriverRoute10Stops();
            var result3 = _tdr3.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");
            Assert.IsTrue(result2, "Single Driver 10 Stops generation failed.");
            Assert.IsTrue(result3, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(_tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.IsTrue(_tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");
            Assert.IsTrue(_tdr3.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SD10Stops_optimization_problem_id);
            _lsOptimizationIDs.Add(_tdr2.SD10Stops_optimization_problem_id);
            _lsOptimizationIDs.Add(_tdr3.SDRT_optimization_problem_id);
        }

        [OneTimeTearDown]
        [Obsolete]
        public void RoutesGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the testing optimization problem failed.");

            if (_lsVehicleIDs.Count > 0)
            {
                var route4Me = new Route4MeManager(CApiKey);

                foreach (var vehId in _lsVehicleIDs)
                {
                    var vehicleParams = new VehicleV4Parameters
                    {
                        VehicleId = vehId
                    };

                    // Run the query
                    route4Me.DeleteVehicle(vehicleParams, out _);
                }

                _lsVehicleIDs.Clear();
            }

            _tdr = null;
            _tdr2 = null;
            _tdr3 = null;
        }

        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private TestDataRepository _tdr;
        private TestDataRepository _tdr2;
        private TestDataRepository _tdr3;
        private List<string> _lsOptimizationIDs;
        private List<string> _lsVehicleIDs;

        [Test]
        public void GetRoutesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                Limit = 20,
                Offset = 0
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.That(dataObjects, Is.InstanceOf<DataObjectRoute[]>(), "GetRoutesTest failed. " + errorString);
        }

        [Test]
        public void GetRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                RouteId = _tdr.SD10Stops_route_id
            };

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRouteTest failed. " + errorString);
        }

        [Test]
        public void GetRoutesByIDsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            #region // Retrieve first 3 routes

            var routesParameters = new RouteParametersQuery
            {
                Offset = 0,
                Limit = 3
            };

            var threeRoutes = route4Me.GetRoutes(routesParameters, out var errorString);

            #endregion

            #region // Retrieve 2 route by their IDs

            var routeParameters = new RouteParametersQuery
            {
                RouteId = threeRoutes[0].RouteId + "," + threeRoutes[1].RouteId
            };

            var twoRoutes = route4Me.GetRoutes(routeParameters, out errorString);

            #endregion

            Assert.That(twoRoutes, Is.InstanceOf<DataObjectRoute[]>(), "GetRoutesByIDsTest failed. " + errorString);

            Assert.IsTrue(twoRoutes.Length == 2, "GetRoutesByIDsTest failed");
        }

        [Test]
        public void GetRoutesFromDateRangeTest()
        {
            if (CApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(CApiKey);

            var routeParameters = new RouteParametersQuery
            {
                StartDate = "2019-08-01",
                EndDate = "2019-08-05"
            };

            // Run the query
            var dataObjects = route4Me.GetRoutes(routeParameters, out var errorString);

            Assert.That(dataObjects, Is.InstanceOf<DataObjectRoute[]>(),
                "GetRoutesFromDateRangeTest failed. " + errorString);
        }

        [Test]
        public void GetRouteDirectionsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Directions = true
            };

            routeParameters.Directions = true;

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRouteDirectionsTest failed. " + errorString);
        }

        [Test]
        public void GetRoutePathPointsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                RoutePathOutput = RoutePathOutput.Points.ToString()
            };

            // Run the query
            var dataObject = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "GetRoutePathPointsTest failed. " + errorString);
        }

        [Test]
        public void ResequenceRouteDestinationsTest()
        {
            var route = _tdr.SD10Stops_route;
            Assert.IsNotNull(
                route,
                "Route for the test Route Destinations Resequence is null.");

            var route4Me = new Route4MeManager(CApiKey);

            var rParams = new RouteParametersQuery
            {
                RouteId = route.RouteId
            };

            var lsAddresses = new List<Address>();
            var address1 = route.Addresses[2];
            var address2 = route.Addresses[3];

            address1.SequenceNo = 4;
            address2.SequenceNo = 3;

            lsAddresses.Add(address1);
            lsAddresses.Add(address2);

            var route1 = route4Me.ManuallyResequenceRoute(
                rParams,
                lsAddresses.ToArray(),
                out var errorString);

            Assert.IsNotNull(route1, "ResequenceRouteDestinationsTest failed. " + errorString);
        }

        [Test]
        public void ResequenceReoptimizeRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;

            Assert.IsNotNull(routeId, "rote_id is null.");

            var routeParams = new RouteParametersQuery
            {
                RouteId = routeId,
                ReOptimize = true,
                Remaining = false,
                DeviceType = DeviceType.Web.Description()
            };

            // Run the query
            var result = route4Me.ReoptimizeRoute(routeParams, out var errorString);

            Assert.IsNotNull(result, "ResequenceReoptimizeRouteTest failed.");

            Assert.That(result, Is.InstanceOf<DataObjectRoute>(),
                "ResequenceReoptimizeRouteTest failed. " + errorString);
        }

        [Test]
        public void ReoptimizeRemainingStopsTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var route = _tdr3.SDRT_route;

            var visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[1].RouteDestinationId,
                IsVisited = true
            };

            var result = route4Me.MarkAddressVisited(visitedParams, out var errorString);

            Assert.IsNotNull(result, "MarkAddressVisitedTest. " + errorString);

            visitedParams = new AddressParameters
            {
                RouteId = route.RouteId,
                AddressId = (int)route.Addresses[2].RouteDestinationId,
                IsVisited = true
            };

            result = route4Me.MarkAddressVisited(visitedParams, out errorString);

            Assert.IsNotNull(result, "MarkAddressVisitedTest. " + errorString);

            var routeParameters = new RouteParametersQuery
            {
                RouteId = route.RouteId,
                ReOptimize = true,
                Remaining = true
            };

            var updatedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            Assert.IsNotNull(updatedRoute, "Cannot update the route " + route.RouteId);
        }

        [Test]
        public void UnlinkRouteFromOptimizationTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr3.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeDuplicateParameters = new RouteParametersQuery
            {
                DuplicateRoutesId = new[] { routeId }
            };

            // Run the query
            var duplicateResult = route4Me.DuplicateRoute(routeDuplicateParameters, out var errorString);

            Assert.IsNotNull(duplicateResult, $"Cannot duplicate a route {routeId ?? "nll"}. " + errorString);
            Assert.IsTrue(duplicateResult.Status, $"Cannot duplicate a route {routeId ?? "nll"}.");
            Assert.IsTrue((duplicateResult?.RouteIDs?.Length ?? 0) > 0,
                $"Cannot duplicate a route {routeId ?? "nll"}.");

            Thread.Sleep(5000);

            var duplicatedRoute = route4Me.GetRoute(
                new RouteParametersQuery { RouteId = duplicateResult.RouteIDs[0] },
                out errorString);

            Assert.IsNotNull(duplicatedRoute, "Cannot retrieve the duplicated route.");
            Assert.That(duplicatedRoute, Is.InstanceOf<DataObjectRoute>(), "Cannot retrieve the duplicated route.");
            //Assert.IsNotNull(duplicatedRoute.OptimizationProblemId, "Optimization problem ID of the duplicated route is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = duplicateResult.RouteIDs[0],
                UnlinkFromMasterOptimization = true
            };

            // Run the query
            var unlinkedRoute = route4Me.UpdateRoute(routeParameters, out errorString);

            Assert.IsNotNull(
                unlinkedRoute,
                "UnlinkRouteFromOptimizationTest failed. " + errorString + Environment.NewLine + "Route ID: " +
                routeId);
            Assert.IsNull(
                unlinkedRoute?.OptimizationProblemId,
                "Optimization problem ID of the unlinked route is not null.");
        }

        [Test]
        public void UpdateRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var parametersNew = new RouteParameters
            {
                RouteName = "New name of the route",
                Metric = Metric.Manhattan
            };

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = parametersNew
            };

            var dataObject = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "UpdateRouteTest failed. " + errorString);
        }

        [Test]
        public void UpdateWholeRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr2.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone(_tdr2.SD10Stops_route);

            #region // Notes, Custom Type Notes, Note File Uploading

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out _);

            var allCustomNotes =
                customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[])
                    ? (CustomNoteType[])customNotesResponse
                    : null;

            string tempFilePath;

            using (var stream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    Console.WriteLine(tempFilePath);

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            _tdr2.SD10Stops_route.Addresses[1].Notes = new[]
            {
                new AddressNote
                {
                    NoteId = -1,
                    RouteId = _tdr2.SD10Stops_route.RouteId,
                    Latitude = _tdr2.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = _tdr2.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content",
                    CustomTypes = allCustomNotes.Length > 0
                        ? new[]
                        {
                            new AddressCustomNote
                            {
                                NoteCustomTypeID = allCustomNotes[0].NoteCustomTypeID.ToString(),
                                NoteCustomValue = allCustomNotes[0].NoteCustomTypeValues[0]
                            }
                        }
                        : null,
                    UploadUrl = tempFilePath
                }
            };

            var updatedRoute0 = route4Me.UpdateRoute(
                _tdr2.SD10Stops_route,
                initialRoute,
                out _);

            Assert.IsTrue(
                updatedRoute0.Addresses[1].Notes.Length == 1,
                "UpdateRouteTest failed: cannot create a note");

            if (allCustomNotes.Length > 0)
                Assert.IsTrue(
                    updatedRoute0.Addresses[1].Notes[0].CustomTypes.Length == 1,
                    "UpdateRouteTest failed: cannot create a custom type note");

            Assert.IsTrue(
                updatedRoute0.Addresses[1].Notes[0].UploadId.Length == 32,
                "UpdateRouteTest failed: cannot create a custom type note");

            #endregion

            _tdr2.SD10Stops_route.ApprovedForExecution = true;
            _tdr2.SD10Stops_route.Parameters.RouteName += " Edited";
            _tdr2.SD10Stops_route.Parameters.Metric = Metric.Manhattan;

            _tdr2.SD10Stops_route.Addresses[1].AddressString += " Edited";
            _tdr2.SD10Stops_route.Addresses[1].Group = "Example Group";
            _tdr2.SD10Stops_route.Addresses[1].CustomerPo = "CPO 456789";
            _tdr2.SD10Stops_route.Addresses[1].InvoiceNo = "INO 789654";
            _tdr2.SD10Stops_route.Addresses[1].ReferenceNo = "RNO 313264";
            _tdr2.SD10Stops_route.Addresses[1].OrderNo = "ONO 654878";
            _tdr2.SD10Stops_route.Addresses[1].Notes = new[]
            {
                new AddressNote
                {
                    RouteDestinationId = -1,
                    RouteId = _tdr.SD10Stops_route.RouteId,
                    Latitude = _tdr.SD10Stops_route.Addresses[1].Latitude,
                    Longitude = _tdr.SD10Stops_route.Addresses[1].Longitude,
                    ActivityType = "dropoff",
                    Contents = "C# SDK Test Content"
                }
            };

            _tdr2.SD10Stops_route.Addresses[2].SequenceNo = 5;
            var addressId = _tdr2.SD10Stops_route.Addresses[2].RouteDestinationId;

            var dataObject = route4Me.UpdateRoute(_tdr2.SD10Stops_route, initialRoute, out var errorString);

            Assert.IsTrue(dataObject.Addresses.Where(x => x.RouteDestinationId == addressId)
                .FirstOrDefault()
                .SequenceNo == 5, "UpdateWholeRouteTest failed  Cannot resequence addresses");

            Assert.IsTrue(
                _tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to true");
            Assert.IsNotNull(
                dataObject,
                "UpdateRouteTest failed. " + errorString);
            Assert.IsTrue(
                dataObject.Parameters.RouteName.Contains("Edited"),
                "UpdateRouteTest failed, the route name not changed.");
            Assert.IsTrue(
                dataObject.Addresses[1].AddressString.Contains("Edited"),
                "UpdateRouteTest failed, second address name not changed.");

            Assert.IsTrue(
                dataObject.Addresses[1].Group == "Example Group",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].CustomerPo == "CPO 456789",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].InvoiceNo == "INO 789654",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].ReferenceNo == "RNO 313264",
                "UpdateWholeRouteTest failed.");
            Assert.IsTrue(
                dataObject.Addresses[1].OrderNo == "ONO 654878",
                "UpdateWholeRouteTest failed.");

            initialRoute = R4MeUtils.ObjectDeepClone(_tdr2.SD10Stops_route);

            _tdr2.SD10Stops_route.ApprovedForExecution = false;
            _tdr2.SD10Stops_route.Addresses[1].Group = null;
            _tdr2.SD10Stops_route.Addresses[1].CustomerPo = null;
            _tdr2.SD10Stops_route.Addresses[1].InvoiceNo = null;
            _tdr2.SD10Stops_route.Addresses[1].ReferenceNo = null;
            _tdr2.SD10Stops_route.Addresses[1].OrderNo = null;

            dataObject = route4Me.UpdateRoute(_tdr2.SD10Stops_route, initialRoute, out errorString);

            Assert.IsFalse(
                _tdr2.SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to false");
            Assert.IsNull(
                dataObject.Addresses[1].Group,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].CustomerPo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].InvoiceNo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].ReferenceNo,
                "UpdateWholeRouteTest failed.");
            Assert.IsNull(
                dataObject.Addresses[1].OrderNo,
                "UpdateWholeRouteTest failed.");
        }

        [Test]
        public void ChangeRouteDepoteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr3.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var initialRoute = R4MeUtils.ObjectDeepClone(_tdr3.SDRT_route);

            Assert.IsTrue(
                _tdr3.SDRT_route.Addresses[0].IsDepot == true,
                "First address is not depot");
            Assert.IsTrue(
                _tdr3.SDRT_route.Addresses[1].IsDepot == false,
                "Second address is depot");

            _tdr3.SDRT_route.Addresses[0].IsDepot = false;
            var addressId0 = _tdr3.SDRT_route.Addresses[0].RouteDestinationId;
            _tdr3.SDRT_route.Addresses[0].Alias = addressId0.ToString();
            initialRoute.Addresses[0].Alias = addressId0.ToString();
            _tdr3.SDRT_route.Addresses[1].IsDepot = true;
            var addressId1 = _tdr3.SDRT_route.Addresses[1].RouteDestinationId;
            _tdr3.SDRT_route.Addresses[1].Alias = addressId1.ToString();
            initialRoute.Addresses[1].Alias = addressId1.ToString();

            var dataObject = route4Me.UpdateRoute(
                _tdr3.SDRT_route, initialRoute,
                out _);

            var address0 = dataObject.Addresses
                .Where(x => x.Alias == addressId0.ToString())
                .FirstOrDefault();

            var address1 = dataObject.Addresses
                .Where(x => x.Alias == addressId1.ToString())
                .FirstOrDefault();

            Assert.IsTrue(address0.IsDepot == false, "First address is depot");
            Assert.IsTrue(address1.IsDepot == true, "Second address is not depot");
        }

        [Test]
        [Obsolete]
        public void AssignVehicleToRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var vehicleGroup = new VehiclesGroupTests();
            var vehicles = vehicleGroup.GetVehiclesList();

            if ((vehicles?.Length ?? 0) < 1)
            {
                var newVehicle = new VehicleV4Parameters
                {
                    VehicleName = "Ford Transit Test 6",
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = vehicleGroup.CreateVehicle(newVehicle);
                _lsVehicleIDs.Add(vehicle.VehicleGuid);
            }

            var vehicleId = (vehicles?.Length ?? 0) > 0
                ? vehicles[new Random().Next(0, vehicles.Length - 1)].VehicleId
                : _lsVehicleIDs[0];

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    VehicleId = vehicleId
                }
            };

            var route = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.That(route.Vehilce, Is.InstanceOf<VehicleV4Response>(),
                "AssignVehicleToRouteTest failed. " + errorString);
        }

        [Test]
        [Obsolete]
        public void AssignMemberToRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var members = route4Me.GetUsers(new GenericParameters(), out var errorString);

            var randomNumber = new Random().Next(0, members.Results.Length - 1);
            var memberId = members.Results[randomNumber].MemberId != null
                ? Convert.ToInt32(members.Results[randomNumber].MemberId)
                : -1;

            Assert.IsTrue(
                memberId != -1,
                "AssignMemberToRouteTest failed - cannot retrieve random member ID");

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(
                routeId,
                "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    MemberId = memberId
                }
            };

            route4Me.UpdateRoute(routeParameters, out errorString);

            var route = route4Me.GetRoute(
                new RouteParametersQuery { RouteId = routeId },
                out errorString);

            Assert.IsTrue(
                route.MemberId == memberId,
                "AssignMemberToRouteTest failed. " + errorString);
        }

        [Test]
        public void UpdateRouteCustomDataTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            var routeDestionationId = _tdr.SD10Stops_route.Addresses[3].RouteDestinationId;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var parameters = new RouteParametersQuery
            {
                RouteId = routeId,
                RouteDestinationId = routeDestionationId
            };

            var customData = new Dictionary<string, string>
            {
                { "animal", "lion" },
                { "bird", "budgie" }
            };

            var result = route4Me.UpdateRouteCustomData(parameters, customData, out var errorString);

            Assert.IsNotNull(result, "UpdateRouteCustomDataTest failed. " + errorString);
        }

        [Test]
        public void UpdateRouteAvoidanceZonesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null..");

            var parameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Parameters = new RouteParameters
                {
                    AvoidanceZones = new[]
                    {
                        "FAA49711A0F1144CE4E203DC18ABDFFB",
                        "9C48E8008E9865006336B99D3595E66A"
                    }
                }
            };

            var result = route4Me.UpdateRoute(parameters, out var errorString);

            Assert.IsNotNull(result, "UpdateRouteAvoidanceZonesTest failed. " + errorString);

            Assert.IsTrue(
                result.Parameters.AvoidanceZones.Length == 2,
                "UpdateRouteAvoidanceZonesTest failed. " + errorString);
        }

        [Test]
        public void RouteOriginParameterTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                Original = true
            };

            var route = route4Me.GetRoute(routeParameters, out var errorString);

            Assert.IsNotNull(
                route,
                "RouteOriginParameterTest failed. " + errorString);
            Assert.IsNotNull(
                route.OriginalRoute,
                "RouteOriginParameterTest failed. " + errorString);
            Assert.That(route.OriginalRoute, Is.InstanceOf<DataObjectRoute>(),
                "RouteOriginParameterTest failed. " + errorString);
        }

        [Test]
        public void ReoptimizeRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                ReOptimize = true
            };

            // Run the query
            var dataObject = route4Me.UpdateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(dataObject, "ReoptimizeRouteTest failed. " + errorString);
        }

        [Test]
        public void DuplicateRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId is null.");

            var routeParameters = new RouteParametersQuery
            {
                DuplicateRoutesId = new[] { routeId }
            };

            // Run the query
            var result = route4Me.DuplicateRoute(routeParameters, out var errorString);

            Assert.IsNotNull(result, "DuplicateRouteTest failed. " + errorString);
            Assert.That(result, Is.InstanceOf<DuplicateRouteResponse>(), "DeleteRoutesTest failed. " + errorString);
            Assert.IsTrue(result.Status, "DuplicateRouteTest failed");

            var routeIdsToDelete = new List<string>();

            foreach (var id in result.RouteIDs) routeIdsToDelete.Add(id);

            route4Me.DeleteRoutes(routeIdsToDelete.ToArray(), out _);
        }

        [Test]
        public void ShareRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId is null.");

            var routeParameters = new RouteParametersQuery
            {
                RouteId = routeId,
                ResponseFormat = "json"
            };

            var email = "regression.autotests+testcsharp123@gmail.com";

            // Run the query
            var result = route4Me.RouteSharing(routeParameters, email, out var errorString);

            Assert.IsTrue(result, "ShareRouteTest failed. " + errorString);
        }

        [Test]
        public void DeleteRoutesTest()
        {
            var routeIdsToDelete = new List<string>();

            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            if (_tdr.SDRT_route_id != null)
                routeIdsToDelete.Add(_tdr.SDRT_route_id);

            var route4Me = new Route4MeManager(CApiKey);

            // Run the query
            var deletedRouteIds = route4Me.DeleteRoutes(
                routeIdsToDelete.ToArray(),
                out var errorString);

            Assert.That(deletedRouteIds, Is.InstanceOf<string[]>(), "DeleteRoutesTest failed. " + errorString);
        }

        [Test]
        public void RunRouteSlowdownTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            // Prepare the addresses
            Address[] addresses =
            {
                #region Addresses

                new Address
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new Address
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    //Alias         = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new Address
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    //Alias         = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                new Address
                {
                    AddressString = "1011 Madison Ave New York, NY 10075",
                    Alias = "Yigal Azrou'l",
                    Latitude = 40.7772129,
                    Longitude = -73.9669,
                    Time = 0
                },

                new Address
                {
                    AddressString = "440 Columbus Ave New York, NY 10024",
                    Alias = "Frank Stella Clothier",
                    Latitude = 40.7808364,
                    Longitude = -73.9732729,
                    Time = 0
                },

                new Address
                {
                    AddressString = "324 Columbus Ave #1 New York, NY 10023",
                    Alias = "Liana",
                    Latitude = 40.7803123,
                    Longitude = -73.9793079,
                    Time = 0
                },

                new Address
                {
                    AddressString = "110 W End Ave New York, NY 10023",
                    Alias = "Toga Bike Shop",
                    Latitude = 40.7753077,
                    Longitude = -73.9861529,
                    Time = 0
                },

                new Address
                {
                    AddressString = "555 W 57th St New York, NY 10019",
                    Alias = "BMW of Manhattan",
                    Latitude = 40.7718005,
                    Longitude = -73.9897716,
                    Time = 0
                },

                new Address
                {
                    AddressString = "57 W 57th St New York, NY 10019",
                    Alias = "Verizon Wireless",
                    Latitude = 40.7558695,
                    Longitude = -73.9862019,
                    Time = 0
                },

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters
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

            var optimizationParameters = new OptimizationParameters
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.IsNotNull(
                dataObject,
                "RunSingleDriverRoundTripfailed. " + errorString);

            Assert.AreEqual(
                dataObject.Parameters.RouteServiceTimeMultiplier,
                15,
                "Cannot set service time slowdown");
            Assert.AreEqual(
                dataObject.Parameters.RouteTimeMultiplier,
                20,
                "Cannot set route travel time slowdown");

            _tdr.RemoveOptimization(new[] { dataObject.OptimizationProblemId });
        }
    }
}