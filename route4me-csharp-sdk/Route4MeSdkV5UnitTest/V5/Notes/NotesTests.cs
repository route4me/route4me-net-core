using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSdkV5UnitTest.V5.Notes
{
    [TestFixture]
    public class NotesTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        private static TestDataRepository _tdr;
        private static string _testRouteId;
        private static long _testDestinationId;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _tdr = new TestDataRepository();

            var result = _tdr.RunOptimizationSingleDriverRoute10Stops();
            Assert.IsTrue(result, "Single Driver 10 Stops generation failed.");

            _testRouteId = _tdr.SD10Stops_route_id;
            _testDestinationId = (long)_tdr.SD10Stops_route.Addresses[1].RouteDestinationId!;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var optimizationResult = _tdr.RemoveOptimization(new[] { _tdr.SD10Stops_optimization_problem_id });
            Assert.IsTrue(optimizationResult, "Removing of the testing optimization problem failed.");
        }

        [Test]
        public void CreateNoteTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var request = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Test note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var result = notesManager.CreateNote(request, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "CreateNote failed");
            Assert.IsNotNull(result.NoteId, "Created note ID is null");
            Assert.IsNotNull(result.Contents, "Note contents is null");
        }

        [Test]
        public async Task CreateNoteAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var request = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Async test note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var result = await notesManager.CreateNoteAsync(request);

            Assert.IsNotNull(result.Item1, "CreateNoteAsync failed");
            Assert.IsNotNull(result.Item1.NoteId);
            Assert.IsNotNull(result.Item1.Contents);
        }

        [Test]
        public void GetNoteTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to get
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Test note for GetNote",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out _);
            Assert.IsNotNull(createdNote);
            var noteId = createdNote.NoteId.Value;

            // Get the note
            var result = notesManager.GetNote(noteId, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "GetNote failed");
            Assert.AreEqual(noteId, result.NoteId, "Retrieved note ID doesn't match");
            Assert.IsNotNull(result.Contents, "Note contents is null");
        }

        [Test]
        public async Task GetNoteAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to get
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Test note for GetNoteAsync",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = await notesManager.CreateNoteAsync(createRequest);
            Assert.IsNotNull(createdNote.Item1);
            var noteId = createdNote.Item1.NoteId.Value;

            // Get the note
            var result = await notesManager.GetNoteAsync(noteId);

            Assert.IsNotNull(result.Item1, "GetNoteAsync failed");
            Assert.AreEqual(noteId, result.Item1.NoteId);
        }

        [Test]
        [Ignore("Requires an API key with permission")]
        public void UpdateNoteTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to update
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Original note content",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out _);
            Assert.IsNotNull(createdNote);
            var noteId = createdNote.NoteId.Value;

            // Update the note
            var updateRequest = new NoteUpdateRequest
            {
                StrNoteContents = "Updated note content at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceType = "web"
            };

            var result = notesManager.UpdateNote(noteId, updateRequest, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "UpdateNote failed");
            Assert.AreEqual(noteId, result.NoteId);
            Assert.IsTrue(result.Contents.Contains("Updated"), "Note content was not updated");
        }

        [Test]
        [Ignore("Requires an API key with permission")]
        public async Task UpdateNoteAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to update
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Original note content async",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = await notesManager.CreateNoteAsync(createRequest);
            Assert.IsNotNull(createdNote.Item1);
            var noteId = createdNote.Item1.NoteId.Value;

            // Update the note
            var updateRequest = new NoteUpdateRequest
            {
                StrNoteContents = "Async updated note content at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceType = "web"
            };

            var result = await notesManager.UpdateNoteAsync(noteId, updateRequest);

            Assert.IsNotNull(result.Item1, "UpdateNoteAsync failed");
            Assert.AreEqual(noteId, result.Item1.NoteId);
        }

        [Test]
        [Ignore("Requires an API key with permission")]
        public void DeleteNoteTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to delete
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Note to delete",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out _);
            Assert.IsNotNull(createdNote);
            var noteId = createdNote.NoteId.Value;

            // Delete the note
            var result = notesManager.DeleteNote(noteId, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "DeleteNote failed");
            Assert.IsTrue(result.Status, "Delete status is false");
            Assert.AreEqual(noteId, result.NoteId, "Deleted note ID doesn't match");
        }

        [Test]
        [Ignore("Requires an API key with permission")]
        public async Task DeleteNoteAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Create a note to delete
            var createRequest = new NoteStoreRequest
            {
                RouteId = _testRouteId,
                StrNoteContents = "Note to delete async",
                AddressId = _testDestinationId,
                DeviceType = "web"
            };

            var createdNote = await notesManager.CreateNoteAsync(createRequest);
            Assert.IsNotNull(createdNote.Item1);
            var noteId = createdNote.Item1.NoteId.Value;

            // Delete the note
            var result = await notesManager.DeleteNoteAsync(noteId);

            Assert.IsNotNull(result.Item1, "DeleteNoteAsync failed");
            Assert.IsTrue(result.Item1.Status);
        }

        [Test]
        public void GetNotesByRouteTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = notesManager.GetNotesByRoute(_testRouteId, 1, 10, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "GetNotesByRoute failed");
            Assert.IsNotNull(result.Data, "Notes data is null");
            Assert.That(result.GetType(), Is.EqualTo(typeof(RouteNoteCollection)));
        }

        [Test]
        public async Task GetNotesByRouteAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = await notesManager.GetNotesByRouteAsync(_testRouteId, 1, 10);

            Assert.IsNotNull(result.Item1, "GetNotesByRouteAsync failed");
            Assert.IsNotNull(result.Item1.Data);
            Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(RouteNoteCollection)));
        }

        [Test]
        public void GetNotesByDestinationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = notesManager.GetNotesByDestination(_testDestinationId, 1, 10, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "GetNotesByDestination failed");
            Assert.IsNotNull(result.Data, "Notes data is null");
            Assert.That(result.GetType(), Is.EqualTo(typeof(RouteNoteCollection)));
        }

        [Test]
        public async Task GetNotesByDestinationAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = await notesManager.GetNotesByDestinationAsync(_testDestinationId, 1, 10);

            Assert.IsNotNull(result.Item1, "GetNotesByDestinationAsync failed");
            Assert.IsNotNull(result.Item1.Data);
            Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(RouteNoteCollection)));
        }

        [Test]
        public void GetNoteCustomTypesTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = notesManager.GetNoteCustomTypes(out ResultResponse resultResponse);

            Assert.IsNotNull(result, "GetNoteCustomTypes failed");
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoteCustomTypeCollection)));
        }

        [Test]
        public async Task GetNoteCustomTypesAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var result = await notesManager.GetNoteCustomTypesAsync();

            Assert.IsNotNull(result.Item1, "GetNoteCustomTypesAsync failed");
            Assert.That(result.Item1.GetType(), Is.EqualTo(typeof(NoteCustomTypeCollection)));
        }

        [Test]
        public void CreateNoteCustomTypeTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var request = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Custom Type " + DateTime.Now.Ticks,
                NoteCustomTypeValues = new[] { "Value 1", "Value 2", "Value 3" },
                NoteCustomFieldType = 1
            };

            var result = notesManager.CreateNoteCustomType(request, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "CreateNoteCustomType failed");
            Assert.IsTrue(result.Status, "Custom type creation status is false");
        }

        [Test]
        public async Task CreateNoteCustomTypeAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var request = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Async Test Custom Type " + DateTime.Now.Ticks,
                NoteCustomTypeValues = new[] { "Async Value 1" },
                NoteCustomFieldType = 1
            };

            var result = await notesManager.CreateNoteCustomTypeAsync(request);

            Assert.IsNotNull(result.Item1, "CreateNoteCustomTypeAsync failed");
            Assert.IsTrue(result.Item1.Status);
        }

        #region Validation Tests

        [Test]
        public void CreateNoteValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = notesManager.CreateNote(null, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null request");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with missing required field (route_id)
            var request2 = new NoteStoreRequest
            {
                StrNoteContents = "Test note without route_id"
            };

            var result2 = notesManager.CreateNote(request2, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for missing route_id");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse2.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with missing required field (note contents)
            var request3 = new NoteStoreRequest
            {
                RouteId = _testRouteId
            };

            var result3 = notesManager.CreateNote(request3, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for missing note contents");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse3.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty route_id
            var request4 = new NoteStoreRequest
            {
                RouteId = "",
                StrNoteContents = "Test note"
            };

            var result4 = notesManager.CreateNote(request4, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for empty route_id");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");

            // Test with whitespace route_id
            var request5 = new NoteStoreRequest
            {
                RouteId = "   ",
                StrNoteContents = "Test note"
            };

            var result5 = notesManager.CreateNote(request5, out ResultResponse resultResponse5);
            Assert.IsNull(result5, "Expected null result for whitespace route_id");
            Assert.IsFalse(resultResponse5.Status, "Expected validation error status");
        }

        [Test]
        public void GetNoteValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null note ID
            var result1 = notesManager.GetNote(null, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null note ID");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty string note ID
            var result2 = notesManager.GetNote("", out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for empty note ID");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");

            // Test with whitespace note ID
            var result3 = notesManager.GetNote("   ", out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for whitespace note ID");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");
        }

        [Test]
        public void UpdateNoteValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null note ID
            var result1 = notesManager.UpdateNote(null, new NoteUpdateRequest { StrNoteContents = "Test" }, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null note ID");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty note ID
            var result2 = notesManager.UpdateNote("", new NoteUpdateRequest { StrNoteContents = "Test" }, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for empty note ID");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");

            // Test with null request
            var result3 = notesManager.UpdateNote(123, null, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for null request");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse3.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with both null
            var result4 = notesManager.UpdateNote(null, null, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for both null");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");
        }

        [Test]
        public void DeleteNoteValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null note ID
            var result1 = notesManager.DeleteNote(null, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null note ID");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty string note ID
            var result2 = notesManager.DeleteNote("", out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for empty note ID");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");

            // Test with whitespace note ID
            var result3 = notesManager.DeleteNote("   ", out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for whitespace note ID");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");
        }

        [Test]
        public void GetNotesByRouteValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null route ID
            var result1 = notesManager.GetNotesByRoute(null, 1, 10, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null route ID");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty route ID
            var result2 = notesManager.GetNotesByRoute("", 1, 10, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for empty route ID");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");

            // Test with whitespace route ID
            var result3 = notesManager.GetNotesByRoute("   ", 1, 10, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for whitespace route ID");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");

            // Test with invalid page number (0)
            var result4 = notesManager.GetNotesByRoute(_testRouteId, 0, 10, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for page = 0");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse4.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid page number (negative)
            var result5 = notesManager.GetNotesByRoute(_testRouteId, -1, 10, out ResultResponse resultResponse5);
            Assert.IsNull(result5, "Expected null result for negative page");
            Assert.IsFalse(resultResponse5.Status, "Expected validation error status");

            // Test with invalid per_page (0)
            var result6 = notesManager.GetNotesByRoute(_testRouteId, 1, 0, out ResultResponse resultResponse6);
            Assert.IsNull(result6, "Expected null result for per_page = 0");
            Assert.IsFalse(resultResponse6.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse6.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid per_page (negative)
            var result7 = notesManager.GetNotesByRoute(_testRouteId, 1, -1, out ResultResponse resultResponse7);
            Assert.IsNull(result7, "Expected null result for negative per_page");
            Assert.IsFalse(resultResponse7.Status, "Expected validation error status");

            // Test with both invalid pagination values
            var result8 = notesManager.GetNotesByRoute(_testRouteId, -1, -1, out ResultResponse resultResponse8);
            Assert.IsNull(result8, "Expected null result for both invalid pagination");
            Assert.IsFalse(resultResponse8.Status, "Expected validation error status");
        }

        [Test]
        public void GetNotesByDestinationValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null destination ID
            var result1 = notesManager.GetNotesByDestination(null, 1, 10, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null destination ID");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty string destination ID
            var result2 = notesManager.GetNotesByDestination("", 1, 10, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for empty destination ID");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");

            // Test with whitespace destination ID
            var result3 = notesManager.GetNotesByDestination("   ", 1, 10, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for whitespace destination ID");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");

            // Test with invalid page number (0)
            var result4 = notesManager.GetNotesByDestination(_testDestinationId, 0, 10, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for page = 0");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse4.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid page number (negative)
            var result5 = notesManager.GetNotesByDestination(_testDestinationId, -5, 10, out ResultResponse resultResponse5);
            Assert.IsNull(result5, "Expected null result for negative page");
            Assert.IsFalse(resultResponse5.Status, "Expected validation error status");

            // Test with invalid per_page (0)
            var result6 = notesManager.GetNotesByDestination(_testDestinationId, 1, 0, out ResultResponse resultResponse6);
            Assert.IsNull(result6, "Expected null result for per_page = 0");
            Assert.IsFalse(resultResponse6.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse6.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid per_page (negative)
            var result7 = notesManager.GetNotesByDestination(_testDestinationId, 1, -10, out ResultResponse resultResponse7);
            Assert.IsNull(result7, "Expected null result for negative per_page");
            Assert.IsFalse(resultResponse7.Status, "Expected validation error status");
        }

        [Test]
        public void CreateNoteCustomTypeValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = notesManager.CreateNoteCustomType(null, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null request");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with missing custom type name
            var request2 = new NoteCustomTypeStoreRequest
            {
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 1
            };

            var result2 = notesManager.CreateNoteCustomType(request2, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for missing custom type name");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse2.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty custom type name
            var request3 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 1
            };

            var result3 = notesManager.CreateNoteCustomType(request3, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for empty custom type name");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");

            // Test with whitespace custom type name
            var request4 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "   ",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 1
            };

            var result4 = notesManager.CreateNoteCustomType(request4, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for whitespace custom type name");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");

            // Test with null values array
            var request5 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = null,
                NoteCustomFieldType = 1
            };

            var result5 = notesManager.CreateNoteCustomType(request5, out ResultResponse resultResponse5);
            Assert.IsNull(result5, "Expected null result for null values array");
            Assert.IsFalse(resultResponse5.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse5.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty values array
            var request6 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = new string[] { },
                NoteCustomFieldType = 1
            };

            var result6 = notesManager.CreateNoteCustomType(request6, out ResultResponse resultResponse6);
            Assert.IsNull(result6, "Expected null result for empty values array");
            Assert.IsFalse(resultResponse6.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse6.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid field type (0)
            var request7 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 0
            };

            var result7 = notesManager.CreateNoteCustomType(request7, out ResultResponse resultResponse7);
            Assert.IsNull(result7, "Expected null result for field type = 0");
            Assert.IsFalse(resultResponse7.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse7.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid field type (5 - too high)
            var request8 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 5
            };

            var result8 = notesManager.CreateNoteCustomType(request8, out ResultResponse resultResponse8);
            Assert.IsNull(result8, "Expected null result for field type = 5");
            Assert.IsFalse(resultResponse8.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse8.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with invalid field type (negative)
            var request9 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = -1
            };

            var result9 = notesManager.CreateNoteCustomType(request9, out ResultResponse resultResponse9);
            Assert.IsNull(result9, "Expected null result for negative field type");
            Assert.IsFalse(resultResponse9.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse9.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with field type = 100 (way out of range)
            var request10 = new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test Type",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 100
            };

            var result10 = notesManager.CreateNoteCustomType(request10, out ResultResponse resultResponse10);
            Assert.IsNull(result10, "Expected null result for field type = 100");
            Assert.IsFalse(resultResponse10.Status, "Expected validation error status");
        }

        [Test]
        public async Task CreateNoteAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = await notesManager.CreateNoteAsync(null);
            Assert.IsNull(result1.Item1, "Expected null result for null request");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");
            Assert.IsTrue(result1.Item2.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with missing route_id
            var result2 = await notesManager.CreateNoteAsync(new NoteStoreRequest { StrNoteContents = "Test" });
            Assert.IsNull(result2.Item1, "Expected null result for missing route_id");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");

            // Test with missing note contents
            var result3 = await notesManager.CreateNoteAsync(new NoteStoreRequest { RouteId = _testRouteId });
            Assert.IsNull(result3.Item1, "Expected null result for missing note contents");
            Assert.IsFalse(result3.Item2.Status, "Expected validation error status");
        }

        [Test]
        public async Task UpdateNoteAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null note ID
            var result1 = await notesManager.UpdateNoteAsync(null, new NoteUpdateRequest { StrNoteContents = "Test" });
            Assert.IsNull(result1.Item1, "Expected null result for null note ID");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");

            // Test with null request
            var result2 = await notesManager.UpdateNoteAsync(123, null);
            Assert.IsNull(result2.Item1, "Expected null result for null request");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");
        }

        [Test]
        public async Task DeleteNoteAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null note ID
            var result = await notesManager.DeleteNoteAsync(null);
            Assert.IsNull(result.Item1, "Expected null result for null note ID");
            Assert.IsFalse(result.Item2.Status, "Expected validation error status");
        }

        [Test]
        public async Task GetNotesByRouteAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null route ID
            var result1 = await notesManager.GetNotesByRouteAsync(null, 1, 10);
            Assert.IsNull(result1.Item1, "Expected null result for null route ID");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");

            // Test with invalid page
            var result2 = await notesManager.GetNotesByRouteAsync(_testRouteId, 0, 10);
            Assert.IsNull(result2.Item1, "Expected null result for page = 0");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");

            // Test with invalid per_page
            var result3 = await notesManager.GetNotesByRouteAsync(_testRouteId, 1, -1);
            Assert.IsNull(result3.Item1, "Expected null result for negative per_page");
            Assert.IsFalse(result3.Item2.Status, "Expected validation error status");
        }

        [Test]
        public async Task GetNotesByDestinationAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null destination ID
            var result1 = await notesManager.GetNotesByDestinationAsync(null, 1, 10);
            Assert.IsNull(result1.Item1, "Expected null result for null destination ID");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");

            // Test with invalid page
            var result2 = await notesManager.GetNotesByDestinationAsync(_testDestinationId, -1, 10);
            Assert.IsNull(result2.Item1, "Expected null result for negative page");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");

            // Test with invalid per_page
            var result3 = await notesManager.GetNotesByDestinationAsync(_testDestinationId, 1, 0);
            Assert.IsNull(result3.Item1, "Expected null result for per_page = 0");
            Assert.IsFalse(result3.Item2.Status, "Expected validation error status");
        }

        [Test]
        public async Task CreateNoteCustomTypeAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = await notesManager.CreateNoteCustomTypeAsync(null);
            Assert.IsNull(result1.Item1, "Expected null result for null request");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");

            // Test with missing custom type name
            var result2 = await notesManager.CreateNoteCustomTypeAsync(new NoteCustomTypeStoreRequest
            {
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 1
            });
            Assert.IsNull(result2.Item1, "Expected null result for missing custom type name");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");

            // Test with empty values array
            var result3 = await notesManager.CreateNoteCustomTypeAsync(new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test",
                NoteCustomTypeValues = new string[] { },
                NoteCustomFieldType = 1
            });
            Assert.IsNull(result3.Item1, "Expected null result for empty values array");
            Assert.IsFalse(result3.Item2.Status, "Expected validation error status");

            // Test with invalid field type
            var result4 = await notesManager.CreateNoteCustomTypeAsync(new NoteCustomTypeStoreRequest
            {
                NoteCustomType = "Test",
                NoteCustomTypeValues = new[] { "Value 1" },
                NoteCustomFieldType = 10
            });
            Assert.IsNull(result4.Item1, "Expected null result for invalid field type");
            Assert.IsFalse(result4.Item2.Status, "Expected validation error status");
        }

        #endregion

        #region Bulk Create Tests

        [Test]
        public void BulkCreateNotesTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var bulkRequest = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Bulk note 1 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = _testDestinationId,
                        DeviceType = "web"
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Bulk note 2 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = _testDestinationId,
                        DeviceType = "web"
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Bulk note 3 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = _testDestinationId,
                        StrUpdateType = "dropoff"
                    }
                },
                DeviceType = "web"
            };

            var result = notesManager.BulkCreateNotes(bulkRequest, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "BulkCreateNotes failed");
            Assert.IsTrue(result.Status, "Bulk create status is false");
            Assert.IsTrue(result.Async, "Expected async flag to be true");
        }

        [Test]
        public async Task BulkCreateNotesAsyncTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var bulkRequest = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Async bulk note 1 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = _testDestinationId,
                        DeviceType = "web"
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Async bulk note 2 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = _testDestinationId,
                        DevLat = 38.024654,
                        DevLng = -77.338814
                    }
                },
                DeviceType = "api"
            };

            var result = await notesManager.BulkCreateNotesAsync(bulkRequest);

            Assert.IsNotNull(result.Item1, "BulkCreateNotesAsync failed");
            Assert.IsTrue(result.Item1.Status, "Bulk create status is false");
            Assert.IsTrue(result.Item1.Async, "Expected async flag to be true");
        }

        [Test]
        public void BulkCreateNotesValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = notesManager.BulkCreateNotes(null, out ResultResponse resultResponse1);
            Assert.IsNull(result1, "Expected null result for null request");
            Assert.IsFalse(resultResponse1.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse1.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with null notes array
            var request2 = new NoteStoreBulkRequest
            {
                Notes = null,
                DeviceType = "web"
            };

            var result2 = notesManager.BulkCreateNotes(request2, out ResultResponse resultResponse2);
            Assert.IsNull(result2, "Expected null result for null notes array");
            Assert.IsFalse(resultResponse2.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse2.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with empty notes array
            var request3 = new NoteStoreBulkRequest
            {
                Notes = new NoteStoreBulkItem[] { },
                DeviceType = "web"
            };

            var result3 = notesManager.BulkCreateNotes(request3, out ResultResponse resultResponse3);
            Assert.IsNull(result3, "Expected null result for empty notes array");
            Assert.IsFalse(resultResponse3.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse3.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with missing route_id in one note
            var request4 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Valid note"
                    },
                    new NoteStoreBulkItem
                    {
                        // Missing RouteId
                        StrNoteContents = "Invalid note - missing route_id"
                    }
                },
                DeviceType = "web"
            };

            var result4 = notesManager.BulkCreateNotes(request4, out ResultResponse resultResponse4);
            Assert.IsNull(result4, "Expected null result for missing route_id");
            Assert.IsFalse(resultResponse4.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse4.Messages.ContainsKey("Error"), "Expected error messages");
            Assert.IsTrue(resultResponse4.Messages["Error"][0].Contains("index 1"), "Expected error to mention index 1");

            // Test with missing note contents in one note
            var request5 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Valid note"
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId
                        // Missing StrNoteContents
                    }
                },
                DeviceType = "web"
            };

            var result5 = notesManager.BulkCreateNotes(request5, out ResultResponse resultResponse5);
            Assert.IsNull(result5, "Expected null result for missing note contents");
            Assert.IsFalse(resultResponse5.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse5.Messages.ContainsKey("Error"), "Expected error messages");
            Assert.IsTrue(resultResponse5.Messages["Error"][0].Contains("index 1"), "Expected error to mention index 1");

            // Test with empty route_id in first note
            var request6 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = "",
                        StrNoteContents = "Note with empty route_id"
                    }
                },
                DeviceType = "web"
            };

            var result6 = notesManager.BulkCreateNotes(request6, out ResultResponse resultResponse6);
            Assert.IsNull(result6, "Expected null result for empty route_id");
            Assert.IsFalse(resultResponse6.Status, "Expected validation error status");
            Assert.IsTrue(resultResponse6.Messages["Error"][0].Contains("index 0"), "Expected error to mention index 0");

            // Test with whitespace route_id
            var request7 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = "   ",
                        StrNoteContents = "Note with whitespace route_id"
                    }
                },
                DeviceType = "web"
            };

            var result7 = notesManager.BulkCreateNotes(request7, out ResultResponse resultResponse7);
            Assert.IsNull(result7, "Expected null result for whitespace route_id");
            Assert.IsFalse(resultResponse7.Status, "Expected validation error status");

            // Test with empty note contents
            var request8 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = ""
                    }
                },
                DeviceType = "web"
            };

            var result8 = notesManager.BulkCreateNotes(request8, out ResultResponse resultResponse8);
            Assert.IsNull(result8, "Expected null result for empty note contents");
            Assert.IsFalse(resultResponse8.Status, "Expected validation error status");

            // Test with whitespace note contents
            var request9 = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "   "
                    }
                },
                DeviceType = "web"
            };

            var result9 = notesManager.BulkCreateNotes(request9, out ResultResponse resultResponse9);
            Assert.IsNull(result9, "Expected null result for whitespace note contents");
            Assert.IsFalse(resultResponse9.Status, "Expected validation error status");
        }

        [Test]
        public async Task BulkCreateNotesAsyncValidationTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            // Test with null request
            var result1 = await notesManager.BulkCreateNotesAsync(null);
            Assert.IsNull(result1.Item1, "Expected null result for null request");
            Assert.IsFalse(result1.Item2.Status, "Expected validation error status");
            Assert.IsTrue(result1.Item2.Messages.ContainsKey("Error"), "Expected error messages");

            // Test with null notes array
            var result2 = await notesManager.BulkCreateNotesAsync(new NoteStoreBulkRequest
            {
                Notes = null
            });
            Assert.IsNull(result2.Item1, "Expected null result for null notes array");
            Assert.IsFalse(result2.Item2.Status, "Expected validation error status");

            // Test with empty notes array
            var result3 = await notesManager.BulkCreateNotesAsync(new NoteStoreBulkRequest
            {
                Notes = new NoteStoreBulkItem[] { }
            });
            Assert.IsNull(result3.Item1, "Expected null result for empty notes array");
            Assert.IsFalse(result3.Item2.Status, "Expected validation error status");

            // Test with missing route_id
            var result4 = await notesManager.BulkCreateNotesAsync(new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        StrNoteContents = "Note without route_id"
                    }
                }
            });
            Assert.IsNull(result4.Item1, "Expected null result for missing route_id");
            Assert.IsFalse(result4.Item2.Status, "Expected validation error status");

            // Test with missing note contents
            var result5 = await notesManager.BulkCreateNotesAsync(new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId
                    }
                }
            });
            Assert.IsNull(result5.Item1, "Expected null result for missing note contents");
            Assert.IsFalse(result5.Item2.Status, "Expected validation error status");
        }

        [Test]
        public void BulkCreateNotesWithOptionalFieldsTest()
        {
            var notesManager = new NotesManagerV5(CApiKey);

            var bulkRequest = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Note with all optional fields",
                        AddressId = _testDestinationId,
                        DevLat = 38.024654,
                        DevLng = -77.338814,
                        RemoteSpeed = 50.5,
                        RemoteAltitude = 100.0,
                        StrUpdateType = "dropoff",
                        DeviceType = "iphone",
                        UtcTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = _testRouteId,
                        StrNoteContents = "Note with minimal fields",
                        DeviceType = "android_phone"
                    }
                },
                DeviceType = "web"
            };

            var result = notesManager.BulkCreateNotes(bulkRequest, out ResultResponse resultResponse);

            Assert.IsNotNull(result, "BulkCreateNotes with optional fields failed");
            Assert.IsTrue(result.Status, "Bulk create status is false");
        }

        #endregion
    }
}

