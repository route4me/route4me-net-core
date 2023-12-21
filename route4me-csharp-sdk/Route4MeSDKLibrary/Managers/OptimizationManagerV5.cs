using System;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.Internal.Requests;
using Route4MeSDKLibrary.DataTypes.Internal.Response;

namespace Route4MeSDKLibrary.Managers
{
    public class OptimizationManagerV5 : Route4MeManagerBase
    {
        public OptimizationManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <returns>Generated optimization problem object</returns>
        public async Task<Tuple<DataObject, ResultResponse>> RunOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DataObject, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <param name="resultResponse"> Returned error string in case of the processs failing </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out ResultResponse resultResponse)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = GetJsonObjectFromAPI<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete,
                out resultResponse);

            return resultResponse == null;
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        /// <returns> Result status true/false </returns>
        public async Task<Tuple<bool, ResultResponse>> RemoveOptimizationAsync(string[] optimizationProblemIDs)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = await GetJsonObjectFromAPIAsync<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, ResultResponse>(response.Item2 == null, response.Item2);
        }
    }
}
