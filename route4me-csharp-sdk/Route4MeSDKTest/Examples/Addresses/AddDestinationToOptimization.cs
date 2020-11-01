using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public DataObject AddDestinationToOptimization(string optimizationProblemID, bool andReOptimize)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the address that we are going to add to an existing route optimization
            var addresses = new Address[]
            {
                new Address() { AddressString = "717 5th Ave New York, NY 10021",
                                Alias         = "Giorgio Armani",
                                Latitude      = 40.7669692,
                                Longitude     = -73.9693864,
                                Time          = 0
                }
            };

            //Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = optimizationProblemID,
                Addresses = addresses,
                ReOptimize = andReOptimize
            };

            // Execute the optimization to re-optimize and rebalance all the routes in this optimization
            DataObject dataObject = route4Me.UpdateOptimization(optimizationParameters, out string errorString);

            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("AddDestinationToOptimization executed successfully");

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                Console.WriteLine("State: {0}", dataObject.State);
            }
            else
            {
                Console.WriteLine("AddDestinationToOptimization error: {0}", errorString);
            }

            return dataObject;
        }
    }
}
