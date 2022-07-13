using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of retrieving a list of driver reviews asynchronously.
        /// </summary>
        public async void GetDriverReviewListAsync()
        {
            // The example requires an API key with special features.
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var queryParameters = new DriverReviewParameters()
            {
                Start = "2020-01-01",
                End = "2030-01-01",
                Page = 1,
                PerPage = 20
            };

            var reviewList = await route4Me.GetDriverReviewListAsync(queryParameters);

            PrintDriverReview(reviewList.Item1, reviewList.Item2);
        }
    }
}
