using System;

using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of updating a note asynchronously using the API 5 endpoint.
        /// </summary>
        public async void UpdateNoteAsync()
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
                StrNoteContents = "Original note content for async update",
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var createdNote = await notesManager.CreateNoteAsync(createRequest);

            if (createdNote.Item1?.NoteId == null)
            {
                Console.WriteLine("Failed to create note for update test.");
                return;
            }

            // Update the note
            var updateRequest = new NoteUpdateRequest
            {
                StrNoteContents = "Async updated note content at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceType = "web"
            };

            var result = await notesManager.UpdateNoteAsync(createdNote.Item1.NoteId.Value, updateRequest);

            if (result.Item1 != null)
            {
                Console.WriteLine("UpdateNoteAsync executed successfully");
                Console.WriteLine("Note ID: {0}", result.Item1.NoteId);
                Console.WriteLine("Updated Contents: {0}", result.Item1.Contents);
            }
            else
            {
                PrintFailResponse(result.Item2, "UpdateNoteAsync");
            }
        }
    }
}