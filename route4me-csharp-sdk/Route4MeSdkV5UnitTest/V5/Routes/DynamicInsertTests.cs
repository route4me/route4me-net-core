using System.Collections.Generic;
using System.Linq;
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
}