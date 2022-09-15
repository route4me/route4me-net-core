using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // The example refers to the process of updating a route (API 5).
        public void UpdateRouteV5()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare the test data
            RunOptimizationSingleDriverRoute10StopsV5();

            // Prepare the route query parameters
            var routeParams = new RouteParametersQuery()
            {
                RouteId = SD10Stops_route_V5.RouteID,
                Parameters = new RouteParameters()
                {
                    RouteName = SD10Stops_route_V5.Parameters.RouteName + " Updated"
                },
                Addresses = new Address[]
                {
                    new Address()
                    {
                        RouteDestinationId = SD10Stops_route_V5.Addresses[2].RouteDestinationId,
                        Alias = "Address 2",
                        AddressStopType = AddressStopType.Delivery.Description(),
                        SequenceNo = 4
                    },
                    new Address()
                    {
                        RouteDestinationId = SD10Stops_route_V5.Addresses[3].RouteDestinationId,
                        Alias = "Address 3",
                        AddressStopType = AddressStopType.PickUp.Description(),
                        SequenceNo = 3
                    }
                }
            };

            // Run the query
            var updatedRoute = route4Me.UpdateRoute(routeParams, out ResultResponse resultResponse);

            // Print the result on the consule
            PrintExampleRouteResult(updatedRoute, null, resultResponse);
        }
    }
}