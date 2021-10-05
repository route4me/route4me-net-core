using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4
{
    public class OptimizationTests : IClassFixture<OptimizationFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;

        private readonly OptimizationFixture _fixture;

        private readonly ITestOutputHelper _output;

        public OptimizationTests(OptimizationFixture fixture, IServer server, ITestOutputHelper output)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            Route4MeApi4Service r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _fixture = fixture;

            _output = output;
        }

        [FactSkipable]
        public async Task GetAllOptimizationsTest()
        {
            var optimizationParameters = new OptimizationParameters()
            {
                Limit = 5,
                Offset = 0
            };

            var response = await r4mController
                .GetOptimizations(optimizationParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectOptimizations>(response.Item1);
            Assert.True(response.Item1.TotalRecords> 0);
        }

        [FactSkipable]
        public async Task GetOptimizationsFromDateRangeTest()
        {
            var queryParameters = new OptimizationParameters()
            {
                StartDate = (DateTime.Now - new TimeSpan(7,0,0,0)).ToString("yyyy-MM-dd"),
                EndDate = (DateTime.Now + new TimeSpan(7, 0, 0, 0)).ToString("yyyy-MM-dd")
            };

            var response = await r4mController
                .GetOptimizations(queryParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObjectOptimizations>(response.Item1);
            Assert.True(response.Item1.TotalRecords > 0);
        }

        [FactSkipable]
        public async Task GetOptimizationTest()
        {
            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = _fixture.tdr.SD10Stops_optimization_problem_id
            };

            var response = await r4mController
                .GetOptimization(optimizationParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObject>(response.Item1);
            Assert.True(response.Item1.Addresses.Length > 0);
        }

        [FactSkipable]
        public async Task ReOptimizationTest()
        {
            var optimizationParameters = new OptimizationParameters()
            {
                OptimizationProblemID = _fixture.tdr.SD10Stops_optimization_problem_id,
                ReOptimize = true
            };

            var response = await r4mController
                .UpdateOptimization(optimizationParameters);

            Assert.NotNull(response);
            Assert.IsType<DataObject>(response.Item1);
            Assert.True(response.Item1.Addresses.Length > 0);
        }

        [FactSkipable]
        public async Task UpdateOptimizationDestinationTest()
        {
            var address = _fixture.tdr.SD10Stops_route.Addresses[3];

            address.FirstName = "UpdatedFirstName";
            address.LastName = "UpdatedLastName";

            var response = await r4mController
                .UpdateOptimizationDestination(address);

            Assert.NotNull(response);
            Assert.IsType<Address>(response.Item1);
            Assert.True(response.Item1.FirstName == address.FirstName);
        }

        [FactSkipable]
        public async Task RemoveOptimizationTest()
        {
            string[] OptIDs = new string[] 
            { 
                _fixture.tdr.SDRT_optimization_problem_id
            };

            var response = await r4mController
                .RemoveOptimization(OptIDs);

            Assert.NotNull(response);
            Assert.IsType<RemoveOptimizationResponse>(response.Item1);
            Assert.True(response.Item1.Status);

            _fixture.removeOptimizationsId
                .Remove(_fixture.tdr.SDRT_optimization_problem_id);
        }
    }
}
