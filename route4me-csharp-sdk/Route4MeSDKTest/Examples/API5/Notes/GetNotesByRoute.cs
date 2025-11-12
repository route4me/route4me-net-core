using System;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting notes by route ID using the API 5 endpoint.
        /// </summary>
        public void GetNotesByRoute()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // Ensure we have a test route
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var result = notesManager.GetNotesByRoute(SD10Stops_route_id_V5, 1, 10, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("GetNotesByRoute executed successfully");
                Console.WriteLine("Total notes: {0}", result.Total);
                Console.WriteLine("Current page: {0}", result.CurrentPage);
                Console.WriteLine("Per page: {0}", result.PerPage);

                if (result.Data != null && result.Data.Length > 0)
                {
                    foreach (var note in result.Data)
                    {
                        Console.WriteLine("  Note ID: {0}, Contents: {1}", note.NoteId, note.Contents);
                    }
                }
                else
                {
                    Console.WriteLine("No notes found on this route.");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "GetNotesByRoute");
            }
        }
    }
}