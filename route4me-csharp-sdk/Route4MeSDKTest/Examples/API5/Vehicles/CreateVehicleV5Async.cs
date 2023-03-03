using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle asynchrnously using the API 5 endpoint.
        /// </summary>
        public async void CreateVehicleV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleParams = new Vehicle()
            {
                VehicleAlias = "Peterbilt 579",
                VehicleVin = "1NP5DB9X93N507873",
                VehicleLicensePlate = "PPV7516",
                VehicleModel = "579",
                VehicleModelYear = 2015,
                VehicleYearAcquired = 2018,
                VehicleRegCountryId = 223,
                VehicleMake = "Peterbilt",
                VehicleTypeId = "tractor_trailer",
                FuelType = "diesel"
            };

            var result = await route4Me.CreateVehicleAsync(vehicleParams);

            if (result != null && result.Item1.GetType() == typeof(Vehicle))
            {
                vehiclesToRemove = new System.Collections.Generic.List<string>();
                vehiclesToRemove.Add(result.Item1.VehicleId);
            }


            PrintTestVehiclesV5(result.Item1, result.Item2);

            RemoveTestVehiclesV5();
        }
    }
}