using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a list of the vehicles by their state using the API 5 endpoint.
        /// </summary>
        public void GetVehiclesByStateV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicles = route4Me.GetVehiclesByState(VehicleStates.ACTIVE, out ResultResponse resultResponse);


            PrintTestVehcilesV5(vehicles, resultResponse);

            RemoveTestVehiclesV5();


        }
    }
}
