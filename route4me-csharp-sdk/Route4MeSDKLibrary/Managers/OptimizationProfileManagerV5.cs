using Route4MeSDK.DataTypes.V5;
using Route4MeSDK;
using System.Threading.Tasks;
using System;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles;

namespace Route4MeSDKLibrary.Managers
{
    public class OptimizationProfileManagerV5 : Route4MeManagerBase
    {
        public OptimizationProfileManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Get Optimization profiles
        /// </summary>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>List of optimization profiles</returns>
        public OptimizationProfilesResponse GetOptimizationProfiles(out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<OptimizationProfilesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OptimizationProfilesList,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get Optimization profiles
        /// </summary>
        /// <returns>List of optimization profiles</returns>
        public async Task<Tuple<OptimizationProfilesResponse, ResultResponse>> GetPodWorkflowsAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<OptimizationProfilesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OptimizationProfilesList,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<OptimizationProfilesResponse, ResultResponse>(result.Item1, result.Item2);
        }
    }
}
