using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of searching for the nearest vahicles to a specified location.
        /// </summary>
        public void SearchNearestVehicles()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare the search parameters
            var searchParams = new VehicleSearchParameters()
            {
                VehicleIDs = new string[] { "0DA1261169ED664C040D9603E97D20C8" },
                Latitude = 40.777213,
                Longitude = -73.9669
            };

            // Run the query
            var vehicle = route4Me.SearchVehicles(searchParams, out ResultResponse resultResponse);

            PrintTestVehiclesV5(vehicle, resultResponse);
        }
    }
}