using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example shows how to use the API 5 endpoint to delete several vehicles at once.
        /// </summary>
        public void DeleteVehiclesV5()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Prepare the query parameters
            string[] vehicleIDs = new string[]
            {
                "2001114BAE4E861642455771FEF9E0F1"
            };

            // Send a request to the server
            var result = route4Me.DeleteVehicles(vehicleIDs, out ResultResponse resultResponse);

            System.Console.WriteLine("Delete result: " +
                                     (result?.IsSuccessStatusCode ?? false));
        }
    }
}