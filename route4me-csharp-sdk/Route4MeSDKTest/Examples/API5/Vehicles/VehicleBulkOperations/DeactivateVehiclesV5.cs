using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deactivating the vehicles using the API 5 endpoint.
        /// </summary>
        public void DeactivateVehiclesV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            string[] vehicleIDs = new string[]
            {
                "403C887C3FF2901FEA84CA834B2597B4",
                "3D40AAE287B3DB6422E31BF53C34BAD4"
            };

            var result = route4Me.DeactivateVehicles(vehicleIDs, out ResultResponse resultResponse);

            PrintTestVehcilesV5(result, resultResponse);
        }
    }
}