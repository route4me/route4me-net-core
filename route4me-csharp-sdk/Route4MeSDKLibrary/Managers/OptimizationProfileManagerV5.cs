using System;
using System.Threading.Tasks;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles;

namespace Route4MeSDKLibrary.Managers
{
    /// <summary>
    /// Manager for Optimization Profiles API V5 operations.
    /// </summary>
    public class OptimizationProfileManagerV5 : Route4MeManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the OptimizationProfileManagerV5 class.
        /// </summary>
        /// <param name="apiKey">The Route4Me API key for authentication.</param>
        public OptimizationProfileManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Retrieves a list of all optimization profiles for the authenticated account.
        /// </summary>
        /// <param name="resultResponse">Contains error information if the operation fails.</param>
        /// <returns>A response containing an array of optimization profiles, or null if the operation failed.</returns>
        public OptimizationProfilesResponse GetOptimizationProfiles(out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<OptimizationProfilesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OptimizationProfilesList,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Asynchronously retrieves a list of all optimization profiles for the authenticated account.
        /// </summary>
        /// <returns>A tuple containing the optimization profiles response and any error information.</returns>
        public async Task<Tuple<OptimizationProfilesResponse, ResultResponse>> GetOptimizationProfilesAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfilesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OptimizationProfilesList,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<OptimizationProfilesResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Saves entities for an optimization profile.
        /// </summary>
        public OptimizationProfileSaveEntities SaveEntities(OptimizationProfileSaveEntities request, out ResultResponse resultResponse)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Items == null || request.Items.Length == 0)
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return null;
            }
            var response = GetJsonObjectFromAPI<OptimizationProfileSaveEntities>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfilesSaveEntities,
                HttpMethodType.Post, null, false, true,
                out resultResponse, null, true);

            return response;
        }

        /// <summary>
        /// Asynchronously saves entities for an optimization profile.
        /// </summary>
        public async Task<Tuple<OptimizationProfileSaveEntities, ResultResponse>> SaveEntitiesAsync(OptimizationProfileSaveEntities request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfileSaveEntities>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfilesSaveEntities,
                HttpMethodType.Post,
                null,
                true,
                false,
                null,
                true).ConfigureAwait(false);

            return new Tuple<OptimizationProfileSaveEntities, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Deletes entities from an optimization profile.
        /// </summary>
        public OptimizationProfileSaveEntities DeleteEntities(OptimizationProfileDeleteEntitiesRequest request, out ResultResponse resultResponse)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Items == null || request.Items.Length == 0)
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return null;
            }
            var response = GetJsonObjectFromAPI<OptimizationProfileSaveEntities>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfilesDeleteEntities,
                HttpMethodType.Post, null, false, true,
                out resultResponse, null, true);

            return response;
        }

        /// <summary>
        /// Asynchronously deletes entities from an optimization profile.
        /// </summary>
        public async Task<Tuple<OptimizationProfileSaveEntities, ResultResponse>> DeleteEntitiesAsync(OptimizationProfileDeleteEntitiesRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfileSaveEntities>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfilesDeleteEntities,
                HttpMethodType.Post,
                null,
                true,
                false,
                null,
                true).ConfigureAwait(false);

            return new Tuple<OptimizationProfileSaveEntities, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Gets an optimization profile by its ID.
        /// </summary>
        /// <param name="optimizationProfileId">The optimization profile ID.</param>
        /// <param name="resultResponse">Contains error information if the operation fails.</param>
        /// <returns>The optimization profile, or null if not found.</returns>
        public OptimizationProfile GetOptimizationProfileById(string optimizationProfileId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId))
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return null;
            }
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var response = GetJsonObjectFromAPI<OptimizationProfileResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response?.Data;
        }

        /// <summary>
        /// Asynchronously gets an optimization profile by its ID.
        /// </summary>
        /// <param name="optimizationProfileId">The optimization profile ID.</param>
        /// <returns>A tuple containing the optimization profile and any error information.</returns>
        public async Task<Tuple<OptimizationProfile, ResultResponse>> GetOptimizationProfileByIdAsync(string optimizationProfileId)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId)) throw new ArgumentNullException(nameof(optimizationProfileId));
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfileResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<OptimizationProfile, ResultResponse>(result.Item1?.Data, result.Item2);
        }

        /// <summary>
        /// Creates a new optimization profile.
        /// </summary>
        /// <param name="request">The create request containing profile details.</param>
        /// <param name="resultResponse">Contains error information if the operation fails.</param>
        /// <returns>The created optimization profile, or null if creation failed.</returns>
        public OptimizationProfile CreateOptimizationProfile(OptimizationProfileCreateRequest request, out ResultResponse resultResponse)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.ProfileName))
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return null;
            }
            var response = GetJsonObjectFromAPI<OptimizationProfileResponse>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfiles,
                HttpMethodType.Post, null, false, true,
                out resultResponse, null, true);

            return response?.Data;
        }

        /// <summary>
        /// Asynchronously creates a new optimization profile.
        /// </summary>
        /// <param name="request">The create request containing profile details.</param>
        /// <returns>A tuple containing the created optimization profile and any error information.</returns>
        public async Task<Tuple<OptimizationProfile, ResultResponse>> CreateOptimizationProfileAsync(OptimizationProfileCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfileResponse>(request,
                R4MEInfrastructureSettingsV5.OptimizationProfiles,
                HttpMethodType.Post,
                null,
                true,
                false,
                null,
                true).ConfigureAwait(false);

            return new Tuple<OptimizationProfile, ResultResponse>(result.Item1?.Data, result.Item2);
        }

        /// <summary>
        /// Updates an existing optimization profile.
        /// </summary>
        /// <param name="optimizationProfileId">The ID of the optimization profile to update.</param>
        /// <param name="request">The update request containing new profile details.</param>
        /// <param name="resultResponse">Contains error information if the operation fails.</param>
        /// <returns>The updated optimization profile, or null if update failed.</returns>
        public OptimizationProfile UpdateOptimizationProfile(string optimizationProfileId, OptimizationProfileCreateRequest request, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId))
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return null;
            }
            if (request == null) throw new ArgumentNullException(nameof(request));
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var response = GetJsonObjectFromAPI<OptimizationProfileResponse>(request,
                url,
                HttpMethodType.Put, null, false, true,
                out resultResponse, null, true);

            return response?.Data;
        }

        /// <summary>
        /// Asynchronously updates an existing optimization profile.
        /// </summary>
        /// <param name="optimizationProfileId">The ID of the optimization profile to update.</param>
        /// <param name="request">The update request containing new profile details.</param>
        /// <returns>A tuple containing the updated optimization profile and any error information.</returns>
        public async Task<Tuple<OptimizationProfile, ResultResponse>> UpdateOptimizationProfileAsync(string optimizationProfileId, OptimizationProfileCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId)) throw new ArgumentNullException(nameof(optimizationProfileId));
            if (request == null) throw new ArgumentNullException(nameof(request));
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfileResponse>(request,
                url,
                HttpMethodType.Put,
                null,
                true,
                false,
                null,
                true).ConfigureAwait(false);

            return new Tuple<OptimizationProfile, ResultResponse>(result.Item1?.Data, result.Item2);
        }

        /// <summary>
        /// Deletes an optimization profile by its ID.
        /// </summary>
        /// <param name="optimizationProfileId">The ID of the optimization profile to delete.</param>
        /// <param name="resultResponse">Contains error information if the operation fails.</param>
        /// <returns>True if the profile was successfully deleted, false otherwise.</returns>
        public bool DeleteOptimizationProfile(string optimizationProfileId, out ResultResponse resultResponse)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId))
            {
                resultResponse = new ResultResponse { Status = false, Code = 406 };
                return false;
            }
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var response = GetJsonObjectFromAPI<dynamic>(new GenericParameters(),
                url,
                HttpMethodType.Delete, false, true,
                out resultResponse);

            return response?.status == true;
        }

        /// <summary>
        /// Asynchronously deletes an optimization profile by its ID.
        /// </summary>
        /// <param name="optimizationProfileId">Optimization profile ID</param>
        /// <returns>Delete status</returns>
        public async Task<Tuple<bool, ResultResponse>> DeleteOptimizationProfileAsync(string optimizationProfileId)
        {
            if (string.IsNullOrWhiteSpace(optimizationProfileId)) throw new ArgumentNullException(nameof(optimizationProfileId));
            var url = $"{R4MEInfrastructureSettingsV5.OptimizationProfiles}/{optimizationProfileId}";
            var result = await GetJsonObjectFromAPIAsync<dynamic>(new GenericParameters(),
                url,
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(result.Item1?.status == true, result.Item2);
        }
    }
}