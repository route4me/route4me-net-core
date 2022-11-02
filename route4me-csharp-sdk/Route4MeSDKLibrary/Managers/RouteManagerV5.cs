using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Routes;
using RouteParametersQuery = Route4MeSDK.QueryTypes.V5.RouteParametersQuery;

namespace Route4MeSDKLibrary.Managers
{
    public class RouteManagerV5 : Route4MeManagerBase
    {
        public RouteManagerV5(string apiKey) : base(apiKey)
        {
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
                HttpMethodType.Get,
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
                HttpMethodType.Get,
                null, true, false).ConfigureAwait(false);

            return new Tuple<DataObjectRoute[], ResultResponse>(result.Item1, result.Item2);
        }


        public DataObjectRoute GetRouteById(RouteParametersQuery routeParameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }


        public async Task<Tuple<DataObjectRoute, ResultResponse>> GetRouteByIdAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);
            return new Tuple<DataObjectRoute, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Retrieves a paginated list of the routes.
        /// </summary>
        /// <param name="routeParameters">Query parameters <see cref="RouteParametersQuery"/></param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>An array of the routes</returns>
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

        public DataObjectRoute[] GetRouteListWithoutElasticSearch(RouteParametersQuery routeParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.Get, null, false, true,
                out resultResponse);

            return result;
        }

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
                R4MEInfrastructureSettingsV5.Routes,
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
                R4MEInfrastructureSettingsV5.Routes,
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
    }
}
