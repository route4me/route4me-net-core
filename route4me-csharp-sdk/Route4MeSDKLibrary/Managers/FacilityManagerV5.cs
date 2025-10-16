using System;
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

            var result = GetJsonObjectFromAPI<FacilityResource>(
                facility,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Post,
                out resultResponse
            );

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
            var result = GetJsonObjectFromAPI<FacilityTypeCollectionResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.FacilityTypes,
                HttpMethodType.Get,
                out resultResponse
            );

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
    }
}

