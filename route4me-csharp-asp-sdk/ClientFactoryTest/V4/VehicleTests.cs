using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK.DataTypes;
using Route4MeSDK.Controllers;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ClientFactoryTest.V4
{
    public class VehicleTests : IClassFixture<VehicleFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;
        Route4MeApi4Service r4mApi4Service;

        private readonly ITestOutputHelper _output;

        private readonly VehicleFixture _fixture;

        public VehicleTests(VehicleFixture fixture, IServer server, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;

            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);
        }

        [FactSkipable]
        public async Task GetVehiclesListTest()
        {
            var vehicleParameters = new VehicleParameters()
            {
                WithPagination = true,
                Page = 1,
                PerPage = 10
            };

            // Run the query
            var vehiclesResult = await r4mController.GetVehicles(vehicleParameters);

            Assert.True((vehiclesResult?.Item1 ?? null) != null, 
                $"GetVehiclesListTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(vehiclesResult.Item2) );
            Assert.IsType<Route4MeSDK.DataTypes.V5.Vehicle[]>(vehiclesResult.Item1);
        }

        [FactSkipable]
        public async Task CreateVehicleTest()
        {
            var newVehicle = new VehicleV4Parameters()
            {
                VehicleName = "Ford Transit Test 7",
                VehicleAlias = "Ford Transit Test 7"
            };

            var createVehicleResult = await r4mController.CreateVehicle(newVehicle);

            Assert.True((createVehicleResult?.Item1 ?? null) != null,
                $"CreateVehicleTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(createVehicleResult.Item2));

            Assert.IsType<VehicleV4CreateResponse>(createVehicleResult.Item1);

            Assert.True(createVehicleResult.Item1.status, "Cannot create a vehicle");

            _fixture.removeVehicleIDs.Add(createVehicleResult.Item1.VehicleGuid);
        }

        [FactSkipable]
        public async Task GetVehicleTest()
        {
            var vehicleParameters = new VehicleParameters
            {
                VehicleId = _fixture.removeVehicleIDs[_fixture.removeVehicleIDs.Count - 1]
            };

            // Run the query
            var vehicleResult = await r4mController.GetVehicle(vehicleParameters);

            Assert.True((vehicleResult?.Item1 ?? null) != null,
                $"GetVehicleTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(vehicleResult.Item2));

            Assert.IsType<Route4MeSDK.DataTypes.V5.Vehicle>(vehicleResult.Item1);
        }

        [FactSkipable]
        public async Task UpdateVehicleTest()
        {
            var vehicleParams = new VehicleV4Parameters()
            {
                VehicleModelYear = 1995,
                VehicleRegCountryId = 223,
                VehicleMake = "Ford",
                FuelType = "unleaded 93",
                HeightInches = 72
            };

            vehicleParams.VehicleId = _fixture.removeVehicleIDs[0];

            // Run the query
            var vehicleUpdateResult = await r4mController.UpdateVehicle(vehicleParams);

            Assert.True((vehicleUpdateResult?.Item1 ?? null) != null,
                $"UpdateVehicleTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(vehicleUpdateResult.Item2));

            Assert.IsType<VehicleV4Response>(vehicleUpdateResult.Item1);

            Assert.Equal("1995", vehicleUpdateResult.Item1.VehicleModelYear);
            Assert.Equal("223", vehicleUpdateResult.Item1.VehicleRegCountryId);
            Assert.Equal("Ford", vehicleUpdateResult.Item1.VehicleMake);
            Assert.Equal("unleaded 93", vehicleUpdateResult.Item1.FuelType);
        }

        [FactSkipable]
        public async Task DeleteVehicleTest()
        {
            var newVehicle = new VehicleV4Parameters()
            {
                VehicleName = "Ford Transit Test 8",
                VehicleAlias = "Ford Transit Test 8"
            };

            var vehicle = _fixture.CreateVehicle(newVehicle);

            var vehicleParams = new VehicleV4Parameters()
            {
                VehicleId = vehicle.VehicleGuid
            };

            // Run the query
            var deleteVehicleResult = await r4mController.DeleteVehicle(vehicleParams);

            Assert.True((deleteVehicleResult?.Item1 ?? null) != null,
                $"DeleteVehicleTest failed.{Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(deleteVehicleResult.Item2));

            Assert.IsType<VehicleV4Response>(deleteVehicleResult.Item1);

        }
    }


}
