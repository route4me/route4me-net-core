using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a paginated list of the vehicles 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetPaginatedVehiclesV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleParams = new VehicleParameters()
            {
                Page = 1,
                PerPage = 10,
                FieldToOrderBy = "vehicle_alias",
                OrderDirection = "asc",
                Show = VehicleStates.ALL.Description(),
                SearchQuery = "TopKick C5500 TST"
            };

            var result = await route4Me.GetPaginatedVehiclesListAsync(vehicleParams);


            PrintTestVehcilesV5(result.Item1, result.Item2);
        }
    }
}
