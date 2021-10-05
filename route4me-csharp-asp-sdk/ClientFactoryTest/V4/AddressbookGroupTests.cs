using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK.Controllers;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ClientFactoryTest.V4
{
    public class AddressbookGroupTests : IClassFixture<AddressBookGroupFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;
        private Route4MeApi4Service r4mApi4Service;

        private readonly ITestOutputHelper _output;

        private readonly AddressBookGroupFixture _fixture;

        public AddressbookGroupTests(AddressBookGroupFixture fixture, IServer server, ITestOutputHelper output)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _output = output;

            _fixture = fixture;
            _output = output;
        }


    }
}
