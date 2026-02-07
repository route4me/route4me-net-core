using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.StrategicPlanner;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Strategic Planner Manager for managing strategic optimizations, scenarios, locations, routes, and visits
    /// </summary>
    public class StrategicPlannerManagerV5 : Route4MeManagerBase
    {
        public StrategicPlannerManagerV5(string apiKey) : base(apiKey)
        {
        }

        public StrategicPlannerManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        #region Upload Operations

        /// <summary>
        /// Uploads a spreadsheet file (CSV, XLS, XLSX) to Google Cloud Storage for preview
        /// </summary>
        /// <param name="filePath">Path to the file to upload</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Upload response with upload_id and encoding options</returns>
        public UploadFileResponse UploadFile(string filePath, out ResultResponse resultResponse)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "strFilename", System.IO.Path.GetFileName(filePath));

                var result = GetJsonObjectFromAPI<UploadFileResponse>(
                    new GenericParameters(),
                    R4MEInfrastructureSettingsV5.StrategicUploadsFile,
                    HttpMethodType.Post,
                    content,
                    true,
                    out resultResponse);

                return result;
            }
        }

        /// <summary>
        /// Uploads a spreadsheet file (CSV, XLS, XLSX) to Google Cloud Storage for preview asynchronously
        /// </summary>
        /// <param name="filePath">Path to the file to upload</param>
        /// <returns>A Tuple containing upload response or/and failure response</returns>
        public async Task<Tuple<UploadFileResponse, ResultResponse>> UploadFileAsync(string filePath)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "strFilename", System.IO.Path.GetFileName(filePath));

                var result = await GetJsonObjectFromAPIAsync<UploadFileResponse>(
                    new GenericParameters(),
                    R4MEInfrastructureSettingsV5.StrategicUploadsFile,
                    HttpMethodType.Post,
                    content,
                    true,
                    false).ConfigureAwait(false);

                return new Tuple<UploadFileResponse, ResultResponse>(result.Item1, result.Item2);
            }
        }

        /// <summary>
        /// Gets preview table of uploaded file
        /// </summary>
        /// <param name="request">Preview request parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Preview data with headers, sample data, and warnings</returns>
        public UploadPreviewResponse GetUploadPreview(UploadPreviewRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<UploadPreviewResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicUploadsPreview,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets preview table of uploaded file asynchronously
        /// </summary>
        /// <param name="request">Preview request parameters</param>
        /// <returns>A Tuple containing preview response or/and failure response</returns>
        public async Task<Tuple<UploadPreviewResponse, ResultResponse>> GetUploadPreviewAsync(UploadPreviewRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<UploadPreviewResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicUploadsPreview,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<UploadPreviewResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Creates an optimization job using uploaded spreadsheet file or customer locations
        /// </summary>
        /// <param name="request">Create optimization request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Response with optimization_id and message</returns>
        public CreateOptimizationResponse CreateOptimizationFromUpload(CreateOptimizationRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicCreateOptimization,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Creates an optimization job using uploaded spreadsheet file or customer locations asynchronously
        /// </summary>
        /// <param name="request">Create optimization request</param>
        /// <returns>A Tuple containing response or/and failure response</returns>
        public async Task<Tuple<CreateOptimizationResponse, ResultResponse>> CreateOptimizationFromUploadAsync(CreateOptimizationRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicCreateOptimization,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CreateOptimizationResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Optimizations

        /// <summary>
        /// Creates or updates a strategic optimization
        /// </summary>
        /// <param name="request">Create/update optimization request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Created/updated optimization response</returns>
        public CreateOrUpdateStrategicOptimizationResponse CreateOrUpdateOptimization(CreateOrUpdateStrategicOptimizationRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<CreateOrUpdateStrategicOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizations + "/optimizations",
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Creates or updates a strategic optimization asynchronously
        /// </summary>
        /// <param name="request">Create/update optimization request</param>
        /// <returns>A Tuple containing response or/and failure response</returns>
        public async Task<Tuple<CreateOrUpdateStrategicOptimizationResponse, ResultResponse>> CreateOrUpdateOptimizationAsync(CreateOrUpdateStrategicOptimizationRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateOrUpdateStrategicOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizations + "/optimizations",
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CreateOrUpdateStrategicOptimizationResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets a strategic optimization by ID
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Strategic optimization resource</returns>
        public StrategicOptimizationResource GetOptimization(string optimizationId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<StrategicOptimizationResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsById.Replace("{id}", optimizationId),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a strategic optimization by ID asynchronously
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <returns>A Tuple containing optimization resource or/and failure response</returns>
        public async Task<Tuple<StrategicOptimizationResource, ResultResponse>> GetOptimizationAsync(string optimizationId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<StrategicOptimizationResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsById.Replace("{id}", optimizationId),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<StrategicOptimizationResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Updates a strategic optimization
        /// </summary>
        /// <param name="optimizationId">Optimization ID to update</param>
        /// <param name="request">Update request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Updated optimization response</returns>
        public CreateOrUpdateStrategicOptimizationResponse UpdateOptimization(string optimizationId, CreateOrUpdateStrategicOptimizationRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<CreateOrUpdateStrategicOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsById.Replace("{id}", optimizationId),
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Updates a strategic optimization asynchronously
        /// </summary>
        /// <param name="optimizationId">Optimization ID to update</param>
        /// <param name="request">Update request</param>
        /// <returns>A Tuple containing response or/and failure response</returns>
        public async Task<Tuple<CreateOrUpdateStrategicOptimizationResponse, ResultResponse>> UpdateOptimizationAsync(string optimizationId, CreateOrUpdateStrategicOptimizationRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateOrUpdateStrategicOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsById.Replace("{id}", optimizationId),
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CreateOrUpdateStrategicOptimizationResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Deletes a strategic optimization
        /// </summary>
        /// <param name="optimizationId">Optimization ID to delete</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool DeleteOptimization(string optimizationId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            GetJsonObjectFromAPI<object>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizations + "/optimizations/" + optimizationId,
                HttpMethodType.Delete,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Deletes a strategic optimization asynchronously
        /// </summary>
        /// <param name="optimizationId">Optimization ID to delete</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> DeleteOptimizationAsync(string optimizationId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<object>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizations + "/optimizations/" + optimizationId,
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Bulk deletes strategic optimizations
        /// </summary>
        /// <param name="request">Delete request with IDs or filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool BulkDeleteOptimizations(DeleteOptimizationsRequest request, out ResultResponse resultResponse)
        {
            GetJsonObjectFromAPI<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsBulkDelete,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Bulk deletes strategic optimizations asynchronously
        /// </summary>
        /// <param name="request">Delete request with IDs or filters</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> BulkDeleteOptimizationsAsync(DeleteOptimizationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsBulkDelete,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Gets a paginated list of strategic optimizations with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of optimizations</returns>
        public StrategicOptimizationCombinedCollection GetOptimizationsCombined(ListStrategicOptimizationsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<StrategicOptimizationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of strategic optimizations with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<StrategicOptimizationCombinedCollection, ResultResponse>> GetOptimizationsCombinedAsync(ListStrategicOptimizationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<StrategicOptimizationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<StrategicOptimizationCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Creates a draft strategic optimization from existing optimization
        /// </summary>
        /// <param name="optimizationId">Base optimization ID for cloning</param>
        /// <param name="request">Draft creation request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Response with status and optimization_id</returns>
        public CreateOptimizationResponse CreateDraftOptimization(string optimizationId, CreateDraftOptimizationRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsDraft.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Creates a draft strategic optimization from existing optimization asynchronously
        /// </summary>
        /// <param name="optimizationId">Base optimization ID for cloning</param>
        /// <param name="request">Draft creation request</param>
        /// <returns>A Tuple containing response or/and failure response</returns>
        public async Task<Tuple<CreateOptimizationResponse, ResultResponse>> CreateDraftOptimizationAsync(string optimizationId, CreateDraftOptimizationRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsDraft.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CreateOptimizationResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets optimization scenarios parameters by optimization ID
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Array of optimization parameters</returns>
        public object[] GetOptimizationParameters(string optimizationId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<object[]>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsParameters.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets optimization scenarios parameters by optimization ID asynchronously
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <returns>A Tuple containing parameters array or/and failure response</returns>
        public async Task<Tuple<object[], ResultResponse>> GetOptimizationParametersAsync(string optimizationId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<object[]>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsParameters.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<object[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Adds scenarios to an optimization
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="request">Add scenarios request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Response with status and optimization_id</returns>
        public CreateOptimizationResponse AddScenarios(string optimizationId, AddScenariosRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsAddScenarios.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Adds scenarios to an optimization asynchronously
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="request">Add scenarios request</param>
        /// <returns>A Tuple containing response or/and failure response</returns>
        public async Task<Tuple<CreateOptimizationResponse, ResultResponse>> AddScenariosAsync(string optimizationId, AddScenariosRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateOptimizationResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsAddScenarios.Replace("{optimization_id}", optimizationId),
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CreateOptimizationResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports optimizations to a file
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportOptimizations(ExportOptimizationsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExport,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports optimizations to a file asynchronously
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportOptimizationsAsync(ExportOptimizationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExport,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports optimizations with nested entities using separate filters
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportOptimizationsNested(NestedOptimizationsExportRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExportNested,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports optimizations with nested entities using separate filters asynchronously
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportOptimizationsNestedAsync(NestedOptimizationsExportRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExportNested,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets totals for nested optimizations export
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Totals response</returns>
        public NestedExportTotalsResponse GetNestedOptimizationsExportTotals(NestedOptimizationsExportRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<NestedExportTotalsResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExportNestedTotals,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets totals for nested optimizations export asynchronously
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <returns>A Tuple containing totals response or/and failure response</returns>
        public async Task<Tuple<NestedExportTotalsResponse, ResultResponse>> GetNestedOptimizationsExportTotalsAsync(NestedOptimizationsExportRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<NestedExportTotalsResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicOptimizationsExportNestedTotals,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<NestedExportTotalsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Scenarios

        /// <summary>
        /// Gets a scenario by ID
        /// </summary>
        /// <param name="scenarioId">Scenario ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Scenario resource</returns>
        public ScenarioResource GetScenario(string scenarioId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<ScenarioResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicScenariosById.Replace("{id}", scenarioId),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a scenario by ID asynchronously
        /// </summary>
        /// <param name="scenarioId">Scenario ID</param>
        /// <returns>A Tuple containing scenario resource or/and failure response</returns>
        public async Task<Tuple<ScenarioResource, ResultResponse>> GetScenarioAsync(string scenarioId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<ScenarioResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicScenariosById.Replace("{id}", scenarioId),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScenarioResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets a paginated list of scenarios with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of scenarios</returns>
        public ScenarioCombinedCollection GetScenariosCombined(ListScenariosRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ScenarioCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of scenarios with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<ScenarioCombinedCollection, ResultResponse>> GetScenariosCombinedAsync(ListScenariosRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ScenarioCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScenarioCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Accepts a scenario and creates routes (regular or master)
        /// </summary>
        /// <param name="scenarioId">Scenario ID to accept</param>
        /// <param name="request">Accept request with routes_type</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Accept response with job_id</returns>
        public AcceptScenarioResponse AcceptScenario(string scenarioId, AcceptScenarioRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<AcceptScenarioResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosAccept.Replace("{id}", scenarioId),
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Accepts a scenario and creates routes (regular or master) asynchronously
        /// </summary>
        /// <param name="scenarioId">Scenario ID to accept</param>
        /// <param name="request">Accept request with routes_type</param>
        /// <returns>A Tuple containing accept response or/and failure response</returns>
        public async Task<Tuple<AcceptScenarioResponse, ResultResponse>> AcceptScenarioAsync(string scenarioId, AcceptScenarioRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<AcceptScenarioResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosAccept.Replace("{id}", scenarioId),
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AcceptScenarioResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Marks scenario import as complete with statistics
        /// </summary>
        /// <param name="scenarioId">Scenario ID</param>
        /// <param name="request">Complete request with route creation stats</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool CompleteScenarioImport(string scenarioId, CompleteScenarioRequest request, out ResultResponse resultResponse)
        {
            GetJsonObjectFromAPI<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosImportComplete.Replace("{id}", scenarioId),
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Marks scenario import as complete with statistics asynchronously
        /// </summary>
        /// <param name="scenarioId">Scenario ID</param>
        /// <param name="request">Complete request with route creation stats</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> CompleteScenarioImportAsync(string scenarioId, CompleteScenarioRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosImportComplete.Replace("{id}", scenarioId),
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Uses AI to generate scenario configuration from a prompt
        /// </summary>
        /// <param name="request">Generate request with AI prompt</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>AI-generated scenario configuration</returns>
        public GenerateScenarioResource GenerateScenarioWithAI(GenerateScenarioRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<GenerateScenarioResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosGenerate,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Uses AI to generate scenario configuration from a prompt asynchronously
        /// </summary>
        /// <param name="request">Generate request with AI prompt</param>
        /// <returns>A Tuple containing AI-generated config or/and failure response</returns>
        public async Task<Tuple<GenerateScenarioResource, ResultResponse>> GenerateScenarioWithAIAsync(GenerateScenarioRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<GenerateScenarioResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosGenerate,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<GenerateScenarioResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports scenarios to a file
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportScenarios(ExportScenariosRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExport,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports scenarios to a file asynchronously
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportScenariosAsync(ExportScenariosRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExport,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports scenarios with nested entities using separate filters
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportScenariosNested(NestedScenariosExportRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExportNested,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports scenarios with nested entities using separate filters asynchronously
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportScenariosNestedAsync(NestedScenariosExportRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExportNested,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets totals for nested scenarios export
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Totals response</returns>
        public NestedExportTotalsResponse GetNestedScenariosExportTotals(NestedScenariosExportRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<NestedExportTotalsResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExportNestedTotals,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets totals for nested scenarios export asynchronously
        /// </summary>
        /// <param name="request">Nested export request</param>
        /// <returns>A Tuple containing totals response or/and failure response</returns>
        public async Task<Tuple<NestedExportTotalsResponse, ResultResponse>> GetNestedScenariosExportTotalsAsync(NestedScenariosExportRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<NestedExportTotalsResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicScenariosExportNestedTotals,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<NestedExportTotalsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Locations

        /// <summary>
        /// Gets a draft location by ID
        /// </summary>
        /// <param name="locationId">Location ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Draft location resource</returns>
        public DraftLocationResource GetLocation(string locationId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<DraftLocationResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a draft location by ID asynchronously
        /// </summary>
        /// <param name="locationId">Location ID</param>
        /// <returns>A Tuple containing draft location resource or/and failure response</returns>
        public async Task<Tuple<DraftLocationResource, ResultResponse>> GetLocationAsync(string locationId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<DraftLocationResource>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets a paginated list of draft locations with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of draft locations</returns>
        public DraftLocationCombinedCollection GetLocationsCombined(ListLocationsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftLocationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of locations with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<DraftLocationCombinedCollection, ResultResponse>> GetLocationsCombinedAsync(ListLocationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftLocationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets draft location clustering data
        /// </summary>
        /// <param name="request">Clustering request with zoom and filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Draft location clustering with clusters and locations</returns>
        public DraftLocationClustering GetLocationsClustering(LocationClusteringRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftLocationClustering>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsClustering,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets location clustering data asynchronously
        /// </summary>
        /// <param name="request">Clustering request with zoom and filters</param>
        /// <returns>A Tuple containing clustering data or/and failure response</returns>
        public async Task<Tuple<DraftLocationClustering, ResultResponse>> GetLocationsClusteringAsync(LocationClusteringRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftLocationClustering>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsClustering,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationClustering, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets draft location heatmap data
        /// </summary>
        /// <param name="request">Heatmap request with zoom and filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Draft location heatmap collection with lat/lng/intensity data</returns>
        public DraftLocationHeatmapCollection GetLocationsHeatmap(LocationHeatmapRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftLocationHeatmapCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsHeatmap,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets location heatmap data asynchronously
        /// </summary>
        /// <param name="request">Heatmap request with zoom and filters</param>
        /// <returns>A Tuple containing heatmap data or/and failure response</returns>
        public async Task<Tuple<DraftLocationHeatmapCollection, ResultResponse>> GetLocationsHeatmapAsync(LocationHeatmapRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftLocationHeatmapCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsHeatmap,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationHeatmapCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Updates a draft location (full update)
        /// </summary>
        /// <param name="locationId">Location ID to update</param>
        /// <param name="request">Update request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Updated draft location resource</returns>
        public DraftLocationResource UpdateLocation(string locationId, LocationUpdateRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftLocationResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Updates a location (full update) asynchronously
        /// </summary>
        /// <param name="locationId">Location ID to update</param>
        /// <param name="request">Update request</param>
        /// <returns>A Tuple containing updated location or/and failure response</returns>
        public async Task<Tuple<DraftLocationResource, ResultResponse>> UpdateLocationAsync(string locationId, LocationUpdateRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftLocationResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Patches a draft location (partial update)
        /// </summary>
        /// <param name="locationId">Location ID to patch</param>
        /// <param name="request">Patch request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Updated draft location resource</returns>
        public DraftLocationResource PatchLocation(string locationId, LocationUpdateRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftLocationResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Patch,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Patches a location (partial update) asynchronously
        /// </summary>
        /// <param name="locationId">Location ID to patch</param>
        /// <param name="request">Patch request</param>
        /// <returns>A Tuple containing updated location or/and failure response</returns>
        public async Task<Tuple<DraftLocationResource, ResultResponse>> PatchLocationAsync(string locationId, LocationUpdateRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftLocationResource>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Patch,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftLocationResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Deletes a draft location
        /// </summary>
        /// <param name="locationId">Location ID to delete</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool DeleteLocation(string locationId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            GetJsonObjectFromAPI<object>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Delete,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Deletes a draft optimization location asynchronously
        /// </summary>
        /// <param name="locationId">Location ID to delete</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> DeleteLocationAsync(string locationId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<object>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicLocationsById.Replace("{location_id}", locationId),
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Bulk updates locations
        /// </summary>
        /// <param name="request">Bulk update request with data and filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool BulkUpdateLocations(BulkUpdateLocationsRequest request, out ResultResponse resultResponse)
        {
            GetJsonObjectFromAPI<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsBulkUpdate,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Bulk updates locations asynchronously
        /// </summary>
        /// <param name="request">Bulk update request with data and filters</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> BulkUpdateLocationsAsync(BulkUpdateLocationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsBulkUpdate,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Bulk deletes locations
        /// </summary>
        /// <param name="request">Bulk delete request with IDs or filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>True if successful</returns>
        public bool BulkDeleteLocations(BulkDeleteLocationsRequest request, out ResultResponse resultResponse)
        {
            GetJsonObjectFromAPI<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsBulkDelete,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        /// Bulk deletes locations asynchronously
        /// </summary>
        /// <param name="request">Bulk delete request with IDs or filters</param>
        /// <returns>A Tuple containing success status and failure response</returns>
        public async Task<Tuple<bool, ResultResponse>> BulkDeleteLocationsAsync(BulkDeleteLocationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<object>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsBulkDelete,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item2 == null, result.Item2);
        }

        /// <summary>
        /// Exports locations to a file
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportLocations(ExportLocationsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsExport,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports locations to a file asynchronously
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportLocationsAsync(ExportLocationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicLocationsExport,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Routes

        /// <summary>
        /// Gets a paginated list of draft routes with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of routes</returns>
        public DraftRouteCombinedCollection GetRoutesCombined(ListRoutesRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftRouteCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicRoutesCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of draft routes with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<DraftRouteCombinedCollection, ResultResponse>> GetRoutesCombinedAsync(ListRoutesRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftRouteCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicRoutesCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftRouteCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports routes to a file
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportRoutes(ExportRoutesRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicRoutesExport,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports routes to a file asynchronously
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportRoutesAsync(ExportRoutesRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicRoutesExport,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Visits

        /// <summary>
        /// Gets a paginated list of draft visits with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of visits</returns>
        public DraftVisitCombinedCollection GetVisitsCombined(ListVisitsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftVisitCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicVisitsCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of draft visits with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<DraftVisitCombinedCollection, ResultResponse>> GetVisitsCombinedAsync(ListVisitsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftVisitCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicVisitsCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftVisitCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Exports visits to a file
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export job response with job_id for tracking</returns>
        public ExportJobResponse ExportVisits(ExportVisitsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicVisitsExport,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Exports visits to a file asynchronously
        /// </summary>
        /// <param name="request">Export request with filters and column selection</param>
        /// <returns>A Tuple containing export job response or/and failure response</returns>
        public async Task<Tuple<ExportJobResponse, ResultResponse>> ExportVisitsAsync(ExportVisitsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<ExportJobResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicVisitsExport,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportJobResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Strategic Destinations

        /// <summary>
        /// Gets a paginated list of draft destinations with filters and sorting
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Combined collection of destinations</returns>
        public DraftDestinationCombinedCollection GetDestinationsCombined(ListDestinationsRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DraftDestinationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicDestinationsCombined,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets a paginated list of draft destinations with filters and sorting asynchronously
        /// </summary>
        /// <param name="request">List request with filters</param>
        /// <returns>A Tuple containing combined collection or/and failure response</returns>
        public async Task<Tuple<DraftDestinationCombinedCollection, ResultResponse>> GetDestinationsCombinedAsync(ListDestinationsRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<DraftDestinationCombinedCollection>(
                request,
                R4MEInfrastructureSettingsV5.StrategicDestinationsCombined,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DraftDestinationCombinedCollection, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Export Operations

        /// <summary>
        /// Gets available export columns for the specified export type
        /// </summary>
        /// <param name="exportType">Export type (optimizations, locations, scenarios, visits, routes)</param>
        /// <param name="viewMode">View mode (optional)</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Array of column metadata</returns>
        public ColumnMetadata[] GetExportColumns(string exportType, string viewMode, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();
            
            var url = R4MEInfrastructureSettingsV5.StrategicExportColumns.Replace("{export_type}", exportType);
            if (!string.IsNullOrEmpty(viewMode))
            {
                url += "?view_mode=" + viewMode;
            }

            var result = GetJsonObjectFromAPI<ColumnMetadata[]>(
                genericParameters,
                url,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets available export columns for the specified export type asynchronously
        /// </summary>
        /// <param name="exportType">Export type (optimizations, locations, scenarios, visits, routes)</param>
        /// <param name="viewMode">View mode (optional)</param>
        /// <returns>A Tuple containing column metadata array or/and failure response</returns>
        public async Task<Tuple<ColumnMetadata[], ResultResponse>> GetExportColumnsAsync(string exportType, string viewMode)
        {
            var genericParameters = new GenericParameters();
            
            var url = R4MEInfrastructureSettingsV5.StrategicExportColumns.Replace("{export_type}", exportType);
            if (!string.IsNullOrEmpty(viewMode))
            {
                url += "?view_mode=" + viewMode;
            }

            var result = await GetJsonObjectFromAPIAsync<ColumnMetadata[]>(
                genericParameters,
                url,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ColumnMetadata[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Checks the status of an export job
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export status response</returns>
        public ExportStatusResponse GetExportStatus(int exportId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<ExportStatusResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportStatus.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Checks the status of an export job asynchronously
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <returns>A Tuple containing export status or/and failure response</returns>
        public async Task<Tuple<ExportStatusResponse, ResultResponse>> GetExportStatusAsync(int exportId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<ExportStatusResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportStatus.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportStatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets the result of an export job
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Export status response with Location header if ready</returns>
        public ExportStatusResponse GetExportResult(int exportId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<ExportStatusResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportResult.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets the result of an export job asynchronously
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <returns>A Tuple containing export status or/and failure response</returns>
        public async Task<Tuple<ExportStatusResponse, ResultResponse>> GetExportResultAsync(int exportId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<ExportStatusResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportResult.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ExportStatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Downloads the exported file
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>File content as byte array</returns>
        public byte[] DownloadExport(int exportId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<byte[]>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportDownload.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Downloads the exported file asynchronously
        /// </summary>
        /// <param name="exportId">Export job ID</param>
        /// <returns>A Tuple containing file content or/and failure response</returns>
        public async Task<Tuple<byte[], ResultResponse>> DownloadExportAsync(int exportId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<byte[]>(
                genericParameters,
                R4MEInfrastructureSettingsV5.StrategicExportDownload.Replace("{id}", exportId.ToString()),
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<byte[], ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Pattern Analysis

        /// <summary>
        /// Uploads a file for pattern analysis
        /// </summary>
        /// <param name="filePath">Path to the file to upload</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Upload response with upload_id</returns>
        public PatternAnalysisUploadFileResponse UploadPatternAnalysisFile(string filePath, out ResultResponse resultResponse)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "strFilename", System.IO.Path.GetFileName(filePath));

                var result = GetJsonObjectFromAPI<PatternAnalysisUploadFileResponse>(
                    new GenericParameters(),
                    R4MEInfrastructureSettingsV5.StrategicPatternAnalysisFile,
                    HttpMethodType.Post,
                    content,
                    true,
                    out resultResponse);

                return result;
            }
        }

        /// <summary>
        /// Uploads a file for pattern analysis asynchronously
        /// </summary>
        /// <param name="filePath">Path to the file to upload</param>
        /// <returns>A Tuple containing upload response or/and failure response</returns>
        public async Task<Tuple<PatternAnalysisUploadFileResponse, ResultResponse>> UploadPatternAnalysisFileAsync(string filePath)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                content.Add(fileContent, "strFilename", System.IO.Path.GetFileName(filePath));

                var result = await GetJsonObjectFromAPIAsync<PatternAnalysisUploadFileResponse>(
                    new GenericParameters(),
                    R4MEInfrastructureSettingsV5.StrategicPatternAnalysisFile,
                    HttpMethodType.Post,
                    content,
                    true,
                    false).ConfigureAwait(false);

                return new Tuple<PatternAnalysisUploadFileResponse, ResultResponse>(result.Item1, result.Item2);
            }
        }

        /// <summary>
        /// Gets preview of pattern analysis file
        /// </summary>
        /// <param name="request">Preview request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Preview data</returns>
        public PatternAnalysisUploadPreviewResponse GetPatternAnalysisPreview(PatternAnalysisUploadPreviewRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<PatternAnalysisUploadPreviewResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicPatternAnalysisPreview,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Gets preview of pattern analysis file asynchronously
        /// </summary>
        /// <param name="request">Preview request</param>
        /// <returns>A Tuple containing preview data or/and failure response</returns>
        public async Task<Tuple<PatternAnalysisUploadPreviewResponse, ResultResponse>> GetPatternAnalysisPreviewAsync(PatternAnalysisUploadPreviewRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<PatternAnalysisUploadPreviewResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicPatternAnalysisPreview,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<PatternAnalysisUploadPreviewResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Initiates pattern analysis on uploaded file
        /// </summary>
        /// <param name="request">Initiate request</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Initiation response with job_id</returns>
        public InitiatePatternAnalysisResponse InitiatePatternAnalysis(InitiatePatternAnalysisRequest request, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<InitiatePatternAnalysisResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicPatternAnalysis,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Initiates pattern analysis on uploaded file asynchronously
        /// </summary>
        /// <param name="request">Initiate request</param>
        /// <returns>A Tuple containing initiation response or/and failure response</returns>
        public async Task<Tuple<InitiatePatternAnalysisResponse, ResultResponse>> InitiatePatternAnalysisAsync(InitiatePatternAnalysisRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<InitiatePatternAnalysisResponse>(
                request,
                R4MEInfrastructureSettingsV5.StrategicPatternAnalysis,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<InitiatePatternAnalysisResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion
    }
}
