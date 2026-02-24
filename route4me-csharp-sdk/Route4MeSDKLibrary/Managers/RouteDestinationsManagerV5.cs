using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Manager for the Route Destinations API (V5).
    /// Provides access to the <c>/route-destinations</c> endpoints, including
    /// list, combined list, single destination lookup, column management,
    /// field metadata, label-code lookup, and sequence optimisation.
    /// </summary>
    public class RouteDestinationsManagerV5 : Route4MeManagerBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref="RouteDestinationsManagerV5"/> with an API key.
        /// </summary>
        /// <param name="apiKey">Route4Me API key.</param>
        public RouteDestinationsManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="RouteDestinationsManagerV5"/>
        /// with an API key and logger.
        /// </summary>
        /// <param name="apiKey">Route4Me API key.</param>
        /// <param name="logger">Logger instance for diagnostic output.</param>
        public RouteDestinationsManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        #region Columns

        /// <summary>
        /// Gets the columns list and their saved display order for the given configurator key.
        /// Uses GET /route-destinations/columns.
        /// </summary>
        /// <param name="columnsConfiguratorKey">Configuration key identifying the saved column set.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteDestinationColumnsResource"/> describing available columns
        /// and their saved order, or <c>null</c> on failure.
        /// </returns>
        public RouteDestinationColumnsResource GetDestinationColumns(
            string columnsConfiguratorKey,
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(columnsConfiguratorKey))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The columnsConfiguratorKey parameter is not specified" } }
                    }
                };
                return null;
            }

            var parameters = new GenericParameters();
            parameters.ParametersCollection.Add("columns_configurator_key", columnsConfiguratorKey);

            return GetJsonObjectFromAPI<RouteDestinationColumnsResource>(
                parameters,
                R4MEInfrastructureSettingsV5.RouteDestinationsColumns,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets the columns list asynchronously.
        /// Uses GET /route-destinations/columns.
        /// </summary>
        /// <param name="columnsConfiguratorKey">Configuration key identifying the saved column set.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the columns resource and failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteDestinationColumnsResource, ResultResponse>> GetDestinationColumnsAsync(
            string columnsConfiguratorKey)
        {
            if (string.IsNullOrEmpty(columnsConfiguratorKey))
            {
                return new Tuple<RouteDestinationColumnsResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The columnsConfiguratorKey parameter is not specified" } }
                    }
                });
            }

            var parameters = new GenericParameters();
            parameters.ParametersCollection.Add("columns_configurator_key", columnsConfiguratorKey);

            var result = await GetJsonObjectFromAPIAsync<RouteDestinationColumnsResource>(
                parameters,
                R4MEInfrastructureSettingsV5.RouteDestinationsColumns,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Updates the display order of destination columns for the given configurator key.
        /// Uses PUT /route-destinations/columns.
        /// </summary>
        /// <param name="request">Request containing the configurator key and desired column order.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// An <see cref="EditDestinationColumnsResponse"/> with the saved column order,
        /// or <c>null</c> on failure.
        /// </returns>
        public EditDestinationColumnsResponse EditDestinationColumns(
            EditDestinationColumnsRequest request,
            out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                };
                return null;
            }

            var json = R4MeUtils.SerializeObjectToJson(request, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return GetJsonObjectFromAPI<EditDestinationColumnsResponse>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinationsColumns,
                HttpMethodType.Put,
                content,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        /// Updates the display order of destination columns asynchronously.
        /// Uses PUT /route-destinations/columns.
        /// </summary>
        /// <param name="request">Request containing the configurator key and desired column order.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the updated columns response
        /// and failure response (if any).
        /// </returns>
        public async Task<Tuple<EditDestinationColumnsResponse, ResultResponse>> EditDestinationColumnsAsync(
            EditDestinationColumnsRequest request)
        {
            if (request == null)
            {
                return new Tuple<EditDestinationColumnsResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                });
            }

            var json = R4MeUtils.SerializeObjectToJson(request, true);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await GetJsonObjectFromAPIAsync<EditDestinationColumnsResponse>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinationsColumns,
                HttpMethodType.Put,
                content,
                true,
                false).ConfigureAwait(false);

            return new Tuple<EditDestinationColumnsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Get Destination

        /// <summary>
        /// Gets a single route destination by its integer ID or UUID.
        /// Uses GET /route-destinations/{id}.
        /// </summary>
        /// <param name="id">Destination integer ID or 32-character hex UUID.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteDestinationResource"/> for the requested destination,
        /// or <c>null</c> on failure.
        /// </returns>
        public RouteDestinationResource GetDestination(string id, out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(id))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The id parameter is not specified" } }
                    }
                };
                return null;
            }

            return GetJsonObjectFromAPI<RouteDestinationResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinations + "/" + id,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets a single route destination asynchronously.
        /// Uses GET /route-destinations/{id}.
        /// </summary>
        /// <param name="id">Destination integer ID or 32-character hex UUID.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the destination resource and failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteDestinationResource, ResultResponse>> GetDestinationAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Tuple<RouteDestinationResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The id parameter is not specified" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<RouteDestinationResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinations + "/" + id,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region List

        /// <summary>
        /// Gets a paginated list of route destinations matching the specified filters.
        /// Uses POST /route-destinations/list.
        /// </summary>
        /// <param name="request">Filter, pagination and field-selection parameters.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteDestinationsListResponse"/> containing matching destination items,
        /// or <c>null</c> on failure.
        /// </returns>
        public RouteDestinationsListResponse GetDestinationsList(
            GetDestinationsRequest request,
            out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                };
                return null;
            }

            return GetJsonObjectFromAPI<RouteDestinationsListResponse>(
                request,
                R4MEInfrastructureSettingsV5.RouteDestinationsList,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Gets a paginated list of route destinations asynchronously.
        /// Uses POST /route-destinations/list.
        /// </summary>
        /// <param name="request">Filter, pagination and field-selection parameters.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the list response and failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteDestinationsListResponse, ResultResponse>> GetDestinationsListAsync(
            GetDestinationsRequest request)
        {
            if (request == null)
            {
                return new Tuple<RouteDestinationsListResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<RouteDestinationsListResponse>(
                request,
                R4MEInfrastructureSettingsV5.RouteDestinationsList,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Gets the list of available destination fields for filtering and sorting.
        /// Uses GET /route-destinations/list/fields.
        /// </summary>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteDestinationFieldsResponse"/> with field metadata,
        /// or <c>null</c> on failure.
        /// </returns>
        public RouteDestinationFieldsResponse GetDestinationFields(out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<RouteDestinationFieldsResponse>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinationsListFields,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets the list of available destination fields asynchronously.
        /// Uses GET /route-destinations/list/fields.
        /// </summary>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the fields response and failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteDestinationFieldsResponse, ResultResponse>> GetDestinationFieldsAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<RouteDestinationFieldsResponse>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteDestinationsListFields,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Sorting

        /// <summary>
        /// Looks up a destination by its label code and returns route/stop context information.
        /// Used for sorting and filtering UIs.
        /// Uses GET /route-destinations/sorting?label_code={labelCode}.
        /// </summary>
        /// <param name="labelCode">Label code assigned to the destination (e.g., "label_5").</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="SortingFilteringResource"/> with destination context,
        /// or <c>null</c> on failure.
        /// </returns>
        public SortingFilteringResource GetDestinationByLabelCode(
            string labelCode,
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(labelCode))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The labelCode parameter is not specified" } }
                    }
                };
                return null;
            }

            var parameters = new GenericParameters();
            parameters.ParametersCollection.Add("label_code", labelCode);

            return GetJsonObjectFromAPI<SortingFilteringResource>(
                parameters,
                R4MEInfrastructureSettingsV5.RouteDestinationsSorting,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Looks up a destination by its label code asynchronously.
        /// Uses GET /route-destinations/sorting?label_code={labelCode}.
        /// </summary>
        /// <param name="labelCode">Label code assigned to the destination.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the sorting resource and failure response (if any).
        /// </returns>
        public async Task<Tuple<SortingFilteringResource, ResultResponse>> GetDestinationByLabelCodeAsync(
            string labelCode)
        {
            if (string.IsNullOrEmpty(labelCode))
            {
                return new Tuple<SortingFilteringResource, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The labelCode parameter is not specified" } }
                    }
                });
            }

            var parameters = new GenericParameters();
            parameters.ParametersCollection.Add("label_code", labelCode);

            var result = await GetJsonObjectFromAPIAsync<SortingFilteringResource>(
                parameters,
                R4MEInfrastructureSettingsV5.RouteDestinationsSorting,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Order

        /// <summary>
        /// Gets all route destinations linked to a specific order UUID.
        /// Uses GET /route-destinations/order/{order_uuid}.
        /// </summary>
        /// <param name="orderUuid">32-character hex UUID of the order.</param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="RouteDestinationsByOrderResponse"/> containing matched destinations,
        /// or <c>null</c> on failure.
        /// </returns>
        public RouteDestinationsByOrderResponse GetDestinationsByOrder(
            string orderUuid,
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrEmpty(orderUuid))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The orderUuid parameter is not specified" } }
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.RouteDestinations + "/order/" + orderUuid;

            return GetJsonObjectFromAPI<RouteDestinationsByOrderResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets all route destinations linked to a specific order UUID asynchronously.
        /// Uses GET /route-destinations/order/{order_uuid}.
        /// </summary>
        /// <param name="orderUuid">32-character hex UUID of the order.</param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the by-order response and failure response (if any).
        /// </returns>
        public async Task<Tuple<RouteDestinationsByOrderResponse, ResultResponse>> GetDestinationsByOrderAsync(
            string orderUuid)
        {
            if (string.IsNullOrEmpty(orderUuid))
            {
                return new Tuple<RouteDestinationsByOrderResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The orderUuid parameter is not specified" } }
                    }
                });
            }

            var url = R4MEInfrastructureSettingsV5.RouteDestinations + "/order/" + orderUuid;

            var result = await GetJsonObjectFromAPIAsync<RouteDestinationsByOrderResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Sequence

        /// <summary>
        /// Computes the optimised visit sequence for a list of coordinates.
        /// Uses POST /route-destinations/sequence.
        /// </summary>
        /// <param name="request">
        /// A <see cref="DestinationSequenceRequest"/> containing the lat/lng coordinates to optimise.
        /// </param>
        /// <param name="resultResponse">Failure response if the request fails.</param>
        /// <returns>
        /// A <see cref="DestinationSequenceListResponse"/> with the optimised index sequence,
        /// or <c>null</c> on failure.
        /// </returns>
        public DestinationSequenceListResponse GetDestinationSequence(
            DestinationSequenceRequest request,
            out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                };
                return null;
            }

            if (request.Coordinates == null || request.Coordinates.Length == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The Coordinates array is null or empty" } }
                    }
                };
                return null;
            }

            return GetJsonObjectFromAPI<DestinationSequenceListResponse>(
                request,
                R4MEInfrastructureSettingsV5.RouteDestinationsSequence,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Computes the optimised visit sequence for a list of coordinates asynchronously.
        /// Uses POST /route-destinations/sequence.
        /// </summary>
        /// <param name="request">
        /// A <see cref="DestinationSequenceRequest"/> containing the lat/lng coordinates to optimise.
        /// </param>
        /// <returns>
        /// A <see cref="Tuple{T1,T2}"/> containing the sequence response and failure response (if any).
        /// </returns>
        public async Task<Tuple<DestinationSequenceListResponse, ResultResponse>> GetDestinationSequenceAsync(
            DestinationSequenceRequest request)
        {
            if (request == null)
            {
                return new Tuple<DestinationSequenceListResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The request parameter is null" } }
                    }
                });
            }

            if (request.Coordinates == null || request.Coordinates.Length == 0)
            {
                return new Tuple<DestinationSequenceListResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The Coordinates array is null or empty" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<DestinationSequenceListResponse>(
                request,
                R4MEInfrastructureSettingsV5.RouteDestinationsSequence,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
