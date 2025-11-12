using System;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a note by ID asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetNoteAsync()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // First, get notes from a route to have a valid note ID
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var notesResult = await notesManager.GetNotesByRouteAsync(SD10Stops_route_id_V5, 1, 10);

            if (notesResult.Item1?.Data == null || notesResult.Item1.Data.Length == 0)
            {
                Console.WriteLine("No notes found on the route.");
                return;
            }

            var noteId = notesResult.Item1.Data[0].NoteId;

            var result = await notesManager.GetNoteAsync(noteId);

            if (result.Item1 != null)
            {
                Console.WriteLine("GetNoteAsync executed successfully");
                Console.WriteLine("Note ID: {0}", result.Item1.NoteId);
                Console.WriteLine("Contents: {0}", result.Item1.Contents);
                Console.WriteLine("Route ID: {0}", result.Item1.RouteId);
            }
            else
            {
                PrintFailResponse(result.Item2, "GetNoteAsync");
            }
        }
    }
}