using System;

using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of deleting a note asynchronously using the API 5 endpoint.
        /// </summary>
        public async void DeleteNoteAsync()
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
                StrNoteContents = "Note to be deleted async",
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var createdNote = await notesManager.CreateNoteAsync(createRequest);

            if (createdNote.Item1?.NoteId == null)
            {
                Console.WriteLine("Failed to create note for delete test.");
                return;
            }

            // Delete the note
            var result = await notesManager.DeleteNoteAsync(createdNote.Item1.NoteId.Value);

            if (result.Item1 != null)
            {
                Console.WriteLine("DeleteNoteAsync executed successfully");
                Console.WriteLine("Note ID: {0}", result.Item1.NoteId);
                Console.WriteLine("Status: {0}", result.Item1.Status);
            }
            else
            {
                PrintFailResponse(result.Item2, "DeleteNoteAsync");
            }
        }
    }
}