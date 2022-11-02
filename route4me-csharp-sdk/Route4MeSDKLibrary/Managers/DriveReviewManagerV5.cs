using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDKLibrary.Managers
{
    public class DriveReviewManagerV5 : Route4MeManagerBase
    {
        public DriveReviewManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>List of the driver reviews</returns>
        public DriverReviewsResponse GetDriverReviewList(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            parameters.PrepareForSerialization();

            var result = GetJsonObjectFromAPI<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get list of the drive reviews.
        /// </summary>
        /// <param name="parameters">Query parmeters</param>
        /// <returns>List of the driver reviews</returns>
        public async Task<Tuple<DriverReviewsResponse, ResultResponse>> GetDriverReviewListAsync(DriverReviewParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DriverReviewsResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DriverReviewsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse GetDriverReviewById(DriverReviewParameters parameters,
            out ResultResponse resultResponse)
        {
            if ((parameters?.RatingId) == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The RatingId parameter is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<DriverReviewResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get driver review by ID
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Driver review</returns>
        public async Task<Tuple<DriverReviewResponse, ResultResponse>> GetDriverReviewByIdAsync(DriverReviewParameters parameters)
        {
            if (parameters?.RatingId == null)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The RatingId parameter is not specified" } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<DriverReviewResponse>(parameters,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + parameters.RatingId,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<DriverReviewResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse CreateDriverReview(DriverReview driverReview, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<DriverReviewResponse>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Upload driver review to the server
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <returns>Driver review</returns>
        public Task<Tuple<DriverReviewResponse, ResultResponse>> CreateDriverReviewAsync(DriverReview driverReview)
        {
            return GetJsonObjectFromAPIAsync<DriverReviewResponse>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Driver review</returns>
        public DriverReviewResponse UpdateDriverReview(DriverReview driverReview,
            HttpMethodType method,
            out ResultResponse resultResponse)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                };

                return null;
            }

            if (driverReview.RatingId == null)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameters doesn't contain parameter RatingId."}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<DriverReviewResponse>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                method,
                out resultResponse);
        }

        /// <summary>
        ///     Update a driver review.
        /// </summary>
        /// <param name="driverReview">Request payload</param>
        /// <param name="method">Http method</param>
        /// <returns>Driver review</returns>
        public async Task<Tuple<DriverReviewResponse, ResultResponse>> UpdateDriverReviewAsync(DriverReview driverReview, HttpMethodType method)
        {
            if (method != HttpMethodType.Patch && method != HttpMethodType.Put)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The parameter method has an incorect value."}}
                    }
                });
            }

            if (driverReview.RatingId == null)
            {
                return new Tuple<DriverReviewResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Error", new[] { "The parameters doesn't contain parameter RatingId." } }
                    }
                });
            }

            var result = await GetJsonObjectFromAPIAsync<DriverReviewResponse>(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview + "/" + driverReview.RatingId,
                method, null, false, false).ConfigureAwait(false);

            return new Tuple<DriverReviewResponse, ResultResponse>(result.Item1, result.Item2);
        }
    }
}
