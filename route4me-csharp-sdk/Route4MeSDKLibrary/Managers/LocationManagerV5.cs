using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Locations;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Manager for Location-related endpoints (API v5.0)
    /// Handles locations list, clustering, heatmap, territories, export, and location types CRUD operations
    /// </summary>
    public sealed class LocationManagerV5 : Route4MeManagerBase
    {
        public LocationManagerV5(string apiKey) : base(apiKey)
        {
        }

        #region Location Endpoints

        /// <summary>
        /// Get combined list with locations data
        /// </summary>
        /// <param name="request">Request parameters including filters, pagination, sorting</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Combined location resource with data and metadata</returns>
        public LocationCombinedResource GetLocationsCombined(LocationCombinedRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationCombinedResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsListCombined,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get combined list with locations data (async)
        /// </summary>
        /// <param name="request">Request parameters including filters, pagination, sorting</param>
        /// <returns>Tuple with combined location resource and result response</returns>
        public async Task<Tuple<LocationCombinedResource, ResultResponse>> GetLocationsCombinedAsync(LocationCombinedRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationCombinedResource, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationCombinedResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsListCombined,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get list with locations data
        /// </summary>
        /// <param name="request">Request parameters including filters, pagination, sorting</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location list resource with items and total count</returns>
        public LocationListResource GetLocationsList(LocationCombinedRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationListResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsList,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get list with locations data (async)
        /// </summary>
        /// <param name="request">Request parameters including filters, pagination, sorting</param>
        /// <returns>Tuple with location list resource and result response</returns>
        public async Task<Tuple<LocationListResource, ResultResponse>> GetLocationsListAsync(LocationCombinedRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationListResource, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationListResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsList,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get clustered locations
        /// </summary>
        /// <param name="request">Clustering request with zoom level and filters</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location clustering resource with clusters and locations</returns>
        public LocationClusteringResource GetLocationsClustering(LocationClusteringRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationClusteringResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsClustering,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get clustered locations (async)
        /// </summary>
        /// <param name="request">Clustering request with zoom level and filters</param>
        /// <returns>Tuple with location clustering resource and result response</returns>
        public async Task<Tuple<LocationClusteringResource, ResultResponse>> GetLocationsClusteringAsync(LocationClusteringRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationClusteringResource, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationClusteringResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationsClustering,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get a heat map of the locations
        /// </summary>
        /// <param name="request">Heatmap request with zoom level and filters</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location heatmap collection with data points [lat, lng, intensity]</returns>
        public LocationHeatmapCollection GetLocationsHeatmap(LocationHeatmapRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationHeatmapCollection>(
                request,
                R4MEInfrastructureSettingsV5.LocationsHeatmap,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get a heat map of the locations (async)
        /// </summary>
        /// <param name="request">Heatmap request with zoom level and filters</param>
        /// <returns>Tuple with location heatmap collection and result response</returns>
        public async Task<Tuple<LocationHeatmapCollection, ResultResponse>> GetLocationsHeatmapAsync(LocationHeatmapRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationHeatmapCollection, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationHeatmapCollection>(
                request,
                R4MEInfrastructureSettingsV5.LocationsHeatmap,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get territories with the number of locations
        /// </summary>
        /// <param name="request">Territories request with pagination and filters</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location territories collection with items and pagination info</returns>
        public LocationTerritoriesCollection GetLocationsTerritories(LocationTerritoriesRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationTerritoriesCollection>(
                request,
                R4MEInfrastructureSettingsV5.LocationsTerritories,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get territories with the number of locations (async)
        /// </summary>
        /// <param name="request">Territories request with pagination and filters</param>
        /// <returns>Tuple with location territories collection and result response</returns>
        public async Task<Tuple<LocationTerritoriesCollection, ResultResponse>> GetLocationsTerritoriesAsync(LocationTerritoriesRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationTerritoriesCollection, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationTerritoriesCollection>(
                request,
                R4MEInfrastructureSettingsV5.LocationsTerritories,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get columns info for locations export
        /// </summary>
        /// <param name="request">Export columns request with filters</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Array of export column resources with field info</returns>
        public LocationExportColumnsResource[] GetLocationsExportColumns(LocationExportColumnsRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationExportColumnsResource[]>(
                request,
                R4MEInfrastructureSettingsV5.LocationsExportColumns,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get columns info for locations export (async)
        /// </summary>
        /// <param name="request">Export columns request with filters</param>
        /// <returns>Tuple with array of export column resources and result response</returns>
        public async Task<Tuple<LocationExportColumnsResource[], ResultResponse>> GetLocationsExportColumnsAsync(LocationExportColumnsRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationExportColumnsResource[], ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationExportColumnsResource[]>(
                request,
                R4MEInfrastructureSettingsV5.LocationsExportColumns,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Export locations
        /// Returns a job ID in the response headers (Location header) for tracking export status
        /// </summary>
        /// <param name="request">Export request with format, columns, and filters</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Status response; check response headers for job tracking URL</returns>
        public StatusResponse ExportLocations(LocationExportRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<StatusResponse>(
                request,
                R4MEInfrastructureSettingsV5.LocationsExport,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Export locations (async)
        /// Returns a job ID in the tuple for tracking export status
        /// </summary>
        /// <param name="request">Export request with format, columns, and filters</param>
        /// <returns>Tuple with status response, result response, and job ID for tracking</returns>
        public async Task<Tuple<StatusResponse, ResultResponse, string>> ExportLocationsAsync(LocationExportRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<StatusResponse, ResultResponse, string>(null, errorResponse, null);
            }

            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(
                request,
                R4MEInfrastructureSettingsV5.LocationsExport,
                HttpMethodType.Post,
                null,
                false,
                false).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Location Types CRUD

        /// <summary>
        /// Get location types
        /// </summary>
        /// <param name="request">Request with filters and pagination</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location type collection with data and pagination metadata</returns>
        public LocationTypeCollection GetLocationTypes(GetLocationTypesRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                request = new GetLocationTypesRequest();
            }

            var result = GetJsonObjectFromAPI<LocationTypeCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.LocationTypes,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get location types (async)
        /// </summary>
        /// <param name="request">Request with filters and pagination</param>
        /// <returns>Tuple with location type collection and result response</returns>
        public async Task<Tuple<LocationTypeCollection, ResultResponse>> GetLocationTypesAsync(GetLocationTypesRequest request = null)
        {
            if (request == null)
            {
                request = new GetLocationTypesRequest();
            }

            var result = await GetJsonObjectFromAPIAsync<LocationTypeCollection>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.LocationTypes,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Get location type by ID
        /// </summary>
        /// <param name="locationTypeId">Location type ID</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Location type resource</returns>
        public LocationTypeResource GetLocationTypeById(string locationTypeId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = GetJsonObjectFromAPI<LocationTypeResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get location type by ID (async)
        /// </summary>
        /// <param name="locationTypeId">Location type ID</param>
        /// <returns>Tuple with location type resource and result response</returns>
        public async Task<Tuple<LocationTypeResource, ResultResponse>> GetLocationTypeByIdAsync(string locationTypeId)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = await GetJsonObjectFromAPIAsync<LocationTypeResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Create location type
        /// </summary>
        /// <param name="request">Store location type request with name, description, and type flags</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Created location type resource</returns>
        public LocationTypeResource CreateLocationType(StoreLocationTypeRequest request, out ResultResponse resultResponse)
        {
            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type name is required" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<LocationTypeResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationTypes,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Create location type (async)
        /// </summary>
        /// <param name="request">Store location type request with name, description, and type flags</param>
        /// <returns>Tuple with created location type resource and result response</returns>
        public async Task<Tuple<LocationTypeResource, ResultResponse>> CreateLocationTypeAsync(StoreLocationTypeRequest request)
        {
            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type name is required" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            var result = await GetJsonObjectFromAPIAsync<LocationTypeResource>(
                request,
                R4MEInfrastructureSettingsV5.LocationTypes,
                HttpMethodType.Post).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Update location type
        /// </summary>
        /// <param name="locationTypeId">Location type ID to update</param>
        /// <param name="request">Store location type request with updated data</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Updated location type resource</returns>
        public LocationTypeResource UpdateLocationType(string locationTypeId, StoreLocationTypeRequest request, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return null;
            }

            if (request == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type name is required" } }
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = GetJsonObjectFromAPI<LocationTypeResource>(
                request,
                url,
                HttpMethodType.Put,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Update location type (async)
        /// </summary>
        /// <param name="locationTypeId">Location type ID to update</param>
        /// <param name="request">Store location type request with updated data</param>
        /// <returns>Tuple with updated location type resource and result response</returns>
        public async Task<Tuple<LocationTypeResource, ResultResponse>> UpdateLocationTypeAsync(string locationTypeId, StoreLocationTypeRequest request)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            if (request == null)
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Request cannot be null" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type name is required" } }
                    }
                };
                return new Tuple<LocationTypeResource, ResultResponse>(null, errorResponse);
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = await GetJsonObjectFromAPIAsync<LocationTypeResource>(
                request,
                url,
                HttpMethodType.Put).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Delete location type
        /// </summary>
        /// <param name="locationTypeId">Location type ID to delete</param>
        /// <param name="resultResponse">Result response with error information if request fails</param>
        /// <returns>Delete location type response with status</returns>
        public DeleteLocationTypeResponse DeleteLocationType(string locationTypeId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return null;
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = GetJsonObjectFromAPI<DeleteLocationTypeResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Delete location type (async)
        /// </summary>
        /// <param name="locationTypeId">Location type ID to delete</param>
        /// <returns>Tuple with delete location type response and result response</returns>
        public async Task<Tuple<DeleteLocationTypeResponse, ResultResponse>> DeleteLocationTypeAsync(string locationTypeId)
        {
            if (string.IsNullOrWhiteSpace(locationTypeId))
            {
                var errorResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "Location type ID is required" } }
                    }
                };
                return new Tuple<DeleteLocationTypeResponse, ResultResponse>(null, errorResponse);
            }

            var url = R4MEInfrastructureSettingsV5.LocationTypeById.Replace("{location_type_id}", locationTypeId);

            var result = await GetJsonObjectFromAPIAsync<DeleteLocationTypeResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}