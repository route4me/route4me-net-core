using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4
{
    public class ActivityFeedTests : FactAttribute, IClassFixture<ActivityFeedFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;

        private readonly ActivityFeedFixture _fixture;

        public ActivityFeedTests(ActivityFeedFixture fixture, IServer server)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            Route4MeApi4Service r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _fixture = fixture;
        }

        [FactSkipable]
        public async Task LogCustomActivityTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoute10Stops is null.");

            string message = "Test User Activity " + DateTime.Now.ToString();

            var activity = new Activity()
            {
                ActivityType = "user_message",
                ActivityMessage = message,
                RouteId = routeId
            };

            var response = await r4mController
                .LogCustomActivity(activity);

            Assert.NotNull(response);
            Assert.IsType<Route4MeSDK.DataTypes.StatusResponse>(response.Item1);
            Assert.True(response.Item1.Status, $"LogCustomActivityTest failed.");
        }

        [FactSkipable]
        public async Task GetRouteTeamActivitiesTest()
        {
            string routeId = _fixture.tdr.SD10Stops_route_id;
            Assert.True(routeId != null, "routeId_SingleDriverRoute10Stops is null.");

            var activityParameters = new ActivityParameters()
            {
                RouteId = routeId,
                Team = "true",
                Limit = 10,
                Offset = 0
            };

            var response = await r4mController
                .GetActivityFeed(activityParameters);

            Assert.NotNull(response);
            Assert.IsType<GetActivitiesResponse>(response.Item1);
            Assert.True(response.Item1.Total > 0);
        }

        [FactSkipable]
        public async Task GetActivitiesTest()
        {
            var activityParameters = new ActivityParameters()
            {
                Limit = 10,
                Offset = 0
            };

            var response = await r4mController
                .GetActivityFeed(activityParameters);

            Assert.NotNull(response);
            Assert.IsType<GetActivitiesResponse>(response.Item1);
            Assert.True(response.Item1.Total > 0);
        }

        [FactSkipable]
        public async Task GetActivitiesByMemberTest()
        {
            Assert.True(_fixture.memberId!=null, "cannot retrieve first user");

            var activityParameters = new ActivityParameters()
            {
                MemberId = Convert.ToInt32(_fixture.memberId),
                Offset = 0,
                Limit = 10
            };

            var response = await r4mController
                .GetActivityFeed(activityParameters);

            Assert.NotNull(response);
            Assert.IsType<GetActivitiesResponse>(response.Item1);
            Assert.True(response.Item1.Total > 0);
        }

        [FactSkipable]
        public async Task GetLastActivities()
        {
            var activitiesAfterTime = DateTime.Now - (new TimeSpan(7, 0, 0, 0));

            activitiesAfterTime = new DateTime(
                                        activitiesAfterTime.Year,
                                        activitiesAfterTime.Month,
                                        activitiesAfterTime.Day,
                                        0, 0, 0);

            uint uiActivitiesAfterTime = (uint)Route4MeSDK.R4MeUtils
                                            .ConvertToUnixTimestamp(activitiesAfterTime);

            var activityParameters = new ActivityParameters()
            {
                Limit = 10,
                Offset = 0,
                Start = uiActivitiesAfterTime
            };

            var response = await r4mController
                .GetActivityFeed(activityParameters);

            Assert.NotNull(response);
            Assert.IsType<GetActivitiesResponse>(response.Item1);
            Assert.True(response.Item1.Total > 0);
        }

        [Theory]
        [InlineData("area-updated")]
        [InlineData("area-added")]
        [InlineData("area-removed")]
        [InlineData("delete-destination")]
        [InlineData("insert-destination")]
        [InlineData("mark-destination-departed")]
        [InlineData("destination-out-sequence")]
        [InlineData("update-destinations")]
        [InlineData("driver-arrived-early")]
        [InlineData("driver-arrived-late")]
        [InlineData("driver-arrived-on-time")]
        [InlineData("geofence-entered")]
        [InlineData("geofence-left")]
        [InlineData("mark-destination-visited")]
        [InlineData("member-created")]
        [InlineData("member-deleted")]
        [InlineData("member-modified")]
        [InlineData("move-destination")]
        [InlineData("note-insert")]
        [InlineData("route-delete")]
        [InlineData("route-optimized")]
        [InlineData("route-owner-changed")]
        [InlineData("order-created")]
        [InlineData("order-updated")]
        [InlineData("order-deleted")]
        [InlineData("unapproved-to-execute")]
        [InlineData("route-update")]
        [InlineData("user_message")]
        public async Task SearchActivitiesByTypeTest(string _activityType)
        {
            var activityParameters = new ActivityParameters { ActivityType = _activityType };

            var response = await r4mController
                .GetActivityFeed(activityParameters);

            Assert.NotNull(response);
            Assert.IsType<GetActivitiesResponse>(response.Item1);
        }
    }
}
