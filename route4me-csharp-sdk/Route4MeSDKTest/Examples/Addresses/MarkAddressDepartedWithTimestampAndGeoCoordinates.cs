using Route4MeSDK.QueryTypes;
using Route4MeSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Departed with timestamp and geo coordinates
        /// </summary>
        /// <param name="aParams">Address parameters</param>
        public void MarkAddressDepartedWithTimestampAndGeoCoordinates(AddressParameters aParams = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            bool isInnerExample = aParams == null ? true : false;

            if (isInnerExample)
            {
                RunOptimizationSingleDriverRoute10Stops();
                OptimizationsToRemove = new List<string>() { SD10Stops_optimization_problem_id };

                aParams = new AddressParameters
                {
                    RouteId = SD10Stops_route_id,
                    RouteDestinationId = (int)SD10Stops_route.Addresses[2].RouteDestinationId,
                    UtcTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    Latitude = SD10Stops_route.Addresses[2].DepartedLatitude,
                    Longitude = SD10Stops_route.Addresses[2].DepartedLongitude,
                };
            }

            // Run the query
            var resultAddress = route4Me
                .MarkAddressDeparted(aParams, out string errorString);

            PrintExampleDestination(resultAddress, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}
