using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of the uploading a driver review asynchronously.
        /// </summary>
        public async void CreateDriverReviewAsync()
        {
            // The example requires an API key with special features.
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            var trackingNumber = SD10Stops_route.Addresses[1].TrackingNumber;

            var newDriverReview = new DriverReview()
            {
                TrackingNumber = trackingNumber, 
                Rating = 4,
                Review = "Test Review"
            };

            var result = await route4Me.CreateDriverReviewAsync(newDriverReview);

            PrintDriverReview(result.Item1.Data, result.Item2);

            RemoveTestOptimizations();
        }
    }
}
