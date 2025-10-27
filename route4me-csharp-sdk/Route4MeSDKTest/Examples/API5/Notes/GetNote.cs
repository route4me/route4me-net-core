using System;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting a note by ID using the API 5 endpoint.
        /// </summary>
        public void GetNote()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // First, get notes from a route to have a valid note ID
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var notes = notesManager.GetNotesByRoute(SD10Stops_route_id_V5, 1, 10, out var notesResponse);

            if (notes?.Data == null || notes.Data.Length == 0)
            {
                Console.WriteLine("No notes found on the route.");
                return;
            }

            var noteId = notes.Data[0].NoteId;

            var result = notesManager.GetNote(noteId, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("GetNote executed successfully");
                Console.WriteLine("Note ID: {0}", result.NoteId);
                Console.WriteLine("Contents: {0}", result.Contents);
                Console.WriteLine("Route ID: {0}", result.RouteId);
            }
            else
            {
                PrintFailResponse(resultResponse, "GetNote");
            }
        }
    }
}
