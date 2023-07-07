using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a vehicle using the API 5 endpoint.
        /// </summary>
        public void CreateVehicleV5()
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

            var vehicle = route4Me.CreateVehicle(vehicleParams, out ResultResponse resultResponse);

            if (vehicle != null && vehicle.GetType() == typeof(Vehicle))
            {
                vehiclesToRemove = new System.Collections.Generic.List<string>();
                vehiclesToRemove.Add(vehicle.VehicleId);
            }


            PrintTestVehiclesV5(vehicle, resultResponse);

            RemoveTestVehiclesV5();
        }
    }
}