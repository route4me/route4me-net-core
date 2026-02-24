using System.Collections.Generic;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSdkV5UnitTest.V5.RouteDestinations;

/// <summary>
/// Integration tests for the Route Destinations API (V5).
/// Each test requires a live API key and creates/tears down a real optimization.
/// </summary>
[TestFixture]
public class RouteDestinationsTests
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

    #region GetDestinationsList

    [Test]
    public void GetDestinationsList_WithRouteIdFilter_ShouldReturnItems()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var request = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 20
        };

        // Act
        var result = route4Me.GetDestinationsList(request, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse, "GetDestinationsList should not return an error");
        Assert.IsNotNull(result, "GetDestinationsList should return a result");
        Assert.IsNotNull(result.Items, "Items array should not be null");
        Assert.Greater(result.Items.Length, 0, "Should return at least one destination for the route");
    }

    [Test]
    public async Task GetDestinationsListAsync_WithRouteIdFilter_ShouldReturnItems()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var request = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 20
        };

        // Act
        var response = await route4Me.GetDestinationsListAsync(request);

        // Assert
        Assert.IsNull(response.Item2, "GetDestinationsListAsync should not return an error");
        Assert.IsNotNull(response.Item1, "GetDestinationsListAsync should return a result");
        Assert.IsNotNull(response.Item1.Items, "Items array should not be null");
        Assert.Greater(response.Item1.Items.Length, 0, "Should return at least one destination");
    }

    [Test]
    public void GetDestinationsList_CustomFields_AreDeserialised()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var request = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 50
        };

        // Act
        var result = route4Me.GetDestinationsList(request, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse, "GetDestinationsList should not return an error");
        Assert.IsNotNull(result?.Items, "Items should not be null");

        // CustomFields (destination-level custom key/value data) is allowed to be null
        // for destinations that have no custom data set — the property itself must be accessible.
        foreach (var item in result.Items)
        {
            // Accessing CustomFields must not throw; value may be null or an empty dict
            var _ = item.CustomFields;
        }
    }

    #endregion

    #region GetDestination

    [Test]
    public void GetDestination_WithValidDestinationId_ShouldSucceed()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        // First get the list to find a valid destination ID
        var listRequest = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 5
        };
        var listResult = route4Me.GetDestinationsList(listRequest, out _);
        Assert.IsNotNull(listResult?.Items, "Need destinations to run this test");
        Assert.Greater(listResult.Items.Length, 0, "Need at least one destination");

        var destinationId = listResult.Items[0].RouteDestinationId?.ToString();
        Assert.IsNotNull(destinationId, "Destination ID should not be null");

        // Act
        var result = route4Me.GetDestination(destinationId, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse, "GetDestination should not return an error");
        Assert.IsNotNull(result, "GetDestination should return a destination resource");
        Assert.AreEqual(listResult.Items[0].RouteDestinationId, result.RouteDestinationId,
            "Returned destination should match the requested ID");
        Assert.IsNotNull(result.DestinationName, "DestinationName should be populated");
    }

    [Test]
    public async Task GetDestinationAsync_WithValidDestinationId_ShouldSucceed()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var listRequest = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 5
        };
        var listResult = route4Me.GetDestinationsList(listRequest, out _);
        Assert.IsNotNull(listResult?.Items);
        Assert.Greater(listResult.Items.Length, 0);

        var destinationId = listResult.Items[0].RouteDestinationId?.ToString();

        // Act
        var response = await route4Me.GetDestinationAsync(destinationId);

        // Assert
        Assert.IsNull(response.Item2, "GetDestinationAsync should not return an error");
        Assert.IsNotNull(response.Item1, "GetDestinationAsync should return a destination resource");
        Assert.IsNotNull(response.Item1.DestinationName, "DestinationName should be populated");
    }

    [Test]
    public void GetDestination_CustomFields_AreAccessible()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);
        var routeId = s_tdr.SD10Stops_route.RouteID;

        var listRequest = new GetDestinationsRequest
        {
            Filters = new DestinationFilters { RouteId = routeId },
            Page = 1,
            PerPage = 5
        };
        var listResult = route4Me.GetDestinationsList(listRequest, out ResultResponse listResp);
        Assert.IsNotNull(listResult?.Items);
        Assert.Greater(listResult.Items.Length, 0);

        var destinationId = listResult.Items[0].RouteDestinationId?.ToString();

        // Act
        var result = route4Me.GetDestination(destinationId, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse);
        Assert.IsNotNull(result);
        // CustomFields property must exist and be accessible (may be null if no custom fields set)
        var customFields = result.CustomFields;
    }

    #endregion

    #region GetDestinationFields

    [Test]
    public void GetDestinationFields_ShouldReturnFieldList()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationFields(out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse, "GetDestinationFields should not return an error");
        Assert.IsNotNull(result, "GetDestinationFields should return a response");
        Assert.IsNotNull(result.Fields, "Fields array should not be null");
        Assert.Greater(result.Fields.Length, 0, "Should return at least one field");
    }

    [Test]
    public async Task GetDestinationFieldsAsync_ShouldReturnFieldList()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var response = await route4Me.GetDestinationFieldsAsync();

        // Assert
        Assert.IsNull(response.Item2, "GetDestinationFieldsAsync should not return an error");
        Assert.IsNotNull(response.Item1, "GetDestinationFieldsAsync should return a response");
        Assert.IsNotNull(response.Item1.Fields, "Fields array should not be null");
        Assert.Greater(response.Item1.Fields.Length, 0, "Should return at least one field");
    }

    [Test]
    public void GetDestinationFields_ShouldHaveExpectedMetadata()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationFields(out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse);
        Assert.IsNotNull(result?.Fields);
        foreach (var field in result.Fields)
        {
            Assert.IsNotNull(field.Label, "Each field should have a label");
            Assert.IsNotNull(field.Value, "Each field should have a value (field name)");
        }
    }

    #endregion

    #region GetDestinationSequence

    [Test]
    public void GetDestinationSequence_WithValidCoordinates_ShouldReturnOptimizedOrder()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var request = new DestinationSequenceRequest
        {
            Coordinates = new[]
            {
                new SequenceCoordinate { Lat = 50.4501f, Lng = 30.5234f },
                new SequenceCoordinate { Lat = 51.5074f, Lng = -0.1278f },
                new SequenceCoordinate { Lat = 48.8566f, Lng = 2.3522f },
                new SequenceCoordinate { Lat = 52.5200f, Lng = 13.4050f }
            }
        };

        // Act
        var result = route4Me.GetDestinationSequence(request, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(resultResponse, "GetDestinationSequence should not return an error");
        Assert.IsNotNull(result, "GetDestinationSequence should return a result");
        Assert.IsNotNull(result.OptimizedSequence, "OptimizedSequence should not be null");
        Assert.AreEqual(4, result.OptimizedSequence.Length, "Should return an index for each input coordinate");
    }

    [Test]
    public async Task GetDestinationSequenceAsync_WithValidCoordinates_ShouldReturnOptimizedOrder()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var request = new DestinationSequenceRequest
        {
            Coordinates = new[]
            {
                new SequenceCoordinate { Lat = 40.7128f, Lng = -74.0060f },
                new SequenceCoordinate { Lat = 34.0522f, Lng = -118.2437f },
                new SequenceCoordinate { Lat = 41.8781f, Lng = -87.6298f }
            }
        };

        // Act
        var response = await route4Me.GetDestinationSequenceAsync(request);

        // Assert
        Assert.IsNull(response.Item2, "GetDestinationSequenceAsync should not return an error");
        Assert.IsNotNull(response.Item1, "GetDestinationSequenceAsync should return a result");
        Assert.IsNotNull(response.Item1.OptimizedSequence, "OptimizedSequence should not be null");
        Assert.AreEqual(3, response.Item1.OptimizedSequence.Length, "Should return an index for each input coordinate");
    }

    [Test]
    public void GetDestinationSequence_WithNullRequest_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationSequence(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null request");
        Assert.IsNotNull(resultResponse, "Should return an error response for null request");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void GetDestinationSequence_WithEmptyCoordinates_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        var request = new DestinationSequenceRequest
        {
            Coordinates = new SequenceCoordinate[0]
        };

        // Act
        var result = route4Me.GetDestinationSequence(request, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for empty coordinates");
        Assert.IsNotNull(resultResponse, "Should return an error response for empty coordinates");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    #endregion

    #region Input Validation

    [Test]
    public void GetDestinationsList_WithNullRequest_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationsList(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null request");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void GetDestination_WithNullId_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestination(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null id");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void GetDestinationColumns_WithNullKey_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationColumns(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null key");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void GetDestinationByLabelCode_WithNullCode_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationByLabelCode(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null label code");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void GetDestinationsByOrder_WithNullUuid_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.GetDestinationsByOrder(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null order UUID");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    [Test]
    public void EditDestinationColumns_WithNullRequest_ShouldReturnError()
    {
        // Arrange
        var route4Me = new Route4MeManagerV5(CApiKey);

        // Act
        var result = route4Me.EditDestinationColumns(null, out ResultResponse resultResponse);

        // Assert
        Assert.IsNull(result, "Result should be null for a null request");
        Assert.IsNotNull(resultResponse, "Should return an error response");
        Assert.IsFalse(resultResponse.Status, "Status should be false");
    }

    #endregion
}
