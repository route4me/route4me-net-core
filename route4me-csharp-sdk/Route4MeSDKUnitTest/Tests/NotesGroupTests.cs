using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class NotesGroupTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;
        private long _lastCustomNoteTypeId;

        [OneTimeSetUp]
        public void NotesGroupInitialize()
        {
            var route4Me = new Route4MeManager(CApiKey);

            _lsOptimizationIDs = new List<string>();

            _tdr = new TestDataRepository();
            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(result, "Single Driver Round Trip generation failed.");

            Assert.IsTrue(_tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SDRT_optimization_problem_id);

            var routeIdToMoveTo = _tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId = _tdr.DataObjectSDRT != null &&
                            _tdr.SDRT_route != null &&
                            _tdr.SDRT_route.Addresses.Length > 1 &&
                            _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? _tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                Format = "json"
            };

            // Run the query
            var contents = "Test Note Contents " + DateTime.Now;
            var note = route4Me.AddAddressNote(
                noteParameters,
                contents,
                out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);

            var response = route4Me.GetAllCustomNoteTypes(out errorString);

            Assert.IsTrue(response.GetType() == typeof(CustomNoteType[]), errorString);

            var notesGroup = new NotesGroupTests();

            if (((CustomNoteType[])response).Length < 2)
            {
                notesGroup.AddCustomNoteType(
                    "Conditions at Site",
                    new[] { "safe", "mild", "dangerous", "slippery" }
                );
                notesGroup.AddCustomNoteType(
                    "To Do",
                    new[] { "Pass a package", "Pickup package", "Do a service" }
                );

                response = route4Me.GetAllCustomNoteTypes(out errorString);
            }

            Assert.IsTrue(
                ((CustomNoteType[])response).Length > 0,
                "Can not find custom note type in the account. " + errorString);

            _lastCustomNoteTypeId =
                ((CustomNoteType[])response)[((CustomNoteType[])response).Length - 1].NoteCustomTypeID;
        }

        [Test]
        public void AddComplexAddressNoteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeIdToMoveTo = _tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId =
                _tdr.DataObjectSDRT != null && _tdr.SDRT_route != null && _tdr.SDRT_route.Addresses.Length > 1 &&
                _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                    ? _tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                    : 0;

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var customNotesResponse = route4Me.GetAllCustomNoteTypes(out _);

            Dictionary<string, string> customNotes = null;

            if (customNotesResponse != null && customNotesResponse.GetType() == typeof(CustomNoteType[]))
            {
                var allCustomNotes = (CustomNoteType[])customNotesResponse;

                if (allCustomNotes.Length > 0)
                    customNotes = new Dictionary<string, string>
                    {
                        {
                            "custom_note_type[" + allCustomNotes[0].NoteCustomTypeID + "]",
                            allCustomNotes[0].NoteCustomTypeValues[0]
                        }
                    };
            }

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description(),
                StrNoteContents = "Test Note Contents " + DateTime.Now
            };

            if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

            string tempFilePath = null;

            using (var stream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png"))
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

            var note = route4Me.AddAddressNote(noteParameters, out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);
        }

        [Test]
        public void AddAddressNoteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeIdToMoveTo = _tdr.SDRT_route_id;
            Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null.");

            var addressId = _tdr.DataObjectSDRT != null &&
                            _tdr.SDRT_route != null &&
                            _tdr.SDRT_route.Addresses.Length > 1 &&
                            _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? _tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeIdToMoveTo,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            // Run the query
            var contents = "Test Note Contents " + DateTime.Now;
            var note = route4Me.AddAddressNote(noteParameters, contents, out var errorString);

            Assert.IsNotNull(note, "AddAddressNoteTest failed. " + errorString);
        }

        [Test]
        public void AddAddressNoteWithFileTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SDRT_route_id;

            Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null.");

            var addressId = _tdr.DataObjectSDRT != null &&
                            _tdr.SDRT_route != null &&
                            _tdr.SDRT_route.Addresses.Length > 1 &&
                            _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? _tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var lat = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Latitude
                : 33.132675170898;
            var lng = _tdr.SDRT_route.Addresses.Length > 1
                ? _tdr.SDRT_route.Addresses[1].Longitude
                : -83.244743347168;

            var noteParameters = new NoteParameters
            {
                RouteId = routeId,
                AddressId = addressId,
                Latitude = lat,
                Longitude = lng,
                DeviceType = DeviceType.Web.Description(),
                ActivityType = StatusUpdateType.DropOff.Description()
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png");

            using (var stream = resourceStream)
            {
                string tempFilePath;

                using (var tempFiles = new TempFileCollection())
                {
                    {
                        tempFilePath = tempFiles.AddExtension("png");
                        Console.WriteLine(tempFilePath);
                        using (Stream fileStream = File.OpenWrite(tempFilePath))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                    // Run the query
                    var contents = "Test Note Contents with Attachment " + DateTime.Now;
                    var note = route4Me.AddAddressNote(
                        noteParameters,
                        contents,
                        tempFilePath,
                        out var errorString);

                    Assert.IsNotNull(note, "AddAddressNoteWithFileTest failed. " + errorString);
                }
            }
        }

        [Test]
        public void GetAddressNotesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var routeId = _tdr.SDRT_route_id;
            Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null.");

            var routeDestinationId = _tdr.DataObjectSDRT != null &&
                                     _tdr.SDRT_route != null &&
                                     _tdr.SDRT_route.Addresses.Length > 1 &&
                                     _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? _tdr.SDRT_route.Addresses[1].RouteDestinationId.Value
                : 0;

            var noteParameters = new NoteParameters
            {
                RouteId = routeId,
                AddressId = routeDestinationId
            };

            // Run the query
            var notes = route4Me.GetAddressNotes(noteParameters, out var errorString);

            Assert.That(notes, Is.InstanceOf<AddressNote[]>(), "GetAddressNotesTest failed. " + errorString);
        }

        [Test]
        public void AddCustomNoteTypeTest()
        {
            var response = AddCustomNoteType(
                "To Do",
                new[] { "Pass a package", "Pickup package", "Do a service" });

            Assert.IsTrue(response.GetType() != typeof(string), response.ToString());

            Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not create new custom note type");
        }

        public object AddCustomNoteType(string customType, string[] customValues)
        {
            var route4Me = new Route4MeManager(CApiKey);

            // Run the query
            var response = route4Me.AddCustomNoteType(customType, customValues, out var errorString);

            return response ?? errorString;
        }

        [Test]
        public void RemoveCustomNoteTypeTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            // Run the query
            var response = route4Me.RemoveCustomNoteType(_lastCustomNoteTypeId, out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not remove the custom note type");
        }

        [Test]
        public void GetAllCustomNoteTypesTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            //string errorString;
            var response = route4Me.GetAllCustomNoteTypes(out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(response.GetType() == typeof(CustomNoteType[]));
        }

        [Test]
        public void AddCustomNoteToRouteTest()
        {
            var route4Me = new Route4MeManager(CApiKey);

            var noteParameters = new NoteParameters
            {
                RouteId = _tdr.SDRT_route.RouteId,
                AddressId = _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                    ? (int)_tdr.SDRT_route.Addresses[1].RouteDestinationId
                    : 0,
                Format = "json",
                Latitude = _tdr.SDRT_route.Addresses[1].Latitude,
                Longitude = _tdr.SDRT_route.Addresses[1].Longitude
            };

            var customNotes = new Dictionary<string, string>
            {
                { "custom_note_type[11]", "slippery" },
                { "custom_note_type[10]", "Backdoor" },
                { "strUpdateType", "dropoff" },
                { "strNoteContents", "test1111" }
            };

            var response = route4Me.AddCustomNoteToRoute(
                noteParameters,
                customNotes,
                out var errorString);

            Assert.IsTrue(response.GetType() != typeof(string), errorString);

            Assert.IsTrue(response.GetType() == typeof(AddressNote));
        }

        [OneTimeTearDown]
        public void NotesGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(result, "Removing of the optimization with 24 stops failed.");
        }
    }
}