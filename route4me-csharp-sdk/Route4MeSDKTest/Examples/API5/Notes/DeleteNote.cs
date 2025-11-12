using System;

using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting a note using the API 5 endpoint.
        /// </summary>
        public void DeleteNote()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // First, create a note to delete
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var createRequest = new NoteStoreRequest
            {
                RouteId = SD10Stops_route_id_V5,
                StrNoteContents = "Note to be deleted",
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var createdNote = notesManager.CreateNote(createRequest, out _);

            if (createdNote?.NoteId == null)
            {
                Console.WriteLine("Failed to create note for delete test.");
                return;
            }

            // Delete the note
            var result = notesManager.DeleteNote(createdNote.NoteId.Value, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("DeleteNote executed successfully");
                Console.WriteLine("Note ID: {0}", result.NoteId);
                Console.WriteLine("Status: {0}", result.Status);
            }
            else
            {
                PrintFailResponse(resultResponse, "DeleteNote");
            }
        }
    }
}