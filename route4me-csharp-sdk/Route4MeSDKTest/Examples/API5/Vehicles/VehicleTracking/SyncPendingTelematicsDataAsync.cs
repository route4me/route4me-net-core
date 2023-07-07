using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a vehicle by ID 
        /// asynchronously using the API 5 endpoint.
        /// </summary>
        public async void SyncPendingTelematicsDataAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var vehicleSyncParams = new VehicleTelematicsSync()
            {
                VehicleAlias = "Commercial Vehicle Medium Duty 4",
                VehicleVin = "WBADE6322VBW51984",
                VehicleRegStateId = 4,
                VehicleRegCountryId = 223,
                VehicleLicensePlate = "CRL8474",
                VehicleTypeId = VehicleTypes.PICKUP_TRUCK.Description(),
                VehicleMake = VehicleMakes.Ford.Description(),
                VehicleModelYear = 2018,
                VehicleYearAcquired = 2020,
                FuelType = FuelTypes.UNLEADED_87.Description(),
                TelematicsGatewayCnnectionId = 1,
                TelematicsGatewayVehicleId = 2,
                ExternalTelematicsVehicleIDs = 2
            };

            var result = await route4Me.SyncPendingTelematicsDataAsync(vehicleSyncParams);

            PrintTestVehiclesV5(result.Item1, result.Item2);
        }
    }
}