using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientFactoryTest.V4.Fixtures
{
    public class VehicleFixture : IDisposable
    {
        static readonly string c_ApiKey = ApiKeys.ActualApiKey; // The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types --- put in the parameter an appropriate API key
        static readonly string c_ApiKey_1 = ApiKeys.DemoApiKey;

        //public TestDataRepository tdr = new TestDataRepository();
        //public TestDataRepository tdr2 = new TestDataRepository();

        public List<string> removeVehicleIDs;

        public bool hasCommercialCapabalities;

        public VehicleFixture()
        {
            removeVehicleIDs = new List<string>();

            var vehicles = this.GetVehiclesList();

            if ((vehicles?.Length ?? 0) < 1)
            {
                var newVehicle = new VehicleV4Parameters()
                {
                    VehicleName = "Ford Transit Test 6",
                    VehicleAlias = "Ford Transit Test 6"
                };

                var vehicle = this.CreateVehicle(newVehicle);

                removeVehicleIDs.Add(vehicle.VehicleGuid);
            }
            else
            {
                foreach (var veh1 in vehicles)
                {
                    removeVehicleIDs.Add(veh1.VehicleId);
                }
            }
        }

        public VehicleV4CreateResponse CreateVehicle(VehicleV4Parameters vehicleParams)
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var result = route4Me.CreateVehicle(vehicleParams, out string errorString);

            Assert.True(result!=null, "CreatetVehiclTest failed. " + errorString);

            return result;
        }

        public void Dispose()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            foreach (var vehicleId in removeVehicleIDs)
            {
                var vehicleParams = new VehicleV4Parameters()
                {
                    VehicleId = vehicleId
                };

                // Run the query
                var vehicles = route4Me.DeleteVehicle(vehicleParams, out string errorString);
            }
        }

        public Route4MeSDK.DataTypes.V5.Vehicle[] GetVehiclesList()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var vehicleParameters = new VehicleParameters
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            var vehicles = route4Me.GetVehicles(vehicleParameters, out string errorString);

            return vehicles;
        }
    }
}
