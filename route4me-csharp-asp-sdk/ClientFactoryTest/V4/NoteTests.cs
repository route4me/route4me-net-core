using ClientFactoryTest.IgnoreTests;
using ClientFactoryTest.V4.Fixtures;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Route4MeSDK;
using Route4MeSDK.Controllers;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Route4MeSDK.Services.Route4MeApi4Service;

namespace ClientFactoryTest.V4
{
    public class NoteTests : IClassFixture<NoteFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient _httpClient;

        private Route4MeApi4Controller r4mController;

        private Route4MeApi4Service r4mApi4Service;

        private readonly ITestOutputHelper _output;

        private readonly NoteFixture _fixture;

        public NoteTests(NoteFixture fixture, IServer server, ITestOutputHelper output)
        {
            _httpClientFactory = ((TestServer)server).Services.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            r4mApi4Service = new Route4MeApi4Service(_httpClient);
            r4mController = new Route4MeApi4Controller(r4mApi4Service, _httpClient);

            _output = output;

            _fixture = fixture;
            _output = output;
        }

        [FactSkipable]
        public async Task AddComplexAddressNoteTest()
        {
            string routeIdToMoveTo = _fixture.tdr.SDRT_route_id;
            Assert.True(routeIdToMoveTo!=null, "routeId_SingleDriverRoundTrip is null.");

            int addressId = (_fixture.tdr.DataObjectSDRT != null && 
                            (_fixture.tdr?.SDRT_route?.Addresses?.Length ?? 0) > 1 && 
                            _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId != null) 
                                ? _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId.Value 
                                : 0;

            double lat = _fixture.tdr.SDRT_route.Addresses.Length > 1
                            ? _fixture.tdr.SDRT_route.Addresses[1].Latitude
                            : 33.132675170898;
            double lng = _fixture.tdr.SDRT_route.Addresses.Length > 1
                            ? _fixture.tdr.SDRT_route.Addresses[1].Longitude
                            : -83.244743347168;

            var customNotesResponseResult = await r4mController.GetAllCustomNoteTypes();

            Assert.True(customNotesResponseResult != null,
                $"AddComplexAddressNoteTest failed. Error: {Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(customNotesResponseResult.Item2));

            var customNotesResponse = customNotesResponseResult.Item1;

            Dictionary<string, string> customNotes = null;

            if (customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[]))
            {
                var allCustomNotes = (CustomNoteType[])customNotesResponse;

                if (allCustomNotes.Length > 0)
                {
                    customNotes = new Dictionary<string, string>()
                {
                    {
                        "custom_note_type["+allCustomNotes[0].NoteCustomTypeID+"]",
                        allCustomNotes[0].NoteCustomTypeValues[0] }
                };
                }
            }

            var noteParameters = new NoteParameters()
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                StrNoteContents = "Test Note Contents " + DateTime.Now.ToString()
            };

            if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

            string tempFilePath = null;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ClientFactoryTest.Resources.test.png"))
            {
                var tempFiles = new TempFileCollection();
                {
                    tempFilePath = tempFiles.AddExtension("png");

                    Console.WriteLine(tempFilePath);

                    using (Stream fileStream = File.OpenWrite(tempFilePath))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            noteParameters.StrFileName = tempFilePath;

            var noteResult = await r4mController.AddAddressNote(noteParameters);

            Assert.True(noteResult != null,
                $"AddComplexAddressNoteTest failed. Error: {Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(noteResult.Item2));

            Assert.IsType<AddAddressNoteResponse>(noteResult.Item1);
        }

        [FactSkipable]
        public async Task AddAddressNoteTest()
        {
            string routeIdToMoveTo = _fixture.tdr.SDRT_route_id;
            Assert.True(routeIdToMoveTo!=null, "routeId_SingleDriverRoundTrip is null.");

            int addressId = (_fixture.tdr.DataObjectSDRT != null &&
                                _fixture.tdr.SDRT_route != null &&
                                _fixture.tdr.SDRT_route.Addresses.Length > 1 &&
                                _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId != null)
                             ? _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                             : 0;

            double lat = _fixture.tdr.SDRT_route.Addresses.Length > 1
                        ? _fixture.tdr.SDRT_route.Addresses[1].Latitude
                        : 33.132675170898;
            double lng = _fixture.tdr.SDRT_route.Addresses.Length > 1
                        ? _fixture.tdr.SDRT_route.Addresses[1].Longitude
                        : -83.244743347168;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            // Run the query
            string contents = "Test Note Contents " + DateTime.Now.ToString();
            var addNoteResult = await r4mController.AddAddressNote(noteParameters, contents);

            Assert.True(addNoteResult != null,
                $"AddComplexAddressNoteTest failed. Error: {Environment.NewLine}" +
                r4mApi4Service.ErrorResponseToString(addNoteResult.Item2));

            Assert.IsType<AddAddressNoteResponse>(addNoteResult.Item1);
        }

        [FactSkipable]
        public async Task AddAddressNoteWithFileTest()
        {
            string routeId = _fixture.tdr.SDRT_route_id;

            Assert.True(routeId!=null, "routeId_SingleDriverRoundTrip is null.");

            int addressId = (_fixture.tdr.DataObjectSDRT != null &&
                                _fixture.tdr.SDRT_route != null &&
                                _fixture.tdr.SDRT_route.Addresses.Length > 1 &&
                                _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId != null)
                             ? _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                             : 0;

            double lat = _fixture.tdr.SDRT_route.Addresses.Length > 1
                            ? _fixture.tdr.SDRT_route.Addresses[1].Latitude
                            : 33.132675170898;
            double lng = _fixture.tdr.SDRT_route.Addresses.Length > 1
                            ? _fixture.tdr.SDRT_route.Addresses[1].Longitude
                            : -83.244743347168;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeId,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("ClientFactoryTest.Resources.test.png");

            using (Stream stream = resourceStream)
            {
                string tempFilePath = null;

                using (var tempFiles = new TempFileCollection())
                {
                    {
                        tempFilePath = tempFiles.AddExtension("png");
                        _output.WriteLine(tempFilePath);
                        using (Stream fileStream = File.OpenWrite(tempFilePath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                    // Run the query
                    string contents = "Test Note Contents with Attachment " + DateTime.Now.ToString();
                    var noteResult = await r4mController.AddAddressNote(
                                                    noteParameters,
                                                    contents,
                                                    tempFilePath);

                    Assert.True(noteResult != null,
                        $"AddAddressNoteWithFileTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(noteResult.Item2));

                    Assert.IsType<AddAddressNoteResponse>(noteResult.Item1);
                };
            }
        }

        [FactSkipable]
        public async Task GetAddressNotesTest()
        {
            string routeId = _fixture.tdr.SDRT_route_id;
            Assert.True(routeId!=null, "routeId_SingleDriverRoundTrip is null.");

            int routeDestinationId = (_fixture.tdr.DataObjectSDRT != null &&
                                        _fixture.tdr.SDRT_route != null &&
                                        _fixture.tdr.SDRT_route.Addresses.Length > 1 &&
                                        _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId != null)
                                     ? _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                                     : 0;

            var noteParameters = new NoteParameters()
            {
                RouteId = routeId,
                AddressId = routeDestinationId
            };

            // Run the query
            var notesResult = await r4mController.GetAddressNotes(noteParameters);

            Assert.True(notesResult != null,
                        $"GetAddressNotesTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(notesResult.Item2));

            Assert.IsType<AddressNote[]>(notesResult.Item1);
        }

        [FactSkipable]
        public async Task AddCustomNoteTypeTest()
        {
            var customNoteResult = await r4mController.AddCustomNoteType(
                                    "To Do", 
                                    new string[] { "Pass a package", "Pickup package", "Do a service" });

            Assert.True((customNoteResult?.Item1 ?? null) != null,
                        $"AddCustomNoteTypeTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(customNoteResult.Item2));

            Assert.IsType<AddCustomNoteTypeResponse>(customNoteResult.Item1);

            _fixture.removeCustomNoteTypes.Add("To Do");
        }

        [FactSkipable]
        public async Task RemoveCustomNoteTypeTest()
        {
            var noteRetrieveResult = await r4mController.GetCustomNoteType("To Do");

            Assert.True((noteRetrieveResult?.Item1?.NoteCustomTypeID ?? null) != null,
                        $"Cannot retrieve the custom note type 'To Do'. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(noteRetrieveResult.Item2));

            // Run the query
            var removeResult = await r4mController.RemoveCustomNoteType(
                                            noteRetrieveResult.Item1.NoteCustomTypeID);

            Assert.True((removeResult?.Item1 ?? null) != null,
                        $"RemoveCustomNoteTypeTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(removeResult.Item2));

            Assert.IsType<AddCustomNoteTypeResponse>(removeResult.Item1);
        }

        [FactSkipable]
        public async Task GetAllCustomNoteTypesTest()
        {
            var result = await r4mController.GetAllCustomNoteTypes();

            Assert.True((result?.Item1 ?? null) != null,
                        $"GetAllCustomNoteTypesTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(result.Item2));

            Assert.IsType<CustomNoteType[]>(result.Item1);
        }

        [FactSkipable]
        public async Task AddCustomNoteToRouteTest()
        {
            var noteParameters = new NoteParameters()
            {
                RouteId = _fixture.tdr.SDRT_route.RouteId,
                AddressId = _fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                            ? (int)_fixture.tdr.SDRT_route.Addresses[1].RouteDestinationId
                            : 0,
                Format = "json",
                Latitude = _fixture.tdr.SDRT_route.Addresses[1].Latitude,
                Longitude = _fixture.tdr.SDRT_route.Addresses[1].Longitude
            };

            var customNotes = new Dictionary<string, string>()
            {
                {"custom_note_type[11]", "slippery"},
                {"custom_note_type[10]", "Backdoor"},
                {"strUpdateType", "dropoff"},
                {"strNoteContents", "test1111"}
            };

            var result = await r4mController.AddCustomNoteToRoute(
                                            noteParameters,
                                            customNotes);

            Assert.True((result?.Item1 ?? null) != null,
                        $"AddCustomNoteToRouteTest failed. Error: {Environment.NewLine}" +
                        r4mApi4Service.ErrorResponseToString(result.Item2));

            Assert.IsType<AddAddressNoteResponse>(result.Item1);
        }
    }
}
