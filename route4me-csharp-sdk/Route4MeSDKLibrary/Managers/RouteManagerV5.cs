using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.Routes;
using Route4MeSDKLibrary.Resilience;

using RouteParametersQuery = Route4MeSDK.QueryTypes.V5.RouteParametersQuery;

namespace Route4MeSDKLibrary.Managers
{
    public class RouteManagerV5 : Route4MeManagerBase
    {
        public RouteManagerV5(string apiKey) : base(apiKey)
        {
        }

        public RouteManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        /// Retrieves a single route by ID with full details including addresses and parameters.
        /// Uses GET /api/v5.0/routes/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>The route response with data property containing route details</returns>
        public GetRouteResponse GetRoute(string routeId, out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var response = GetJsonObjectFromAPI<GetRouteResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.Routes + "/" + routeId,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Retrieves a single route by ID with full details including addresses and parameters asynchronously.
        /// Uses GET /api/v5.0/routes/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <returns>A Tuple type object containing the route response or/and failure response</returns>
        public async Task<Tuple<GetRouteResponse, ResultResponse>> GetRouteAsync(string routeId)
        {
            var genericParameters = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<GetRouteResponse>(
                genericParameters,
                R4MEInfrastructureSettingsV5.Routes + "/" + routeId,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<GetRouteResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Retrieves a list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRoutesAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Post,
                null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a list of the routes via the routes/list endpoint (POST /api/v5.0/routes/list).
        /// The API returns { data, links, meta }; items in data are deserialized as <see cref="DataObjectRouteExtended"/>.
        /// </summary>
        /// <param name="routeParameters">Query parameters</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes (from response.data)</returns>
        public DataObjectRoute[] GetRoutesList(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            // So that POST /routes/list filters by route_id: send it in the body (filters.route_id), not only in query.
            if (!string.IsNullOrEmpty(routeParameters.RouteId) && routeParameters.Filters == null)
                routeParameters.Filters = new RouteFilterParametersFilters { RouteId = routeParameters.RouteId };

            var response = GetJsonObjectFromAPI<RoutesListResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesList,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return response?.Data;
        }

        /// <summary>
        /// Retrieves a list of the routes via the routes/list endpoint asynchronously (POST /api/v5.0/routes/list).
        /// The API returns { data, links, meta }; items in data are deserialized as <see cref="DataObjectRouteExtended"/>.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list (from response.data) or/and failure response</returns>
        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRoutesListAsync(RouteParametersQuery routeParameters)
        {
            // So that POST /routes/list filters by route_id: send it in the body (filters.route_id), not only in query.
            if (!string.IsNullOrEmpty(routeParameters.RouteId) && routeParameters.Filters == null)
                routeParameters.Filters = new RouteFilterParametersFilters { RouteId = routeParameters.RouteId };

            var response = await GetJsonObjectFromAPIAsync<RoutesListResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesList,
                HttpMethodType.Post,
                null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(response.Item1?.Data, response.Item2);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public RoutesResponse GetAllRoutesWithPagination(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Retrieves a paginated list of the routes asynchronously.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public async Task<Tuple<RoutesResponse, ResultResponse>> GetAllRoutesWithPaginationAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesPaginate,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes without elastic search.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public RoutesResponse GetPaginatedRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a paginated list of the routes without elastic search.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <returns>A Tuple type object containing a route list or/and failure response</returns>
        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public async Task<Tuple<RoutesResponse, ResultResponse>> GetPaginatedRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get a route list by filtering.
        /// </summary>
        /// <param name="routeFilterParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An object <see cref="RouteFilterResponse"/></returns>
        public RouteFilterResponse GetRoutesByFilter(RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RouteFilterResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.Routes51,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get a route list by filtering asynchronously.
        /// </summary>
        /// <param name="routeFilterParameters">Route filtering parameters</param>
        /// <returns>A RouteFilterResponse type object<see cref="RouteFilterResponse"/></returns>
        public Task<Tuple<RouteFilterResponse, ResultResponse>> GetRoutesByFilterAsync(RouteFilterParameters routeFilterParameters)
        {
            var result = GetJsonObjectFromAPIAsync<RouteFilterResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.Routes51,
                HttpMethodType.Post);

            return result;
        }

        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public RoutesResponse GetRouteDataTableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<RoutesResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post, null, false, true,
                out resultResponse);

            return result;
        }

        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public async Task<Tuple<RoutesResponse, ResultResponse>> GetRouteDataTableWithElasticSearchAsync(RouteFilterParameters routeFilterParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RoutesResponse>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<RoutesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        public DataObjectRoute[] GetRouteDatatableWithElasticSearch(
            RouteFilterParameters routeFilterParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post,
                null, false, true,
                out resultResponse);

            return result;
        }

        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteDatatableWithElasticSearchAsync(
            RouteFilterParameters routeFilterParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatable,
                HttpMethodType.Post, null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }

        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

        [Obsolete("This method is deprecated and will be removed in a future version.")]
        public async Task<Tuple<DataObjectRoute[], ResultResponse>> GetRouteListWithoutElasticSearchAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Makes a copy of the existing route.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A RouteDuplicateResponse type object <see cref="RouteDuplicateResponse"/></returns>
        public RouteDuplicateResponse DuplicateRoute(string[] routeIDs, out ResultResponse resultResponse)
        {
            var duplicateParameter = new Dictionary<string, string[]>
            {
                {
                    "duplicate_routes_id", routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Makes a copy of the existing route asynchronously.
        /// </summary>
        /// <param name="routeIDs">An array of route IDs.</param>
        /// <returns>A Tuple type object containing an array of the duplicated route IDs
        /// or/and failure response</returns>
        public async Task<Tuple<RouteDuplicateResponse, ResultResponse>> DuplicateRouteAsync(string[] routeIDs)
        {
            var duplicateParameter = new Dictionary<string, string[]>
            {
                {
                    "duplicate_routes_id", routeIDs
                }
            };

            var duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, true);

            var content = new StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = await GetJsonObjectFromAPIAsync<RouteDuplicateResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                false,
                false).ConfigureAwait(false);

            return new Tuple<RouteDuplicateResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Removes specified routes from the account.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A DeleteRoutes type object <see cref="RoutesDeleteResponse"/></returns>
        public RoutesDeleteResponse DeleteRoutes(string[] routeIds, out ResultResponse resultResponse)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            var response = GetJsonObjectFromAPI<RoutesDeleteResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDelete,
                HttpMethodType.Delete,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Removes specified routes from the account asynchronously.
        /// </summary>
        /// <param name="routeIds">An array of route IDs.</param>
        /// <returns>A Tuple type object containing deleted route ID(s) 
        /// or/and failure response</returns>
        public Task<Tuple<RoutesDeleteResponse, ResultResponse>> DeleteRoutesAsync(string[] routeIds)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            return GetJsonObjectFromAPIAsync<RoutesDeleteResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDelete,
                HttpMethodType.Delete);
        }

        public RouteDataTableConfigResponse GetRouteDataTableConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableConfigAsync()
        {
            var genericParameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.Get);
        }

        public RouteDataTableConfigResponse GetRouteDataTableFallbackConfig(out ResultResponse resultResponse)
        {
            var genericParameters = new GenericParameters();

            var result = GetJsonObjectFromAPI<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        public Task<Tuple<RouteDataTableConfigResponse, ResultResponse>> GetRouteDataTableFallbackConfigAsync()
        {
            var genericParameters = new GenericParameters();

            return GetJsonObjectFromAPIAsync<RouteDataTableConfigResponse>(genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Updates a route by sending route query parameters containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <param name="resultResponse"></param>
        /// <returns>Updated route</returns>
        public DataObjectRoute UpdateRoute(RouteParametersQuery routeQuery, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<DataObjectRoute>(
                routeQuery,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                false,
                true,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Updates asynchronously a route by sending route query parameters  
        /// containg Route Parameters and Addresses.
        /// </summary>
        /// <param name="routeQuery">Route query parameters</param>
        /// <returns>A Tuple type object containing updated route or/and failure response</returns>
        public Task<Tuple<DataObjectRoute, ResultResponse, string>> UpdateRouteAsync(RouteParametersQuery routeQuery)
        {
            var response = GetJsonObjectFromAPIAsync<DataObjectRoute>(
                routeQuery,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                null,
                true,
                false);

            return response;
        }

        /// <summary>
        /// Inserts the route breaks.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>The status of the inserting process </returns>
        public StatusResponse InsertRouteBreaks(RouteBreaks breaks, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(
                breaks,
                R4MEInfrastructureSettingsV5.RouteBreaks,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Inserts the route breaks asyncronously.
        /// </summary>
        /// <param name="breaks">Route breaks <see cref="RouteBreaks"/></param>
        /// <returns>A Tuple type object containing the status of the inserting process
        /// or/and failure response</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> InsertRouteBreaksAsync(RouteBreaks breaks)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(
                breaks,
                R4MEInfrastructureSettingsV5.RouteBreaks,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        /// Returns found routes for the specified scheduled date and location.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found routes with predicted time and distance parameters</returns>
        public DynamicInsertMatchedRoute[] DynamicInsertRouteAddresses(
                                                DynamicInsertRequest dynamicInsertRequest,
                                                out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DynamicInsertMatchedRoute[]>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsert,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Returns found routes for the specified scheduled date and location asynchronously.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <returns>A Tuple type object containing the matched routes 
        /// or/and failure response</returns>
        public Task<Tuple<DynamicInsertMatchedRoute[], ResultResponse, string>> DynamicInsertRouteAddressesAsync(
                                                DynamicInsertRequest dynamicInsertRequest)
        {
            return GetJsonObjectFromAPIAsync<DynamicInsertMatchedRoute[]>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsert,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        /// Dispatches an asynchronous lookup of the best positions to insert a new destination.
        /// Poll <see cref="GetDynamicInsertJobStatus"/>, then fetch <see cref="GetDynamicInsertJobResult"/>.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The accepted job</returns>
        public DynamicInsertJob DispatchDynamicInsertRouteAddresses(
                                                DynamicInsertRequest dynamicInsertRequest,
                                                out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DynamicInsertJob>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsertAsync,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Dispatches an asynchronous lookup of the best positions to insert a new destination asynchronously.
        /// </summary>
        /// <param name="dynamicInsertRequest">Request body parameters</param>
        /// <returns>A Tuple type object containing the accepted job or/and failure response</returns>
        public Task<Tuple<DynamicInsertJob, ResultResponse>> DispatchDynamicInsertRouteAddressesAsync(
                                                DynamicInsertRequest dynamicInsertRequest)
        {
            return GetJsonObjectFromAPIAsync<DynamicInsertJob>(
                dynamicInsertRequest,
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsertAsync,
                HttpMethodType.Post);
        }

        /// <summary>
        /// Returns the status of an asynchronous dynamic insert job.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The job status</returns>
        public DynamicInsertJobResult GetDynamicInsertJobStatus(string jobId, out ResultResponse resultResponse)
        {
            var result = GetDynamicInsertJobStatusAsync(jobId).GetAwaiter().GetResult();

            resultResponse = result.Item2;

            return result.Item1;
        }

        /// <summary>
        /// Returns the status of an asynchronous dynamic insert job asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing the job status or/and failure response</returns>
        public async Task<Tuple<DynamicInsertJobResult, ResultResponse>> GetDynamicInsertJobStatusAsync(string jobId)
        {
            try
            {
                var url = R4MEInfrastructureSettingsV5.RouteAddressDynamicInsertJobStatus + "/" + jobId;

                using (var response = await HttpResiliencePolicy.GetAsyncPolicy()
                           .ExecuteAsync(async () =>
                           {
                               // A request message cannot be sent twice, so each retry builds a new one.
                               using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                               {
                                   request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                                   request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                   return await NoRedirectHttpClient.Value.SendAsync(request).ConfigureAwait(false);
                               }
                           })
                           .ConfigureAwait(false))
                {
                    // 303 means the job is done; its body carries the final status.
                    if (response.StatusCode != HttpStatusCode.SeeOther && !response.IsSuccessStatusCode)
                    {
                        var errorMessageContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        return new Tuple<DynamicInsertJobResult, ResultResponse>(null, new ResultResponse
                        {
                            Status = false,
                            Messages = new Dictionary<string, string[]>
                            {
                                { "ErrorMessageContent", new[] { errorMessageContent } }
                            }
                        });
                    }

                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    return new Tuple<DynamicInsertJobResult, ResultResponse>(
                        stream.ReadObjectNew<DynamicInsertJobResult>(), null);
                }
            }
            catch (Exception e)
            {
                var resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]> { { "Error", new[] { e.Message } } }
                };

                return new Tuple<DynamicInsertJobResult, ResultResponse>(null, resultResponse);
            }
        }

        // The job status endpoint answers 303 when the job is done, and .NET drops the Authorization
        // header when it follows a redirect, so the follow-up request fails with 401.
        // This client does not follow redirects, so the 303 body can be read directly.
        private static readonly Lazy<HttpClient> NoRedirectHttpClient = new Lazy<HttpClient>(() =>
        {
            var handler = Route4MeConfig.HttpMessageHandlerFactory?.Invoke() ?? new HttpClientHandler();

            // A custom handler usually wraps the transport handler, so walk the chain to reach it.
            var transport = handler;
            while (transport is DelegatingHandler delegatingHandler)
            {
                transport = delegatingHandler.InnerHandler;
            }

            // Reflection also reaches SocketsHttpHandler, which netstandard2.0 cannot reference.
            transport?.GetType().GetProperty("AllowAutoRedirect")?.SetValue(transport, false);

            return new HttpClient(handler) { Timeout = Route4MeConfig.HttpTimeout };
        });

        /// <summary>
        /// Returns the result of a finished asynchronous dynamic insert job.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The job result with the best insertion options</returns>
        public DynamicInsertJobResult GetDynamicInsertJobResult(string jobId, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DynamicInsertJobResult>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsertJobResult + "/" + jobId,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        /// Returns the result of a finished asynchronous dynamic insert job asynchronously.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>A Tuple type object containing the job result or/and failure response</returns>
        public async Task<Tuple<DynamicInsertJobResult, ResultResponse>> GetDynamicInsertJobResultAsync(string jobId)
        {
            var result = await GetJsonObjectFromAPIAsync<DynamicInsertJobResult>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteAddressDynamicInsertJobResult + "/" + jobId,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DynamicInsertJobResult, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets the route-level custom data for the specified route.
        /// Uses GET /api/v5.0/routes/{route_id} and extracts the custom_data field.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of dictionaries representing route custom data, or null if not set</returns>
        [Obsolete("Use RouteCustomDataManagerV5.GetRouteCustomDataDedicated instead")]
        public Dictionary<string, string> GetRouteCustomData(string routeId, out ResultResponse resultResponse)
        {
            var response = GetRoute(routeId, out resultResponse);

            return response?.Data?.RouteCustomData;
        }

        /// <summary>
        /// Gets the route-level custom data for the specified route asynchronously.
        /// Uses GET /api/v5.0/routes/{route_id} and extracts the custom_data field.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <returns>A Tuple containing route custom data or/and failure response</returns>
        [Obsolete("Use RouteCustomDataManagerV5.GetRouteCustomDataDedicatedAsync instead")]
        public async Task<Tuple<Dictionary<string, string>, ResultResponse>> GetRouteCustomDataAsync(string routeId)
        {
            var result = await GetRouteAsync(routeId).ConfigureAwait(false);

            return new Tuple<Dictionary<string, string>, ResultResponse>(
                result.Item1?.Data?.RouteCustomData,
                result.Item2);
        }

        /// <summary>
        /// Updates route-level custom data for the specified route.
        /// Uses PUT /api/v5.0/routes with custom_data in the request body.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <param name="customData">The custom data to set on the route</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>The updated route object</returns>
        [Obsolete("Use RouteCustomDataManagerV5.UpdateRouteCustomData which calls the dedicated PUT /route-custom-data/{route_id} endpoint.")]
        public DataObjectRoute UpdateRouteCustomData(
            string routeId,
            Dictionary<string, string>[] customData,
            out ResultResponse resultResponse)
        {
            var routeQuery = new RouteParametersQuery
            {
                RouteId = routeId,
                RouteCustomData = customData
            };

            return UpdateRoute(routeQuery, out resultResponse);
        }

        /// <summary>
        /// Updates route-level custom data for the specified route asynchronously.
        /// Uses PUT /api/v5.0/routes with custom_data in the request body.
        /// </summary>
        /// <param name="routeId">The route ID (32-character hex string)</param>
        /// <param name="customData">The custom data to set on the route</param>
        /// <returns>A Tuple containing the updated route or/and failure response</returns>
        [Obsolete("Use RouteCustomDataManagerV5.UpdateRouteCustomDataAsync which calls the dedicated PUT /route-custom-data/{route_id} endpoint.")]
        public Task<Tuple<DataObjectRoute, ResultResponse, string>> UpdateRouteCustomDataAsync(
            string routeId,
            Dictionary<string, string>[] customData)
        {
            var routeQuery = new RouteParametersQuery
            {
                RouteId = routeId,
                RouteCustomData = customData
            };

            return UpdateRouteAsync(routeQuery);
        }
    }
}