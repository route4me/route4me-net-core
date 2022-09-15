using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        // The example refers to the process of updating a route asynchronously (API 5).
        public async void UpdateRouteV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            RunOptimizationSingleDriverRoute10StopsV5();

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

            var result = await route4Me.UpdateRouteAsync(routeParams);

            PrintExampleRouteResult(result.Item1, null, result.Item2);
        }
    }
}