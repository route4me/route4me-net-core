using System;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSdkV5UnitTest.V5.Locations
{
    /// <summary>
    /// Unit tests for LocationManagerV5
    /// Tests location list, clustering, heatmap, territories, export, and location type CRUD operations
    /// </summary>
    [TestFixture]
    public class LocationManagerV5Tests
    {
        private LocationManagerV5 _locationManager;
        private string _apiKey;

        [SetUp]
        public void Setup()
        {
            _apiKey = ApiKeys.ActualApiKey;
            _locationManager = new LocationManagerV5(_apiKey);
        }

        #region Location List Tests

        [Test]
        public void GetLocationsCombinedTest()
        {
            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                },
                Page = 1,
                PerPage = 10
            };

            var result = _locationManager.GetLocationsCombined(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsCombined failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.Data, "Result data should not be null");
        }

        [Test]
        public async Task GetLocationsCombinedAsyncTest()
        {
            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                },
                Page = 1,
                PerPage = 10
            };

            var (result, resultResponse) = await _locationManager.GetLocationsCombinedAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsCombinedAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.Data, "Result data should not be null");
        }

        [Test]
        public void GetLocationsCombinedValidationTest()
        {
            var result = _locationManager.GetLocationsCombined(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void GetLocationsListTest()
        {
            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters(),
                Page = 1,
                PerPage = 10
            };

            var result = _locationManager.GetLocationsList(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsList failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationsListAsyncTest()
        {
            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters(),
                Page = 1,
                PerPage = 10
            };

            var (result, resultResponse) = await _locationManager.GetLocationsListAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsListAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        #endregion

        #region Clustering Tests

        [Test]
        public void GetLocationsClusteringTest()
        {
            var request = new LocationClusteringRequest
            {
                Filters = new LocationFilters(),
                Clustering = new ClusteringParameters
                {
                    Zoom = 10
                },
                WithClusters = true
            };

            var result = _locationManager.GetLocationsClustering(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsClustering failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationsClusteringAsyncTest()
        {
            var request = new LocationClusteringRequest
            {
                Filters = new LocationFilters(),
                Clustering = new ClusteringParameters
                {
                    Zoom = 10
                },
                WithClusters = true
            };

            var (result, resultResponse) = await _locationManager.GetLocationsClusteringAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsClusteringAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        #endregion

        #region Heatmap Tests

        [Test]
        public void GetLocationsHeatmapTest()
        {
            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters(),
                Zoom = 10
            };

            var result = _locationManager.GetLocationsHeatmap(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsHeatmap failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationsHeatmapAsyncTest()
        {
            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters(),
                Zoom = 10
            };

            var (result, resultResponse) = await _locationManager.GetLocationsHeatmapAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsHeatmapAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        #endregion

        #region Territories Tests

        [Test]
        public void GetLocationsTerritoriesTest()
        {
            var request = new LocationTerritoriesRequest
            {
                Filters = new LocationFilters(),
                TerritoriesPage = 1,
                TerritoriesPerPage = 10
            };

            var result = _locationManager.GetLocationsTerritories(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsTerritories failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationsTerritoriesAsyncTest()
        {
            var request = new LocationTerritoriesRequest
            {
                Filters = new LocationFilters(),
                TerritoriesPage = 1,
                TerritoriesPerPage = 10
            };

            var (result, resultResponse) = await _locationManager.GetLocationsTerritoriesAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsTerritoriesAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        #endregion

        #region Export Tests

        [Test]
        public void GetLocationsExportColumnsTest()
        {
            var request = new LocationExportColumnsRequest
            {
                Filters = new LocationFilters()
            };

            var result = _locationManager.GetLocationsExportColumns(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsExportColumns failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationsExportColumnsAsyncTest()
        {
            var request = new LocationExportColumnsRequest
            {
                Filters = new LocationFilters()
            };

            var (result, resultResponse) = await _locationManager.GetLocationsExportColumnsAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationsExportColumnsAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public void ExportLocationsTest()
        {
            var request = new LocationExportRequest
            {
                Filters = new LocationFilters(),
                Format = "csv",
                Filename = "test_export",
                Page = 1,
                PerPage = 10
            };

            var result = _locationManager.ExportLocations(request, out var resultResponse);

            // Export returns 202 Accepted, so we check for either null response or status true
            Assert.IsTrue(resultResponse == null || resultResponse.Status, "ExportLocations failed");
        }

        [Test]
        public async Task ExportLocationsAsyncTest()
        {
            var request = new LocationExportRequest
            {
                Filters = new LocationFilters(),
                Format = "csv",
                Filename = "test_export_async",
                Page = 1,
                PerPage = 10
            };

            var (result, resultResponse, jobId) = await _locationManager.ExportLocationsAsync(request);

            // Export returns 202 Accepted, so we check for either null response or status true
            Assert.IsTrue(resultResponse == null || resultResponse.Status, "ExportLocationsAsync failed");
        }

        #endregion

        #region Location Types CRUD Tests

        [Test]
        public void GetLocationTypesTest()
        {
            var request = new GetLocationTypesRequest
            {
                Page = 1,
                PerPage = 10
            };

            var result = _locationManager.GetLocationTypes(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationTypes failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public async Task GetLocationTypesAsyncTest()
        {
            var request = new GetLocationTypesRequest
            {
                Page = 1,
                PerPage = 10
            };

            var (result, resultResponse) = await _locationManager.GetLocationTypesAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationTypesAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
        }

        [Test]
        public void GetLocationTypeByIdTest()
        {
            // First, create a location type to test retrieval
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "Test Location Type " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Test Description",
                IsParentType = true,
                IsChildType = false
            };

            var created = _locationManager.CreateLocationType(createRequest, out var createResponse);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");
            Assert.IsNotNull(created, "Created location type should not be null");

            // Now test GetById
            var result = _locationManager.GetLocationTypeById(created.LocationTypeId, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationTypeById failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(created.LocationTypeId, result.LocationTypeId, "Location type IDs should match");

            // Cleanup
            _locationManager.DeleteLocationType(created.LocationTypeId, out _);
        }

        [Test]
        public async Task GetLocationTypeByIdAsyncTest()
        {
            // First, create a location type to test retrieval
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "Test Location Type Async " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Test Description Async",
                IsParentType = true,
                IsChildType = false
            };

            var (created, createResponse) = await _locationManager.CreateLocationTypeAsync(createRequest);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");
            Assert.IsNotNull(created, "Created location type should not be null");

            // Now test GetByIdAsync
            var (result, resultResponse) = await _locationManager.GetLocationTypeByIdAsync(created.LocationTypeId);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "GetLocationTypeByIdAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(created.LocationTypeId, result.LocationTypeId, "Location type IDs should match");

            // Cleanup
            await _locationManager.DeleteLocationTypeAsync(created.LocationTypeId);
        }

        [Test]
        public void CreateLocationTypeTest()
        {
            var request = new StoreLocationTypeRequest
            {
                Name = "Test Location Type " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Test Description for Create",
                IsParentType = true,
                IsChildType = false
            };

            var result = _locationManager.CreateLocationType(request, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "CreateLocationType failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.LocationTypeId, "LocationTypeId should not be null");
            Assert.AreEqual(request.Name, result.Name, "Names should match");

            // Cleanup
            _locationManager.DeleteLocationType(result.LocationTypeId, out _);
        }

        [Test]
        public async Task CreateLocationTypeAsyncTest()
        {
            var request = new StoreLocationTypeRequest
            {
                Name = "Test Location Type Async " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Test Description for Create Async",
                IsParentType = false,
                IsChildType = true
            };

            var (result, resultResponse) = await _locationManager.CreateLocationTypeAsync(request);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "CreateLocationTypeAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsNotNull(result.LocationTypeId, "LocationTypeId should not be null");
            Assert.AreEqual(request.Name, result.Name, "Names should match");

            // Cleanup
            await _locationManager.DeleteLocationTypeAsync(result.LocationTypeId);
        }

        [Test]
        public void UpdateLocationTypeTest()
        {
            // First, create a location type
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "Original Name " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Original Description",
                IsParentType = true,
                IsChildType = false
            };

            var created = _locationManager.CreateLocationType(createRequest, out var createResponse);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");

            // Now update it
            var updateRequest = new StoreLocationTypeRequest
            {
                Name = "Updated Name " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Updated Description",
                IsParentType = false,
                IsChildType = true
            };

            var result = _locationManager.UpdateLocationType(created.LocationTypeId, updateRequest, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "UpdateLocationType failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(created.LocationTypeId, result.LocationTypeId, "Location type IDs should match");
            Assert.AreEqual(updateRequest.Name, result.Name, "Name should be updated");

            // Cleanup
            _locationManager.DeleteLocationType(created.LocationTypeId, out _);
        }

        [Test]
        public async Task UpdateLocationTypeAsyncTest()
        {
            // First, create a location type
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "Original Name Async " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Original Description Async",
                IsParentType = true,
                IsChildType = false
            };

            var (created, createResponse) = await _locationManager.CreateLocationTypeAsync(createRequest);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");

            // Now update it
            var updateRequest = new StoreLocationTypeRequest
            {
                Name = "Updated Name Async " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Updated Description Async",
                IsParentType = false,
                IsChildType = true
            };

            var (result, resultResponse) = await _locationManager.UpdateLocationTypeAsync(created.LocationTypeId, updateRequest);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "UpdateLocationTypeAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(created.LocationTypeId, result.LocationTypeId, "Location type IDs should match");
            Assert.AreEqual(updateRequest.Name, result.Name, "Name should be updated");

            // Cleanup
            await _locationManager.DeleteLocationTypeAsync(created.LocationTypeId);
        }

        [Test]
        public void DeleteLocationTypeTest()
        {
            // First, create a location type
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "To Delete " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Will be deleted",
                IsParentType = true,
                IsChildType = false
            };

            var created = _locationManager.CreateLocationType(createRequest, out var createResponse);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");

            // Now delete it
            var result = _locationManager.DeleteLocationType(created.LocationTypeId, out var resultResponse);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "DeleteLocationType failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.Status, "Deletion should be successful");
        }

        [Test]
        public async Task DeleteLocationTypeAsyncTest()
        {
            // First, create a location type
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "To Delete Async " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Will be deleted async",
                IsParentType = true,
                IsChildType = false
            };

            var (created, createResponse) = await _locationManager.CreateLocationTypeAsync(createRequest);
            Assert.IsTrue(createResponse == null || createResponse.Status, "Create failed");

            // Now delete it
            var (result, resultResponse) = await _locationManager.DeleteLocationTypeAsync(created.LocationTypeId);

            Assert.IsTrue(resultResponse == null || resultResponse.Status, "DeleteLocationTypeAsync failed");
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.Status, "Deletion should be successful");
        }

        [Test]
        public void CreateLocationTypeValidationTest()
        {
            // Null request
            var result1 = _locationManager.CreateLocationType(null, out var response1);
            Assert.IsFalse(response1.Status, "Should fail with null request");
            Assert.IsNull(result1, "Result should be null");

            // Missing name
            var request = new StoreLocationTypeRequest
            {
                Name = "",
                IsParentType = true,
                IsChildType = false
            };
            var result2 = _locationManager.CreateLocationType(request, out var response2);
            Assert.IsFalse(response2.Status, "Should fail with empty name");
            Assert.IsNull(result2, "Result should be null");
        }

        [Test]
        public void GetLocationTypeByIdValidationTest()
        {
            var result = _locationManager.GetLocationTypeById(null, out var resultResponse);
            Assert.IsFalse(resultResponse.Status, "Should fail with null ID");
            Assert.IsNull(result, "Result should be null");
        }

        [Test]
        public void UpdateLocationTypeValidationTest()
        {
            // Null ID
            var request = new StoreLocationTypeRequest
            {
                Name = "Test",
                IsParentType = true,
                IsChildType = false
            };
            var result1 = _locationManager.UpdateLocationType(null, request, out var response1);
            Assert.IsFalse(response1.Status, "Should fail with null ID");
            Assert.IsNull(result1, "Result should be null");

            // Null request
            var result2 = _locationManager.UpdateLocationType("test-id", null, out var response2);
            Assert.IsFalse(response2.Status, "Should fail with null request");
            Assert.IsNull(result2, "Result should be null");
        }

        [Test]
        public void DeleteLocationTypeValidationTest()
        {
            var result = _locationManager.DeleteLocationType(null, out var resultResponse);
            Assert.IsFalse(resultResponse.Status, "Should fail with null ID");
            Assert.IsNull(result, "Result should be null");
        }

        #endregion

        #region Additional Validation Tests

        [Test]
        public void GetLocationsListValidationTest()
        {
            var result = _locationManager.GetLocationsList(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationsListValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.GetLocationsListAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void GetLocationsClusteringValidationTest()
        {
            var result = _locationManager.GetLocationsClustering(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationsClusteringValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.GetLocationsClusteringAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void GetLocationsHeatmapValidationTest()
        {
            var result = _locationManager.GetLocationsHeatmap(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationsHeatmapValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.GetLocationsHeatmapAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void GetLocationsTerritoriesValidationTest()
        {
            var result = _locationManager.GetLocationsTerritories(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationsTerritoriesValidationAsyncTest()
        {
            var response = await _locationManager.GetLocationsTerritoriesAsync(null);
            var result = response.Item1;
            var resultResponse = response.Item2;

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void GetLocationsExportColumnsValidationTest()
        {
            var result = _locationManager.GetLocationsExportColumns(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationsExportColumnsValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.GetLocationsExportColumnsAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public void ExportLocationsValidationTest()
        {
            var result = _locationManager.ExportLocations(null, out var resultResponse);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task ExportLocationsValidationAsyncTest()
        {
            var (result, resultResponse, _) = await _locationManager.ExportLocationsAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null request");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task GetLocationTypeByIdValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.GetLocationTypeByIdAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null ID");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        [Test]
        public async Task CreateLocationTypeValidationAsyncTest()
        {
            // Null request
            var response1 = await _locationManager.CreateLocationTypeAsync(null);
            Assert.IsFalse(response1.Item2.Status, "Should fail with null request");
            Assert.IsNull(response1.Item1, "Result should be null");

            // Missing name
            var request = new StoreLocationTypeRequest
            {
                Name = "",
                IsParentType = true,
                IsChildType = false
            };
            var response2 = await _locationManager.CreateLocationTypeAsync(request);
            Assert.IsFalse(response2.Item2.Status, "Should fail with empty name");
            Assert.IsNull(response2.Item1, "Result should be null");
        }

        [Test]
        public async Task UpdateLocationTypeValidationAsyncTest()
        {
            var request = new StoreLocationTypeRequest
            {
                Name = "Test",
                IsParentType = true,
                IsChildType = false
            };

            // Null ID
            var response1 = await _locationManager.UpdateLocationTypeAsync(null, request);
            Assert.IsFalse(response1.Item2.Status, "Should fail with null ID");
            Assert.IsNull(response1.Item1, "Result should be null");

            // Null request
            var response2 = await _locationManager.UpdateLocationTypeAsync("test-id", null);
            Assert.IsFalse(response2.Item2.Status, "Should fail with null request");
            Assert.IsNull(response2.Item1, "Result should be null");
        }

        [Test]
        public async Task DeleteLocationTypeValidationAsyncTest()
        {
            var (result, resultResponse) = await _locationManager.DeleteLocationTypeAsync(null);

            Assert.IsFalse(resultResponse.Status, "Should fail with null ID");
            Assert.IsNull(result, "Result should be null");
            Assert.IsTrue(resultResponse.Messages.ContainsKey("Error"), "Should contain error message");
        }

        #endregion
    }
}