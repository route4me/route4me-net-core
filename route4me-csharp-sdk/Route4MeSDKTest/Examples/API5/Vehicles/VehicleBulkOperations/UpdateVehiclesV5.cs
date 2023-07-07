using Route4MeSDK.DataTypes.V5;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating the vehicles using the API 5 endpoint.
        /// </summary>
        public async void UpdateVehiclesV5()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            #region Prepare query parameters

            Random rnd = new Random();
            var costNew1 = (double)rnd.Next(10000, 20000);
            var costNew2 = (double)rnd.Next(10000, 20000);

            var vehicleArray = new Route4MeSDK.DataTypes.V5.Vehicles()
            {
                VehicleArray = new Vehicle[]
                {
                    new Vehicle()
                    {
                        VehicleId = "2001114BAE4E861642455771FEF9E0F1",
                        VehicleCostNew = costNew1
                    },
                    new Vehicle()
                    {
                        VehicleId = "2565EC5FD5380A57F7EBB5D34512888F",
                        VehicleCostNew = costNew2
                    }
                }
            };

            #endregion

            // Run the query
            var result = await route4Me.UpdateVehiclesAsync(vehicleArray);

            #region Get job result

            string jobId = result.Item3;

            var jobResult = route4Me.GetVehicleJobResult(jobId, out ResultResponse resultResponse);

            #endregion

            Console.WriteLine($"Job result: {(jobResult?.Status ?? false)}");

            PrintTestVehiclesV5(result, result.Item2);
        }
    }
}