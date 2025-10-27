using System;
using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a note using the API 5 endpoint.
        /// </summary>
        public void UpdateNote()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // First, create a note to update
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var createRequest = new NoteStoreRequest
            {
                RouteId = SD10Stops_route_id_V5,
                StrNoteContents = "Original note content",
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out _);

            if (createdNote?.NoteId == null)
            {
                Console.WriteLine("Failed to create note for update test.");
                return;
            }

            // Update the note
            var updateRequest = new NoteUpdateRequest
            {
                StrNoteContents = "Updated note content at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceType = "web"
            };

            var result = notesManager.UpdateNote(createdNote.NoteId.Value, updateRequest, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("UpdateNote executed successfully");
                Console.WriteLine("Note ID: {0}", result.NoteId);
                Console.WriteLine("Updated Contents: {0}", result.Contents);
            }
            else
            {
                PrintFailResponse(resultResponse, "UpdateNote");
            }
        }
    }
}
