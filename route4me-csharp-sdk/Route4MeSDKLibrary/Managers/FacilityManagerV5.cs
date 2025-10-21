using System;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Facility Management API V5
    /// </summary>
    public class FacilityManagerV5 : Route4MeManagerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">Route4Me API key</param>
        public FacilityManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Get a single facility by ID
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Facility resource</returns>
        public FacilityResource GetFacility(string facilityId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<FacilityResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Get,
                out resultResponse
            );

            return result;
        }

        /// <summary>
        /// Get a paginated list of facilities
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Paginated facilities</returns>
        public FacilitiesPaginateResource GetFacilities(
            FacilityGetParameters parameters, 
            out ResultResponse resultResponse)
        {
            if (parameters == null)
            {
                parameters = new FacilityGetParameters();
            }

            var result = GetJsonObjectFromAPI<FacilitiesPaginateResource>(
                parameters,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Get,
                out resultResponse
            );

            return result;
        }

        /// <summary>
        /// Create a new facility
        /// </summary>
        /// <param name="facility">Facility creation request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created facility</returns>
        public FacilityResource CreateFacility(
            FacilityCreateRequest facility, 
            out ResultResponse resultResponse)
        {
            if (facility == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility data is required" } }
                    }
                };
                return null;
            }

            // Debug: Log the API call details
            System.Console.WriteLine($"[DEBUG] Creating facility at URL: {R4MEInfrastructureSettingsV5.Facilities}");
            System.Console.WriteLine($"[DEBUG] Request data: FacilityAlias={facility.FacilityAlias}, Address={facility.Address}, Status={facility.Status}");

            var result = GetJsonObjectFromAPI<FacilityResource>(
                facility,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Post,
                out resultResponse
            );

            // Debug: Log the response
            System.Console.WriteLine($"[DEBUG] API Response: Result={(result != null ? "SUCCESS" : "NULL")}");
            if (resultResponse != null)
            {
                System.Console.WriteLine($"[DEBUG] Error Response: Status={resultResponse.Status}, Code={resultResponse.Code}");
                if (resultResponse.Messages != null)
                {
                    foreach (var msg in resultResponse.Messages)
                    {
                        System.Console.WriteLine($"[DEBUG] Error Message: {msg.Key} = {string.Join(", ", msg.Value)}");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Update an existing facility
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <param name="facility">Facility update request</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated facility</returns>
        public FacilityResource UpdateFacility(
            string facilityId,
            FacilityUpdateRequest facility, 
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return null;
            }

            if (facility == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility update data is required" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<FacilityResource>(
                facility,
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Put,
                out resultResponse
            );

            return result;
        }

        /// <summary>
        /// Delete a facility by ID
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Remaining facilities collection</returns>
        public FacilityResource[] DeleteFacility(
            string facilityId, 
            out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<FacilityResource[]>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Delete,
                out resultResponse
            );

            return result;
        }

        /// <summary>
        /// Get all facility types
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Collection of facility types</returns>
        public FacilityTypeCollectionResource GetFacilityTypes(out ResultResponse resultResponse)
        {
            // Debug: Log the API call details
            System.Console.WriteLine($"[DEBUG] Getting facility types at URL: {R4MEInfrastructureSettingsV5.FacilityTypes}");

            var result = GetJsonObjectFromAPI<FacilityTypeCollectionResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.FacilityTypes,
                HttpMethodType.Get,
                out resultResponse
            );

            // Debug: Log the response
            System.Console.WriteLine($"[DEBUG] Facility Types Response: Result={(result != null ? "SUCCESS" : "NULL")}");
            if (result != null)
            {
                System.Console.WriteLine($"[DEBUG] Facility Types Count: {result.Data?.Length ?? 0}");
            }
            if (resultResponse != null)
            {
                System.Console.WriteLine($"[DEBUG] Error Response: Status={resultResponse.Status}, Code={resultResponse.Code}");
                if (resultResponse.Messages != null)
                {
                    foreach (var msg in resultResponse.Messages)
                    {
                        System.Console.WriteLine($"[DEBUG] Error Message: {msg.Key} = {string.Join(", ", msg.Value)}");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a single facility type by ID
        /// </summary>
        /// <param name="typeId">Facility type ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Facility type resource</returns>
        public FacilityTypeResource GetFacilityType(int typeId, out ResultResponse resultResponse)
        {
            if (typeId <= 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Valid facility type ID is required" } }
                    }
                };
                return null;
            }

            var result = GetJsonObjectFromAPI<FacilityTypeResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.FacilityTypes}/{typeId}",
                HttpMethodType.Get,
                out resultResponse
            );

            return result;
        }

        #region Async Methods

        /// <summary>
        /// Get a single facility by ID asynchronously
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <returns>A Tuple containing the facility resource and error response</returns>
        public async Task<Tuple<FacilityResource, ResultResponse>> GetFacilityAsync(string facilityId)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return new Tuple<FacilityResource, ResultResponse>(null, error);
            }

            var result = await GetJsonObjectFromAPIAsync<FacilityResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Get
            ).ConfigureAwait(false);

            return new Tuple<FacilityResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get a paginated list of facilities asynchronously
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>A Tuple containing paginated facilities and error response</returns>
        public async Task<Tuple<FacilitiesPaginateResource, ResultResponse>> GetFacilitiesAsync(
            FacilityGetParameters parameters = null)
        {
            if (parameters == null)
            {
                parameters = new FacilityGetParameters();
            }

            var result = await GetJsonObjectFromAPIAsync<FacilitiesPaginateResource>(
                parameters,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Get
            ).ConfigureAwait(false);

            return new Tuple<FacilitiesPaginateResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Create a new facility asynchronously
        /// </summary>
        /// <param name="facility">Facility creation request</param>
        /// <returns>A Tuple containing the created facility and error response</returns>
        public async Task<Tuple<FacilityResource, ResultResponse>> CreateFacilityAsync(
            FacilityCreateRequest facility)
        {
            if (facility == null)
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility data is required" } }
                    }
                };
                return new Tuple<FacilityResource, ResultResponse>(null, error);
            }

            var result = await GetJsonObjectFromAPIAsync<FacilityResource>(
                facility,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Post
            ).ConfigureAwait(false);

            return new Tuple<FacilityResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Update an existing facility asynchronously
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <param name="facility">Facility update request</param>
        /// <returns>A Tuple containing the updated facility and error response</returns>
        public async Task<Tuple<FacilityResource, ResultResponse>> UpdateFacilityAsync(
            string facilityId,
            FacilityUpdateRequest facility)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return new Tuple<FacilityResource, ResultResponse>(null, error);
            }

            if (facility == null)
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility update data is required" } }
                    }
                };
                return new Tuple<FacilityResource, ResultResponse>(null, error);
            }

            var result = await GetJsonObjectFromAPIAsync<FacilityResource>(
                facility,
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Put
            ).ConfigureAwait(false);

            return new Tuple<FacilityResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Delete a facility by ID asynchronously
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <returns>A Tuple containing the remaining facilities collection and error response</returns>
        public async Task<Tuple<FacilityResource[], ResultResponse>> DeleteFacilityAsync(
            string facilityId)
        {
            if (string.IsNullOrWhiteSpace(facilityId))
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Facility ID is required" } }
                    }
                };
                return new Tuple<FacilityResource[], ResultResponse>(null, error);
            }

            var result = await GetJsonObjectFromAPIAsync<FacilityResource[]>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Delete
            ).ConfigureAwait(false);

            return new Tuple<FacilityResource[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get all facility types asynchronously
        /// </summary>
        /// <returns>A Tuple containing collection of facility types and error response</returns>
        public async Task<Tuple<FacilityTypeCollectionResource, ResultResponse>> GetFacilityTypesAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<FacilityTypeCollectionResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.FacilityTypes,
                HttpMethodType.Get
            ).ConfigureAwait(false);

            return new Tuple<FacilityTypeCollectionResource, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get a single facility type by ID asynchronously
        /// </summary>
        /// <param name="typeId">Facility type ID</param>
        /// <returns>A Tuple containing the facility type resource and error response</returns>
        public async Task<Tuple<FacilityTypeResource, ResultResponse>> GetFacilityTypeAsync(int typeId)
        {
            if (typeId <= 0)
            {
                var error = new ResultResponse
                {
                    Status = false,
                    Messages = new System.Collections.Generic.Dictionary<string, string[]>
                    {
                        { "error", new[] { "Valid facility type ID is required" } }
                    }
                };
                return new Tuple<FacilityTypeResource, ResultResponse>(null, error);
            }

            var result = await GetJsonObjectFromAPIAsync<FacilityTypeResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.FacilityTypes}/{typeId}",
                HttpMethodType.Get
            ).ConfigureAwait(false);

            return new Tuple<FacilityTypeResource, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion
    }
}

