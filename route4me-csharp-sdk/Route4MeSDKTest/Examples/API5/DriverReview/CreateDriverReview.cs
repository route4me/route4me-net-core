using Route4MeSDK.DataTypes.V5;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of the uploading a driver review.
        /// </summary>
        public void CreateDriverReview()
        {
            // The example requires an API key with special features.
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10Stops();

            OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

            var trackingNumber = SD10Stops_route.Addresses[1].TrackingNumber;

            var newDriverReview = new DriverReview()
            {
                TrackingNumber = trackingNumber,
                Rating = 4,
                Review = "Test Review"
            };

            var driverReview = route4Me.CreateDriverReview(newDriverReview,
                                                          out ResultResponse resultResponse);

            PrintDriverReview(driverReview.Data, resultResponse);

            RemoveTestOptimizations();
        }
    }
}
