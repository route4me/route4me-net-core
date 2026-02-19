using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Manager for the Route Custom Data API (V5).
    /// Provides dedicated CRUD operations for route-level custom data via the
    /// <c>/route-custom-data/</c> endpoints, distinct from the route-level
    /// <c>route_custom_data</c> field in the Routes API.
    /// </summary>
    public class RouteCustomDataManagerV5 : Route4MeManagerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RouteCustomDataManagerV5"/> with an API key.
        /// </summary>
        /// <param name="apiKey">The Route4Me API key.</param>
        public RouteCustomDataManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RouteCustomDataManagerV5"/> with an API key and logger.
        /// </summary>
        /// <param name="apiKey">The Route4Me API key.</param>
        /// <param name="logger">Logger instance for diagnostic output.</param>
        public RouteCustomDataManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        /// Gets the custom data for the specified route.
        /// Uses GET /route-custom-data/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">Route ID (32-character hex string).</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A dictionary of custom key-value pairs for the route, or <c>null</c> on failure.
        /// Returns an empty dictionary if the route has no custom data.
        /// </returns>
        public Dictionary<string, string> GetRouteCustomData(string routeId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(routeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeId parameter is not specified" } }
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<Dictionary<string, string>>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomData + "/" + routeId,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        /// Gets the custom data for the specified route asynchronously.
        /// Uses GET /route-custom-data/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">Route ID (32-character hex string).</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the custom data dictionary
        /// and the failure response (if any).
        /// </returns>
        public async Task<Tuple<Dictionary<string, string>, ResultResponse>> GetRouteCustomDataAsync(string routeId)
        {
            if (string.IsNullOrEmpty(routeId))
            {
                return new Tuple<Dictionary<string, string>, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeId parameter is not specified" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<Dictionary<string, string>>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomData + "/" + routeId,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Dictionary<string, string>, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Updates the custom data for the specified route, replacing all existing entries.
        /// Keys not present in <paramref name="customData"/> are removed.
        /// To clear all custom data, submit a single key with a <c>null</c> value.
        /// Uses PUT /route-custom-data/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">Route ID (32-character hex string).</param>
        /// <param name="customData">Custom data key-value pairs to set on the route.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// The updated custom data dictionary as stored by the API, or <c>null</c> on failure.
        /// </returns>
        public Dictionary<string, string> UpdateRouteCustomData(
            string routeId,
            Dictionary<string, string> customData,
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(routeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeId parameter is not specified" } }
                    }
                };

                return null;
            }

            if (customData == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The customData parameter is null" } }
                    }
                };

                return null;
            }

            var json = R4MeUtils.SerializeObjectToJson(customData, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return GetJsonObjectFromAPI<Dictionary<string, string>>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomData + "/" + routeId,
                HttpMethodType.Put,
                content,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        /// Updates the custom data for the specified route asynchronously, replacing all existing entries.
        /// Keys not present in <paramref name="customData"/> are removed.
        /// To clear all custom data, submit a single key with a <c>null</c> value.
        /// Uses PUT /route-custom-data/{route_id} endpoint.
        /// </summary>
        /// <param name="routeId">Route ID (32-character hex string).</param>
        /// <param name="customData">Custom data key-value pairs to set on the route.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the updated custom data dictionary
        /// and the failure response (if any).
        /// </returns>
        public async Task<Tuple<Dictionary<string, string>, ResultResponse>> UpdateRouteCustomDataAsync(
            string routeId,
            Dictionary<string, string> customData)
        {
            if (string.IsNullOrEmpty(routeId))
            {
                return new Tuple<Dictionary<string, string>, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeId parameter is not specified" } }
                    }
                });
            }

            if (customData == null)
            {
                return new Tuple<Dictionary<string, string>, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The customData parameter is null" } }
                    }
                });
            }

            var json = R4MeUtils.SerializeObjectToJson(customData, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await GetJsonObjectFromAPIAsync<Dictionary<string, string>>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomData + "/" + routeId,
                HttpMethodType.Put,
                content,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Dictionary<string, string>, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets the custom data for multiple routes by their IDs in a single request.
        /// Uses POST /route-custom-data/bulk endpoint.
        /// </summary>
        /// <param name="routeIds">Array of route IDs (32-character hex strings).</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteCustomDataCollection"/> whose <c>Data</c> property maps
        /// each route ID to its custom data dictionary, or <c>null</c> on failure.
        /// </returns>
        public RouteCustomDataCollection GetBulkRouteCustomData(
            string[] routeIds,
            out ResultResponse resultResponse)
        {
            if (routeIds == null || routeIds.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeIds array is null or empty" } }
                    }
                };

                return null;
            }

            var requestBody = new Dictionary<string, string[]> { { "route_ids", routeIds } };
            var json = R4MeUtils.SerializeObjectToJson(requestBody, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return GetJsonObjectFromAPI<RouteCustomDataCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomDataBulk,
                HttpMethodType.Post,
                content,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        /// Gets the custom data for multiple routes by their IDs in a single request asynchronously.
        /// Uses POST /route-custom-data/bulk endpoint.
        /// </summary>
        /// <param name="routeIds">Array of route IDs (32-character hex strings).</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing a <see cref="RouteCustomDataCollection"/>
        /// and the failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteCustomDataCollection, ResultResponse>> GetBulkRouteCustomDataAsync(
            string[] routeIds)
        {
            if (routeIds == null || routeIds.Length == 0)
            {
                return new Tuple<RouteCustomDataCollection, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The routeIds array is null or empty" } }
                    }
                });
            }

            var requestBody = new Dictionary<string, string[]> { { "route_ids", routeIds } };
            var json = R4MeUtils.SerializeObjectToJson(requestBody, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await GetJsonObjectFromAPIAsync<RouteCustomDataCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteCustomDataBulk,
                HttpMethodType.Post,
                content,
                true,
                false).ConfigureAwait(false);

            return new Tuple<RouteCustomDataCollection, ResultResponse>(result.Item1, result.Item2);
        }
    }
}