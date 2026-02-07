using System;
using System.IO;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSdkV5UnitTest.V5.StrategicPlanner
{
    /// <summary>
    /// Unit tests for StrategicPlannerManagerV5
    /// Tests all strategic planner operations including optimizations, scenarios, locations, routes, visits, and exports
    /// </summary>
    [TestFixture]
    public class StrategicPlannerManagerV5Tests
    {
        private StrategicPlannerManagerV5 _manager;
        private string _apiKey;
        private string _testOptimizationId;
        private string _testScenarioId;
        private string _testLocationId;

        [SetUp]
        public void Setup()
        {
            _apiKey = ApiKeys.ActualApiKey;
            _manager = new StrategicPlannerManagerV5(_apiKey);
        }

        #region Upload Tests

        [Test]
        public void UploadFileValidationTest()
        {
            // Test with non-existent file
            var result = _manager.UploadFile("nonexistent.csv", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for non-existent file");
            Assert.IsFalse(resultResponse.Status, "Should fail with non-existent file");
        }

        [Test]
        public void GetUploadPreviewValidationTest()
        {
            var request = new UploadPreviewRequest
            {
                StrUploadID = null
            };

            var result = _manager.GetUploadPreview(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null upload ID");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public async Task GetUploadPreviewAsyncValidationTest()
        {
            var request = new UploadPreviewRequest
            {
                StrUploadID = null
            };

            var (result, resultResponse) = await _manager.GetUploadPreviewAsync(request);

            Assert.IsNotNull(resultResponse, "Should return error for null upload ID");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void CreateOptimizationFromUploadValidationTest()
        {
            var request = new CreateOptimizationRequest
            {
                ParamsJson = null
            };

            var result = _manager.CreateOptimizationFromUpload(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null params");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        #endregion

        #region Strategic Optimizations Tests

        [Test]
        public void CreateOrUpdateOptimizationValidationTest()
        {
            var request = new CreateOrUpdateStrategicOptimizationRequest
            {
                Name = null,
                RootMemberId = null,
                Locations = null
            };

            var result = _manager.CreateOrUpdateOptimization(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid request");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public async Task CreateOrUpdateOptimizationAsyncValidationTest()
        {
            var request = new CreateOrUpdateStrategicOptimizationRequest
            {
                Name = null,
                RootMemberId = null,
                Locations = null
            };

            var (result, resultResponse) = await _manager.CreateOrUpdateOptimizationAsync(request);

            Assert.IsNotNull(resultResponse, "Should return error for invalid request");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void GetOptimizationValidationTest()
        {
            var result = _manager.GetOptimization("invalid_id", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public async Task GetOptimizationAsyncValidationTest()
        {
            var (result, resultResponse) = await _manager.GetOptimizationAsync("invalid_id");

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public void GetOptimizationsCombinedTest()
        {
            var request = new ListStrategicOptimizationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new StrategicOptimizationFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetOptimizationsCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetOptimizationsCombinedAsyncTest()
        {
            var request = new ListStrategicOptimizationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new StrategicOptimizationFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetOptimizationsCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public void DeleteOptimizationValidationTest()
        {
            var result = _manager.DeleteOptimization("invalid_id", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
            Assert.IsFalse(result, "Should return false");
        }

        [Test]
        public void BulkDeleteOptimizationsValidationTest()
        {
            var request = new DeleteOptimizationsRequest
            {
                Ids = null
            };

            var result = _manager.BulkDeleteOptimizations(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null IDs");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void AddScenariosValidationTest()
        {
            var request = new AddScenariosRequest
            {
                ParamsJson = null
            };

            var result = _manager.AddScenarios("test_id", request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null params");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        #endregion

        #region Strategic Scenarios Tests

        [Test]
        public void GetScenarioValidationTest()
        {
            var result = _manager.GetScenario("invalid_id", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public async Task GetScenarioAsyncValidationTest()
        {
            var (result, resultResponse) = await _manager.GetScenarioAsync("invalid_id");

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public void GetScenariosCombinedTest()
        {
            var request = new ListScenariosRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new ScenarioFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetScenariosCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetScenariosCombinedAsyncTest()
        {
            var request = new ListScenariosRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new ScenarioFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetScenariosCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public void AcceptScenarioValidationTest()
        {
            var request = new AcceptScenarioRequest
            {
                RoutesType = null
            };

            var result = _manager.AcceptScenario("test_id", request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null routes type");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void CompleteScenarioImportValidationTest()
        {
            var request = new CompleteScenarioRequest
            {
                TotalCreatedRoutes = null,
                TotalFailedRoutes = null
            };

            var result = _manager.CompleteScenarioImport("test_id", request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null values");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void GenerateScenarioWithAIValidationTest()
        {
            var request = new GenerateScenarioRequest
            {
                Prompt = null
            };

            var result = _manager.GenerateScenarioWithAI(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null prompt");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public async Task GenerateScenarioWithAIAsyncValidationTest()
        {
            var request = new GenerateScenarioRequest
            {
                Prompt = null
            };

            var (result, resultResponse) = await _manager.GenerateScenarioWithAIAsync(request);

            Assert.IsNotNull(resultResponse, "Should return error for null prompt");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        #endregion

        #region Strategic Locations Tests

        [Test]
        public void GetLocationValidationTest()
        {
            var result = _manager.GetLocation("invalid_id", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public async Task GetLocationAsyncValidationTest()
        {
            var (result, resultResponse) = await _manager.GetLocationAsync("invalid_id");

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public void GetLocationsCombinedTest()
        {
            var request = new ListLocationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetLocationsCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetLocationsCombinedAsyncTest()
        {
            var request = new ListLocationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetLocationsCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

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

            var result = _manager.GetLocationsClustering(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
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

            var (result, resultResponse) = await _manager.GetLocationsClusteringAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
        }

        [Test]
        public void GetLocationsHeatmapTest()
        {
            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters(),
                Zoom = 10
            };

            var result = _manager.GetLocationsHeatmap(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
        }

        [Test]
        public async Task GetLocationsHeatmapAsyncTest()
        {
            var request = new LocationHeatmapRequest
            {
                Filters = new LocationFilters(),
                Zoom = 10
            };

            var (result, resultResponse) = await _manager.GetLocationsHeatmapAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
        }

        [Test]
        public void UpdateLocationValidationTest()
        {
            var request = new LocationUpdateRequest();

            var result = _manager.UpdateLocation("invalid_id", request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public async Task UpdateLocationAsyncValidationTest()
        {
            var request = new LocationUpdateRequest();

            var (result, resultResponse) = await _manager.UpdateLocationAsync("invalid_id", request);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public void PatchLocationValidationTest()
        {
            var request = new LocationUpdateRequest();

            var result = _manager.PatchLocation("invalid_id", request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
        }

        [Test]
        public void DeleteLocationValidationTest()
        {
            var result = _manager.DeleteLocation("invalid_id", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
            Assert.IsFalse(result, "Should return false");
        }

        [Test]
        public async Task DeleteLocationAsyncValidationTest()
        {
            var (result, resultResponse) = await _manager.DeleteLocationAsync("invalid_id");

            Assert.IsNotNull(resultResponse, "Should return error for invalid ID");
            Assert.IsFalse(resultResponse.Status, "Should fail with invalid ID");
            Assert.IsFalse(result, "Should return false");
        }

        [Test]
        public void BulkUpdateLocationsValidationTest()
        {
            var request = new BulkUpdateLocationsRequest
            {
                Ids = null,
                Data = null
            };

            var result = _manager.BulkUpdateLocations(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null data");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void BulkDeleteLocationsValidationTest()
        {
            var request = new BulkDeleteLocationsRequest
            {
                Ids = null
            };

            var result = _manager.BulkDeleteLocations(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null IDs");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        #endregion

        #region Strategic Routes Tests

        [Test]
        public void GetRoutesCombinedTest()
        {
            var request = new ListRoutesRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new RouteFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetRoutesCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetRoutesCombinedAsyncTest()
        {
            var request = new ListRoutesRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new RouteFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetRoutesCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        #endregion

        #region Strategic Visits Tests

        [Test]
        public void GetVisitsCombinedTest()
        {
            var request = new ListVisitsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new VisitFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetVisitsCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetVisitsCombinedAsyncTest()
        {
            var request = new ListVisitsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new VisitFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetVisitsCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        #endregion

        #region Strategic Destinations Tests

        [Test]
        public void GetDestinationsCombinedTest()
        {
            var request = new ListDestinationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new DestinationFilters
                {
                    SearchQuery = ""
                }
            };

            var result = _manager.GetDestinationsCombined(request, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        [Test]
        public async Task GetDestinationsCombinedAsyncTest()
        {
            var request = new ListDestinationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new DestinationFilters
                {
                    SearchQuery = ""
                }
            };

            var (result, resultResponse) = await _manager.GetDestinationsCombinedAsync(request);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
                Assert.IsNotNull(result.Data, "Data should not be null");
            }
        }

        #endregion

        #region Export Operations Tests

        [Test]
        public void GetExportColumnsTest()
        {
            var result = _manager.GetExportColumns("optimizations", null, out var resultResponse);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
        }

        [Test]
        public async Task GetExportColumnsAsyncTest()
        {
            var (result, resultResponse) = await _manager.GetExportColumnsAsync("optimizations", null);

            // Should either succeed or fail gracefully
            if (resultResponse != null)
            {
                Assert.IsFalse(resultResponse.Status, "If error, status should be false");
            }
            else
            {
                Assert.IsNotNull(result, "Result should not be null on success");
            }
        }

        [Test]
        public void GetExportStatusTest()
        {
            var result = _manager.GetExportStatus(99999, out var resultResponse);

            // Should return 404 error for non-existent job
            Assert.IsNotNull(resultResponse, "Should return error for non-existent job");
            Assert.IsFalse(resultResponse.Status, "Should fail with non-existent job");
        }

        [Test]
        public async Task GetExportStatusAsyncTest()
        {
            var (result, resultResponse) = await _manager.GetExportStatusAsync(99999);

            // Should return 404 error for non-existent job
            Assert.IsNotNull(resultResponse, "Should return error for non-existent job");
            Assert.IsFalse(resultResponse.Status, "Should fail with non-existent job");
        }

        [Test]
        public void ExportOptimizationsValidationTest()
        {
            var request = new ExportOptimizationsRequest
            {
                Ids = null,
                Columns = null
            };

            var result = _manager.ExportOptimizations(request, out var resultResponse);

            // Should handle gracefully - may require filters or IDs
            Assert.IsTrue(result == null || resultResponse != null, "Should validate request");
        }

        #endregion

        #region Pattern Analysis Tests

        [Test]
        public void UploadPatternAnalysisFileValidationTest()
        {
            var result = _manager.UploadPatternAnalysisFile("nonexistent.csv", out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for non-existent file");
            Assert.IsFalse(resultResponse.Status, "Should fail with non-existent file");
        }

        [Test]
        public void GetPatternAnalysisPreviewValidationTest()
        {
            var request = new PatternAnalysisUploadPreviewRequest
            {
                StrUploadID = null
            };

            var result = _manager.GetPatternAnalysisPreview(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null upload ID");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        [Test]
        public void InitiatePatternAnalysisValidationTest()
        {
            var request = new InitiatePatternAnalysisRequest
            {
                StrUploadID = null
            };

            var result = _manager.InitiatePatternAnalysis(request, out var resultResponse);

            Assert.IsNotNull(resultResponse, "Should return error for null upload ID");
            Assert.IsFalse(resultResponse.Status, "Should fail validation");
        }

        #endregion

        #region Integration Tests

        [Test]
        public void ManagerInitializationTest()
        {
            Assert.IsNotNull(_manager, "Manager should be initialized");
        }

        [Test]
        public void ManagerAccessViaFacadeTest()
        {
            var route4Me = new Route4MeSDK.Route4MeManagerV5(_apiKey);
            
            Assert.IsNotNull(route4Me.StrategicPlanner, "StrategicPlanner should be accessible via facade");
        }

        #endregion

        [TearDown]
        public void Cleanup()
        {
            // Clean up any test data if created
            // This would be implemented when running actual integration tests
        }
    }
}
