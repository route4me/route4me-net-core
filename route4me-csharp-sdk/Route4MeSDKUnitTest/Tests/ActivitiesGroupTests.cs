using System;
using System.Collections.Generic;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class ActivitiesGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;

        [OneTimeSetUp]
        public void ActivitiesGroupInitialize()
        {
            _lsOptimizationIDs = new List<string>();

            _tdr = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();

            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");

            Assert.IsTrue(
                _tdr.SD10Stops_route.Addresses.Length > 0,
                "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SD10Stops_optimization_problem_id);
        }

        [Test]
        public void LogCustomActivityTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var message = "Test User Activity " + DateTime.Now;

            var activity = new Activity
            {
                ActivityType = "user_message",
                ActivityMessage = message,
                RouteId = routeId
            };

            // Run the query
            var added = route4Me.LogCustomActivity(activity, out var errorString);

            Assert.IsTrue(added, "LogCustomActivityTest failed. " + errorString);
        }

        [Test]
        public void GetRouteTeamActivitiesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeId = _tdr.SD10Stops_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null.");

            var activityParameters = new ActivityParameters
            {
                RouteId = routeId,
                Team = "true",
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "GetRouteTeamActivitiesTest failed. " + errorString);
        }

        [Test]
        public void GetActivitiesTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                Limit = 10,
                Offset = 0
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "GetActivitiesTest failed. " + errorString);
        }

        [Test]
        public void GetActivitiesByMemberTest()
        {
            if (c_ApiKey == ApiKeys.DemoApiKey) return;

            var route4Me = new Route4MeManager(c_ApiKey);

            var parameters = new GenericParameters();

            var response = route4Me.GetUsers(parameters, out var userErrorString);

            Assert.That(response.Results, Is.InstanceOf<MemberResponseV4[]>(), "GetActivitiesByMemberTest failed - cannot get users");
            Assert.IsTrue(
                response.Results.Length > 0,
                "Cannot retrieve more than 0 users");

            var activityParameters = new ActivityParameters
            {
                MemberId = response.Results[0].MemberId != null
                    ? Convert.ToInt32(response.Results[0].MemberId)
                    : -1,
                Offset = 0,
                Limit = 10
            };

            // Run the query
            var activities = route4Me.GetActivities(activityParameters, out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "GetActivitiesByMemberTest failed. " + errorString);
        }

        [Test]
        public void GetLastActivities()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activitiesAfterTime = DateTime.Now - new TimeSpan(7, 0, 0, 0);

            activitiesAfterTime = new DateTime(
                activitiesAfterTime.Year,
                activitiesAfterTime.Month,
                activitiesAfterTime.Day,
                0, 0, 0);

            var uiActivitiesAfterTime = (uint) R4MeUtils
                .ConvertToUnixTimestamp(activitiesAfterTime);

            var activityParameters = new ActivityParameters
            {
                Limit = 10,
                Offset = 0,
                Start = uiActivitiesAfterTime
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            foreach (var activity in activities)
            {
                var activityTime = activity.ActivityTimestamp != null
                    ? (uint) activity.ActivityTimestamp
                    : 0;
                Assert.IsTrue(
                    activityTime >= uiActivitiesAfterTime,
                    "GetLastActivities failed. " + errorString);
            }
        }

        [Test]
        public void SearchAreaUpdatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-updated"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchAreaUpdatedTest failed. " + errorString);
        }

        [Test]
        public void SearchAreaAddedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-added"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchAreaAddedTest failed. " + errorString);
        }

        [Test]
        public void SearchAreaRemovedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters {ActivityType = "area-removed"};

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchAreaRemovedTest failed. " + errorString);
        }

        [Test]
        public void SearchDestinationDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "delete-destination"
                //RouteId = "5C15E83A4BE005BCD1537955D28D51D7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDestinationDeletedTest failed. " + errorString);
        }

        [Test]
        public void SearchDestinationInsertedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination"
                //RouteId = "87B8873BAEA4E09942C68E2C92A9C4B7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDestinationInsertedTest failed. " + errorString);
        }

        [Test]
        public void SearchDestinationMarkedAsDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed"
                //RouteId = "03CEF546324F727239ABA69EFF3766E1"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDestinationMarkedAsDepartedTest failed. " + errorString);
        }

        [Test]
        public void SearchDestinationOutSequenceTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "destination-out-sequence"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDestinationOutSequenceTest failed. " + errorString);
        }

        [Test]
        public void SearchDestinationUpdatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "update-destinations"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDestinationUpdatedTest failed. " + errorString);
        }

        [Test]
        public void SearchDriverArrivedEarlyTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-early"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDriverArrivedEarlyTest failed. " + errorString);
        }

        [Test]
        public void SearchDriverArrivedLateTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-late"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDriverArrivedLateTest failed. " + errorString);
        }

        [Test]
        public void SearchDriverArrivedOnTimeTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "driver-arrived-on-time"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchDriverArrivedOnTimeTest failed. " + errorString);
        }

        [Test]
        public void SearchGeofenceEnteredTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "geofence-entered"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchGeofenceEnteredTest failed. " + errorString);
        }

        [Test]
        public void SearchGeofenceLeftTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "geofence-left"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchGeofenceLeftTest failed. " + errorString);
        }

        [Test]
        public void SearchInsertDestinationAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "insert-destination"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchInsertDestinationAllTest failed. " + errorString);
        }

        [Test]
        public void SearchMarkDestinationDepartedAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-departed"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMarkDestinationDepartedAllTest failed. " + errorString);
        }

        [Test]
        public void SearchMarkDestinationVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "mark-destination-visited"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMarkDestinationVisitedTest failed. " + errorString);
        }

        [Test]
        public void SearchMemberCreatedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-created"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMemberCreatedTest failed. " + errorString);
        }

        [Test]
        public void SearchMemberDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-deleted"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMemberDeletedTest failed. " + errorString);
        }

        [Test]
        public void SearchMemberModifiedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "member-modified"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMemberModifiedTest failed. " + errorString);
        }

        [Test]
        public void SearchMoveDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "move-destination"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchMoveDestinationTest failed. " + errorString);
        }

        [Test]
        public void SearchNoteInsertedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "note-insert"
                //RouteId = "C3E7FD2F8775526674AE5FD83E25B88A"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchNoteInsertedTest failed. " + errorString);
        }

        [Test]
        public void SearchNoteInsertedAllTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "note-insert"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchNoteInsertedAllTest failed. " + errorString);
        }

        [Test]
        public void SearchRouteDeletedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-delete"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchRouteDeletedTest failed. " + errorString);
        }

        [Test]
        public void SearchRouteOptimizedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-optimized"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchRouteOptimizedTest failed. " + errorString);
        }

        [Test]
        public void SearchRouteOwnerChanged()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var activityParameters = new ActivityParameters
            {
                ActivityType = "route-owner-changed"
                //RouteId = "5C15E83A4BE005BCD1537955D28D51D7"
            };

            // Run the query
            var activities = route4Me.GetActivities(
                activityParameters,
                out var errorString);

            Assert.That(activities, Is.InstanceOf<Activity[]>(), "SearchRouteOwnerChanged failed. " + errorString);
        }

        [OneTimeTearDown]
        public void ActivitiesGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");
        }
    }
}