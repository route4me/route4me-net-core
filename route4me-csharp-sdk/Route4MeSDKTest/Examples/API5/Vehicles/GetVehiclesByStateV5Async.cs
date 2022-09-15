using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a list of the vehicles by their state 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetVehiclesByStateV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var result = await route4Me.GetVehiclesByStateAsync(VehicleStates.ACTIVE);

            PrintTestVehcilesV5(result.Item1, result.Item2);

            RemoveTestVehiclesV5();
        }
    }
}