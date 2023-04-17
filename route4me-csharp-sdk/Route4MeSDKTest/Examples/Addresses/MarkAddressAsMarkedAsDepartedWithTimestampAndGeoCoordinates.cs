using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Mark Address as Departed with timestamp and geo coordinates
        /// </summary>
        /// <param name="aParams">Address parameters</param>
        public void MarkAddressAsMarkedAsDepartedWithTimestampAndGeoCoordinates(AddressParameters aParams = null)
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
                    TimestampLastDeparted = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    DepartedLatitude = SD10Stops_route.Addresses[2].DepartedLatitude,
                    DepartedLongitude = SD10Stops_route.Addresses[2].DepartedLongitude,
                };
            }

            // Run the query
            Address resultAddress = route4Me
                .MarkAddressAsMarkedAsDeparted(aParams, out string errorString);

            PrintExampleDestination(resultAddress, errorString);

            if (isInnerExample) RemoveTestOptimizations();
        }
    }
}