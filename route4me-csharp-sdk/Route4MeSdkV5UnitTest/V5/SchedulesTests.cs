using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Schedules;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Schedules;

namespace Route4MeSdkV5UnitTest.V5
{
    [TestFixture]
    public class SchedulesTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository tdr;
        private static List<string> lsOptimizationIDs;

        [SetUp]
        public void Setup()
        {
            lsOptimizationIDs = new List<string>();

            tdr = new TestDataRepository();

            var result = tdr.RunOptimizationSingleDriverRoute10Stops();
            Assert.True(result, "Single Driver 10 Stops generation failed.");

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id);
        }

        [TearDown]
        public void TearDown()
        {
            var optimizationResult = tdr.RemoveOptimization(lsOptimizationIDs.ToArray());

            Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Test]
        public void GetScheduleListWithPaginationTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var routeParameters = new SchedulesParametersPaginated
            {
                Page = 1,
                PerPage = 15,
                WithPagination = true
            };

            var scheduleListWithPagination = route4Me.GetScheduleListWithPagination(routeParameters, out ResultResponse resultResponse);

            Assert.That(scheduleListWithPagination.GetType(), Is.EqualTo(typeof(ScheduleListWithPagination)));
        }

        [Test]
        public void GetScheduleListTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleList = route4Me.GetSchedulesList(out ResultResponse resultResponse);

            Assert.That(scheduleList.GetType(), Is.EqualTo(typeof(ScheduleList)));
        }

        [Test]
        public void CreateScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var response = route4Me.CreateSchedule(schedule, out ResultResponse resultResponse);

            Assert.That(response.Data.Single().Name, Is.EqualTo(scheduleName));
        }

        [Test]
        public void GetScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var response = route4Me.CreateSchedule(schedule, out ResultResponse resultResponse);

            var scheduleList = route4Me.GetSchedule(response.Data.Single().ScheduleUid, out resultResponse);

            Assert.That(scheduleList.Data.Single().ScheduleUid, Is.EqualTo(response.Data.Single().ScheduleUid));
        }

        [Test]
        public void UpdateScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName,
                AdvancedScheduleIntervalDays = 1
            };

            var response = route4Me.CreateSchedule(schedule, out ResultResponse resultResponse);

            var scheduleList = route4Me.GetSchedule(response.Data.Single().ScheduleUid, out resultResponse);

            schedule = scheduleList.Data.Single();
            schedule.Name += " updated";
            var scheduleUid = schedule.ScheduleUid;
            schedule.ScheduleUid = null;

            scheduleList = route4Me.UpdateSchedule(scheduleUid, schedule, out resultResponse);

            Assert.That(scheduleList.Data.Single().Name.EndsWith("updated"), Is.True);
            Assert.That(scheduleUid, Is.EqualTo(scheduleList.Data.Single().ScheduleUid));
        }

        [Test]
        public void DeleteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var response = route4Me.CreateSchedule(schedule, out ResultResponse resultResponse);

            var scheduleList = route4Me.GetSchedule(response.Data.Single().ScheduleUid, out resultResponse);

            schedule = scheduleList.Data.Single();

            scheduleList = route4Me.DeleteSchedule(new DeleteScheduleParameters(){ ScheduleUid = schedule.ScheduleUid, WithRoutes = false }, out resultResponse);

            Assert.That(scheduleList.Data.Length, Is.EqualTo(1));
        }

        [Test]
        public void GetRouteScheduleListWithPaginationTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var routeParameters = new SchedulesParametersPaginated
            {
                Page = 1,
                PerPage = 15,
                WithPagination = true
            };

            var routeScheduleListWithPagination = route4Me.GetRouteScheduleListWithPagination(routeParameters, out ResultResponse resultResponse);

            Assert.That(routeScheduleListWithPagination.GetType(), Is.EqualTo(typeof(RouteScheduleListWithPagination)));
        }

        [Test]
        public void GetRouteScheduleListTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleList = route4Me.GetRouteSchedulesList(out ResultResponse resultResponse);

            Assert.That(scheduleList.GetType(), Is.EqualTo(typeof(RouteScheduleList)));
        }

        [Test]
        public void CreateRouteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = scheduleResponse.Data.Single().ScheduleUid },
                out resultResponse);

            Assert.That(createRouteScheduleResponse.Data.Length, Is.GreaterThan(0));
        }

        [Test]
        public void GetRouteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = scheduleResponse.Data.Single().ScheduleUid },
                out resultResponse);

            var response = route4Me.GetRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);

            Assert.That(response.Data.Single().Schedule.Name, Is.EqualTo(scheduleName));
        }

        [Test]
        public void UpdateRouteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName1 = "My schedule 1";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName1
            };

            var scheduleResponse1 = route4Me.CreateSchedule(schedule, out var resultResponse);

            var scheduleName2 = "My schedule 2";
            Schedule schedule2 = new Schedule()
            {
                Name = scheduleName2
            };

            var scheduleResponse2 = route4Me.CreateSchedule(schedule2, out resultResponse);

            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = scheduleResponse1.Data.Single().ScheduleUid },
                out resultResponse);


            var routeSchedule = createRouteScheduleResponse.Data.Single();
            routeSchedule.ScheduleUid = scheduleResponse2.Data.Single().ScheduleUid;

            var updateRouteScheduleResponse = route4Me.UpdateRouteSchedule(tdr.SD10Stops_route_id,
                routeSchedule, out resultResponse);

            var response = route4Me.GetRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);

            Assert.That(response.Data.Single(x => x.ScheduleUid == scheduleResponse2.Data.Single().ScheduleUid).Schedule.Name, Is.EqualTo(scheduleName2));
        }

        [Test]
        public void DeleteRouteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = scheduleResponse.Data.Single().ScheduleUid },
                out resultResponse);

            var response = route4Me.GetRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);

            Assert.That(response.Data.Single().Schedule.Name, Is.EqualTo(scheduleName));

            var deleteResponse = route4Me.DeleteRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);
            Assert.That(deleteResponse.Status, Is.True);

            response = route4Me.GetRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);

            Assert.That(response.Data.Length, Is.Zero);
        }


        [Test]
        public void GetRouteSchedulePreviewTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = scheduleResponse.Data.Single().ScheduleUid },
                out resultResponse);

            var response = route4Me.GetRouteSchedulePreview(tdr.SD10Stops_route_id, new RouteSchedulePreviewParameters() { Start = "2020-09-20", End = "2023-09-20" }, out resultResponse);

            Assert.That(response.Dates.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ReplaceRouteScheduleTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule 1";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createdSchedule = scheduleResponse.Data.Single();
            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = createdSchedule.ScheduleUid },
                out resultResponse);


            var routeSchedule = createRouteScheduleResponse.Data.Single();
            routeSchedule.ScheduleUid = createdSchedule.ScheduleUid;

            var replaceRouteScheduleRequest = new ReplaceRouteScheduleBodyRequest();
            replaceRouteScheduleRequest.Name = createdSchedule.Name;
            replaceRouteScheduleRequest.AdvancedScheduleIntervalDays = 2;
            replaceRouteScheduleRequest.AdvancedInterval = 10;
            replaceRouteScheduleRequest.MemberId = routeSchedule.MemberId;
            replaceRouteScheduleRequest.ScheduleBlacklist = createdSchedule.ScheduleBlacklist;
            replaceRouteScheduleRequest.ScheduleData = createdSchedule.ScheduleData;
            replaceRouteScheduleRequest.Timezone = createdSchedule.Timezone;
            replaceRouteScheduleRequest.VehicleId = routeSchedule.VehicleId;


            var updateRouteScheduleResponse = route4Me.ReplaceRouteSchedule(tdr.SD10Stops_route_id,
                replaceRouteScheduleRequest, out resultResponse);

            Assert.That(updateRouteScheduleResponse.Data.Length, Is.EqualTo(1));

            var response = route4Me.GetRouteSchedule(tdr.SD10Stops_route_id, out resultResponse);

            Assert.That(response.Data.Single().Schedule.AdvancedInterval, Is.EqualTo(10));
        }

        [Test]
        public void RouteScheduleIsCopyTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule 1";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createdSchedule = scheduleResponse.Data.Single();
            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                { RouteId = tdr.SD10Stops_route_id, ScheduleUid = createdSchedule.ScheduleUid },
                out resultResponse);


            var response = route4Me.IsRouteScheduleCopied(tdr.SD10Stops_route_id, out resultResponse);
            Assert.That(response.Status, Is.False);
        }

        [Test] //TODO: response structure is different: [] instead of object
        public void RouteScheduleGetCopiesTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var scheduleName = "My schedule 1";
            Schedule schedule = new Schedule()
            {
                Name = scheduleName
            };

            var scheduleResponse = route4Me.CreateSchedule(schedule, out var resultResponse);

            var createdSchedule = scheduleResponse.Data.Single();
            var createRouteScheduleResponse = route4Me.CreateRouteSchedule(
                new RouteSchedule()
                    { RouteId = tdr.SD10Stops_route_id, ScheduleUid = createdSchedule.ScheduleUid },
                out resultResponse);


            var response = route4Me.GetRouteScheduleCopies(new GetRouteScheduleCopiesRequest(){ RouteId = tdr.SD10Stops_route_id, ScheduleId = createdSchedule.ScheduleUid }, out resultResponse);
            Assert.That(response.ScheduledRoutes.Length, Is.EqualTo(0));
        }

        [Test]
        public void CreateMasterRouteAsyncTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var request = new CreateMasterRouteRequest();
            request.RouteName = "my route name";
            request.RouteId = tdr.SD10Stops_route_id;
            var response = route4Me.CreateMasterRoute(request, out ResultResponse resultResponse);

            Assert.That(response.Status);
            Assert.That(response.Data, Is.Null);
        }

        [Test]
        public void CreateMasterRouteSyncTest()
        {
            var route4Me = new SchedulesManagerV5(CApiKey);

            var request = new CreateMasterRouteRequest();
            request.RouteName = "my route name 2";
            request.RouteId = tdr.SD10Stops_route_id;
            request.Sync = true;
            var response = route4Me.CreateMasterRoute(request, out ResultResponse resultResponse);

            Assert.That(response.Status);
            Assert.That(response.Data, Is.Not.Null);
        }
    }
}
