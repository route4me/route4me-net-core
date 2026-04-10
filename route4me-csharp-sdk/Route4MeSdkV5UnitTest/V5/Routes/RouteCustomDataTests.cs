using System.Collections.Generic;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.Routes;

[TestFixture]
public class RouteCustomDataTests
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
    public void GetRouteCustomData_ForExistingRoute_ShouldSucceed()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        // Act
        var result = route4Me.GetRouteCustomDataDedicated(routeId, out ResultResponse resultResponse);

        // Assert
        // A fresh route may have no custom data; both null and empty dict are valid
        Assert.IsNull(resultResponse, "GetRouteCustomDataDedicated should not return an error for a valid route");
    }

    [Test]
    public async Task GetRouteCustomDataAsync_ForExistingRoute_ShouldSucceed()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        // Act
        var response = await route4Me.GetRouteCustomDataDedicatedAsync(routeId);

        // Assert
        Assert.IsNull(response.Item2, "GetRouteCustomDataDedicatedAsync should not return an error for a valid route");
    }

    [Test]
    public void UpdateRouteCustomData_WithValidData_ShouldReturnUpdatedData()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var customData = new Dictionary<string, string>
        {
            { "project_code", "PRJ-001" },
            { "priority", "high" },
            { "notes", "Test route custom data" }
        };

        // Act
        var result = route4Me.UpdateRouteCustomData(routeId, customData, out ResultResponse resultResponse);

        // Assert
        Assert.IsNotNull(result, "UpdateRouteCustomData should return updated data");
        Assert.IsNull(resultResponse, "UpdateRouteCustomData should not return an error");
        Assert.IsTrue(result.ContainsKey("project_code"), "Response should contain 'project_code'");
        Assert.AreEqual("PRJ-001", result["project_code"], "project_code value should match");
        Assert.IsTrue(result.ContainsKey("priority"), "Response should contain 'priority'");
        Assert.AreEqual("high", result["priority"], "priority value should match");
    }

    [Test]
    public async Task UpdateRouteCustomDataAsync_WithValidData_ShouldReturnUpdatedData()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var customData = new Dictionary<string, string>
        {
            { "async_key1", "async_value1" },
            { "async_key2", "async_value2" }
        };

        // Act
        var response = await route4Me.UpdateRouteCustomDataAsync(routeId, customData);

        // Assert
        Assert.IsNotNull(response.Item1, "UpdateRouteCustomDataAsync should return updated data");
        Assert.IsNull(response.Item2, "UpdateRouteCustomDataAsync should not return an error");
        Assert.IsTrue(response.Item1.ContainsKey("async_key1"), "Response should contain 'async_key1'");
        Assert.AreEqual("async_value1", response.Item1["async_key1"], "async_key1 value should match");
    }

    [Test]
    public void UpdateRouteCustomData_ReplacesPreviousData()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        // First update: set two keys
        var initialData = new Dictionary<string, string>
        {
            { "key_to_keep", "keep_value" },
            { "key_to_remove", "remove_value" }
        };
        route4Me.UpdateRouteCustomData(routeId, initialData, out _);

        // Second update: only send one key (the other should be removed)
        var updatedData = new Dictionary<string, string>
        {
            { "key_to_keep", "keep_value" }
        };

        // Act
        var result = route4Me.UpdateRouteCustomData(routeId, updatedData, out ResultResponse resultResponse);

        // Assert
        Assert.IsNotNull(result, "UpdateRouteCustomData should return result");
        Assert.IsNull(resultResponse, "UpdateRouteCustomData should not return an error");
        Assert.IsTrue(result.ContainsKey("key_to_keep"), "Remaining key should be present");
        Assert.IsFalse(result.ContainsKey("key_to_remove"), "Removed key should not be present");
    }

    [Test]
    public void GetBulkRouteCustomData_WithValidRouteIds_ShouldReturnCollection()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;
        var routeIds = new[] { routeId };

        // Seed custom data so the route appears in the bulk response
        var seedData = new Dictionary<string, string> { { "bulk_test_key", "bulk_test_value" } };
        route4Me.UpdateRouteCustomData(routeId, seedData, out _);

        // Act
        var result = route4Me.GetBulkRouteCustomData(routeIds, out ResultResponse resultResponse);

        // Assert
        Assert.IsNotNull(result, "GetBulkRouteCustomData should return a collection");
        Assert.IsNull(resultResponse, "GetBulkRouteCustomData should not return an error");
        Assert.IsNotNull(result.Data, "Collection Data property should not be null");
        Assert.IsTrue(result.Data.ContainsKey(routeId), "Collection should contain the requested route ID");
    }

    [Test]
    public async Task GetBulkRouteCustomDataAsync_WithValidRouteIds_ShouldReturnCollection()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;
        var routeIds = new[] { routeId };

        // Seed custom data so the route appears in the bulk response
        var seedData = new Dictionary<string, string> { { "bulk_async_key", "bulk_async_value" } };
        route4Me.UpdateRouteCustomData(routeId, seedData, out _);

        // Act
        var response = await route4Me.GetBulkRouteCustomDataAsync(routeIds);

        // Assert
        Assert.IsNotNull(response.Item1, "GetBulkRouteCustomDataAsync should return a collection");
        Assert.IsNull(response.Item2, "GetBulkRouteCustomDataAsync should not return an error");
        Assert.IsNotNull(response.Item1.Data, "Collection Data property should not be null");
        Assert.IsTrue(response.Item1.Data.ContainsKey(routeId), "Collection should contain the requested route ID");
    }

    [Test]
    public void GetRouteCustomData_AfterUpdate_ShouldReflectUpdatedValues()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var customData = new Dictionary<string, string>
        {
            { "verify_key", "verify_value" }
        };

        // Update custom data first
        route4Me.UpdateRouteCustomData(routeId, customData, out _);

        // Act - fetch it back
        var result = route4Me.GetRouteCustomDataDedicated(routeId, out ResultResponse resultResponse);

        // Assert
        Assert.IsNotNull(result, "GetRouteCustomDataDedicated should return data after update");
        Assert.IsNull(resultResponse, "GetRouteCustomDataDedicated should not return an error");
        Assert.IsTrue(result.ContainsKey("verify_key"), "Custom data should contain the set key");
        Assert.AreEqual("verify_value", result["verify_key"], "Custom data value should match");
    }
}