using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.Routes;

[TestFixture]
public class DynamicInsertTests
{
    private static readonly string CApiKey = ApiKeys.ActualApiKey;

    private static TestDataRepository s_tdr;
    private static List<string> s_lsOptimizationIDs;

    [SetUp]
    public void Setup()
    {
        s_lsOptimizationIDs = new List<string>();
        s_tdr = new TestDataRepository();

        var result = s_tdr.RunOptimizationSingleDriverRoute10Stops();

        Assert.True(result, "Single Driver 10 Stops generation failed.");
        Assert.True(s_tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.");

        s_lsOptimizationIDs.Add(s_tdr.SD10Stops_optimization_problem_id);
    }

    [TearDown]
    public void TearDown()
    {
        var optimizationResult = s_tdr.RemoveOptimization(s_lsOptimizationIDs.ToArray());
        Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
    }

    [Test]
    public void DynamicInsertRouteAddresses_WithRouteIds_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = route4Me.DynamicInsertRouteAddresses(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddresses returned null");
        Assert.That(result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Length, 0, "No matched routes returned");

        // Verify the returned route
        var matchedRoute = result.First();
        Assert.NotNull(matchedRoute.RouteId, "RouteId should not be null");
        Assert.NotNull(matchedRoute.RouteName, "RouteName should not be null");
        Assert.IsNotNull(matchedRoute.RecommendedInsertionStopNumber,
            "RecommendedInsertionStopNumber should not be null");
        Assert.Greater(matchedRoute.RecommendedInsertionStopNumber.Value, 0,
            "RecommendedInsertionStopNumber should be greater than 0");
        Assert.IsNotNull(matchedRoute.NewDistance, "NewDistance should not be null");
        Assert.IsNotNull(matchedRoute.OldDistance, "OldDistance should not be null");
        Assert.GreaterOrEqual(matchedRoute.NewDistance.Value, matchedRoute.OldDistance.Value,
            "NewDistance should be greater than or equal to OldDistance");
        Assert.IsNotNull(matchedRoute.PercentageDistanceIncrease,
            "PercentageDistanceIncrease should not be null");
        Assert.GreaterOrEqual(matchedRoute.PercentageDistanceIncrease.Value, 0,
            "PercentageDistanceIncrease should be non-negative");
    }

    [Test]
    public async Task DynamicInsertRouteAddressesAsync_WithRouteIds_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = await route4Me.DynamicInsertRouteAddressesAsync(dynamicInsertParams);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddressesAsync returned null");
        Assert.NotNull(result.Item1, "Result data should not be null");
        Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Item1.Length, 0, "No matched routes returned");

        // Verify the returned route
        var matchedRoute = result.Item1.First();
        Assert.NotNull(matchedRoute.RouteId, "RouteId should not be null");
        Assert.NotNull(matchedRoute.RouteName, "RouteName should not be null");
        Assert.IsNotNull(matchedRoute.RecommendedInsertionStopNumber,
            "RecommendedInsertionStopNumber should not be null");
        Assert.Greater(matchedRoute.RecommendedInsertionStopNumber.Value, 0,
            "RecommendedInsertionStopNumber should be greater than 0");
        Assert.IsNotNull(matchedRoute.NewDistance, "NewDistance should not be null");
        Assert.IsNotNull(matchedRoute.OldDistance, "OldDistance should not be null");
        Assert.GreaterOrEqual(matchedRoute.NewDistance.Value, matchedRoute.OldDistance.Value,
            "NewDistance should be greater than or equal to OldDistance");
        Assert.IsNotNull(matchedRoute.PercentageDistanceIncrease,
            "PercentageDistanceIncrease should not be null");
        Assert.GreaterOrEqual(matchedRoute.PercentageDistanceIncrease.Value, 0,
            "PercentageDistanceIncrease should be non-negative");
    }

    [Test]
    public void DynamicInsertRouteAddresses_WithRouteIds_OptimalAnywhere_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.OptimalAnywhere.Description(),
            LookupResultsLimit = 5,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = route4Me.DynamicInsertRouteAddresses(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddresses returned null");
        Assert.That(result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Length, 0, "No matched routes returned");
    }

    [Test]
    public void DynamicInsertRouteAddresses_WithRouteIds_RecommendByDuration_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Duration.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = route4Me.DynamicInsertRouteAddresses(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddresses returned null");
        Assert.That(result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Length, 0, "No matched routes returned");

        // Verify time-related fields are populated
        var matchedRoute = result.First();
        Assert.IsNotNull(matchedRoute.NewTime, "NewTime should not be null");
        Assert.Greater(matchedRoute.NewTime.Value, 0, "NewTime should be greater than 0");
        Assert.IsNotNull(matchedRoute.PercentageTimeIncrease,
            "PercentageTimeIncrease should not be null");
        Assert.GreaterOrEqual(matchedRoute.PercentageTimeIncrease.Value, 0,
            "PercentageTimeIncrease should be non-negative");
    }

    [Test]
    public void DynamicInsertRouteAddresses_WithRouteIds_EndOfRoute_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.EndOfRoute.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = route4Me.DynamicInsertRouteAddresses(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddresses returned null");
        Assert.That(result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Length, 0, "No matched routes returned");
    }

    [Test]
    public void DynamicInsertRouteAddresses_WithRouteIds_BeginningOfRoute_ShouldReturnMatchedRoutes()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.BeginningOfRoute.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };

        // Act
        var result = route4Me.DynamicInsertRouteAddresses(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddresses returned null");
        Assert.That(result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(result.Length, 0, "No matched routes returned");
    }

    [Test]
    public void DynamicInsertRouteAddressesJob_WithRouteIds_ShouldReturnJobEnvelope()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = BuildRequest();

        // Act
        var dispatch = route4Me.DynamicInsertRouteAddressesJob(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        // Assert - the 202 envelope carries job_id, status_url and result_url
        Assert.NotNull(dispatch, "DynamicInsertRouteAddressesJob returned null");
        Assert.IsNotEmpty(dispatch.JobId, "job_id should be present in the 202 body");
        Assert.IsNotEmpty(dispatch.StatusUrl, "status_url should be present in the 202 body");
        Assert.IsNotEmpty(dispatch.ResultUrl, "result_url should be present in the 202 body");
    }

    [Test]
    public async Task DynamicInsertRouteAddressesJobAsync_WithRouteIds_ShouldReturnJobEnvelope()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = BuildRequest();

        // Act
        var result = await route4Me.DynamicInsertRouteAddressesJobAsync(dynamicInsertParams);

        // Assert
        Assert.NotNull(result, "DynamicInsertRouteAddressesJobAsync returned null");
        Assert.NotNull(result.Item1, "Job envelope should not be null");
        Assert.IsNotEmpty(result.Item1.JobId, "job_id should be present in the 202 body");
        Assert.IsNotEmpty(result.Item1.StatusUrl, "status_url should be present in the 202 body");
        Assert.IsNotEmpty(result.Item1.ResultUrl, "result_url should be present in the 202 body");
        // The job id is also surfaced from the Location header (third tuple element).
        Assert.IsNotEmpty(result.Item3, "job id should be extracted from the Location header");
    }

    [Test]
    public void DynamicInsertJob_FullFlow_DispatchPollResult_ShouldMatchSyncShape()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var dynamicInsertParams = BuildRequest();

        // Act - dispatch
        var dispatch = route4Me.DynamicInsertRouteAddressesJob(
            dynamicInsertParams,
            out ResultResponse resultResponse);

        Assert.NotNull(dispatch, "Dispatch returned null");
        Assert.IsNotEmpty(dispatch.JobId, "job_id should be present");

        // Act - poll status, then fetch result once terminal
        DynamicInsertJobResult jobResult = null;

        for (var attempt = 0; attempt < 30; attempt++)
        {
            var status = route4Me.GetDynamicInsertJobStatus(dispatch.JobId, out resultResponse);
            Assert.NotNull(status, "Status response should not be null");

            jobResult = route4Me.GetDynamicInsertJobResult(dispatch.JobId, out resultResponse);

            if (jobResult?.Result != null)
            {
                break;
            }

            Thread.Sleep(2000);
        }

        // Assert - the result payload matches the synchronous endpoint's shape
        Assert.NotNull(jobResult, "Job result was not produced in time");
        Assert.IsTrue(jobResult.Status, "Job result status should be true");
        Assert.That(jobResult.Result.GetType(), Is.EqualTo(typeof(DynamicInsertMatchedRoute[])));
        Assert.Greater(jobResult.Result.Length, 0, "No matched routes returned");

        var matchedRoute = jobResult.Result.First();
        Assert.NotNull(matchedRoute.RouteId, "RouteId should not be null");
        Assert.NotNull(matchedRoute.RouteName, "RouteName should not be null");
        Assert.IsNotNull(matchedRoute.RecommendedInsertionStopNumber,
            "RecommendedInsertionStopNumber should not be null");
    }

    [Test]
    public void GetDynamicInsertJobResult_UnknownJobId_ShouldReturnFailure()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act - an unknown/expired job id must not yield a result payload
        var jobResult = route4Me.GetDynamicInsertJobResult(
            "00000000-0000-0000-0000-000000000000",
            out ResultResponse resultResponse);

        // Assert
        Assert.IsTrue(jobResult?.Result == null, "Unknown job id should not return a result payload");
    }

    private DynamicInsertRequest BuildRequest()
    {
        return new DynamicInsertRequest()
        {
            RouteIds = new[] { s_tdr.SD10Stops_route.RouteID },
            Latitude = 33.1296514,
            Longitude = -83.2485687,
            InsertMode = DynamicInsertMode.OptimalAfterLastVisited.Description(),
            LookupResultsLimit = 3,
            RecommendBy = DynamicInsertRecomendBy.Distance.Description(),
            MaxIncreasePercentAllowed = 500
        };
    }
}