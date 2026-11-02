using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSdkV5UnitTest.V5.Routes;

/// <summary>
///     The test account needs the Fueling API feature; without it the endpoint answers 403.
/// </summary>
[TestFixture]
public class RouteRefuelSuggestionsTests
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

        s_lsOptimizationIDs.Add(s_tdr.SD10Stops_optimization_problem_id);
    }

    [TearDown]
    public void TearDown()
    {
        var optimizationResult = s_tdr.RemoveOptimization(s_lsOptimizationIDs.ToArray());
        Assert.True(optimizationResult, "Removing of the testing optimization problem failed.");
    }

    [Test]
    public void SuggestRouteRefuels_WithNamedRouteAndFuelOverrides_ShouldPlanTheRoute()
    {
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var refuelRequest = new RouteRefuelSuggestionsRequest()
        {
            Routes = new[]
            {
                new RouteRefuelRouteRequest()
                {
                    RouteId = routeId,
                    StartingFuelLevelPercent = 15,
                    TankCapacity = 40,
                    TankCapacityUnit = "L",
                    FuelConsumption = 9,
                    FuelConsumptionUnit = "l/100km"
                }
            },
            // 0 skips the station search, so the result does not depend on the station provider.
            MaxStations = 0
        };

        var result = route4Me.SuggestRouteRefuels(refuelRequest, out ResultResponse resultResponse);

        var failMessages = resultResponse?.Messages?.SelectMany(message => message.Value) ?? Enumerable.Empty<string>();
        Assert.NotNull(result, "SuggestRouteRefuels failed: " + string.Join("; ", failMessages));
        Assert.That(result.MatchedRoutesCount, Is.EqualTo(1));
        Assert.That(result.CheckedRoutesCount, Is.EqualTo(1));

        var route = result.Routes[0];
        Assert.That(route.RouteId, Is.EqualTo(routeId).IgnoreCase);
        Assert.That(route.Status, Is.EqualTo("ok"), $"The route was skipped: {route.Reason}");
        // The overrides were read: a misnamed field would fall back to the (absent) vehicle data.
        Assert.That(route.Fuel.StartingLevelSource, Is.EqualTo("request"));
        Assert.That(route.Fuel.TankCapacitySource, Is.EqualTo("request"));
        Assert.That(route.Fuel.ConsumptionSource, Is.EqualTo("request"));
        Assert.NotNull(route.Suggestions);
    }
}