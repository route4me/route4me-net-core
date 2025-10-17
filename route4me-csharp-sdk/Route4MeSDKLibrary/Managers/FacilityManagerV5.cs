using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDKLibrary.Managers
{
    // NOTE:
    // Input data validation wasn't implemented in scope of MVP's estimate
    // The better way to do it is:
    // 1. Define necessary interfaces for each field we have to validate
    // 2. Inherit these interfaces for input types
    // 3. Define validators for each interface (using FluentValidation)

    /// <summary>
    /// Represents a possibility to manage facilities and facility types
    /// </summary>
    public class FacilityManagerV5 : Route4MeManagerBase
    {
        public FacilityManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Gets the paginated list of facilities
        /// </summary>
        /// <param name="facilityParams">Query params</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the facilities</returns>
        public FacilitiesPaginateResource GetFacilities(FacilityGetParameters facilityParams, out ResultResponse resultResponse)
        {
            // NOTE: Input data validation wasn't implemented in scope of MVP

            return GetJsonObjectFromAPI<FacilitiesPaginateResource>(
                facilityParams,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets the facility by specifying facility id
        /// </summary>
        /// <param name="facilityId">Facility id</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The facility</returns
        public FacilityResource GetFacility(string facilityId, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<FacilityResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Creates a new facility
        /// </summary>
        /// <param name="request">The request data to create a new facility</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The created facility</returns>
        public FacilityResource CreateFacility(FacilityCreateRequest request, out ResultResponse resultResponse)
        {
            // NOTE: Input data validation wasn't implemented in scope of MVP

            return GetJsonObjectFromAPI<FacilityResource>(
                request,
                R4MEInfrastructureSettingsV5.Facilities,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Updates an existing facility
        /// </summary>
        /// <param name="facilityId">Facility id</param>
        /// <param name="request">The request data to update an existing facility</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The updated facility</returns>
        public FacilityResource UpdateFacility(string facilityId, FacilityUpdateRequest request, out ResultResponse resultResponse)
        {
            // NOTE: Input data validation wasn't implemented in scope of MVP

            return GetJsonObjectFromAPI<FacilityResource>(
                request,
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Put,
                out resultResponse);
        }

        /// <summary>
        /// Removes an existing facility
        /// </summary>
        /// <param name="facilityId">Facility id</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The removed facility</returns>
        public FacilityResource DeleteFacility(string facilityId, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<FacilityResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.Facilities}/{facilityId}",
                HttpMethodType.Delete,
                out resultResponse);
        }

        /// <summary>
        /// Gets facility types list
        /// </summary>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the facility types</returns>
        public FacilityTypeCollectionResource GetFacilityTypes(out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<FacilityTypeCollectionResource>(
                new GenericParameters(),
                R4MEInfrastructureSettingsV5.FacilityTypes,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Gets the facility type by specifying facility type id
        /// </summary>
        /// <param name="typeId">Facility type id</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The facility type</returns>
        public FacilityTypeResource GetFacilityType(int typeId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<FacilityTypeResource>(
                new GenericParameters(),
                $"{R4MEInfrastructureSettingsV5.FacilityTypes}/{typeId}",
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }
    }
}
