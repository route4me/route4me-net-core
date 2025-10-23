using System;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of getting notes by destination ID asynchronously using the API 5 endpoint.
        /// </summary>
        public async void GetNotesByDestinationAsync()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // Ensure we have a test route
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route.");
                return;
            }

            var destinationId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId;

            var result = await notesManager.GetNotesByDestinationAsync(destinationId, 1, 10);

            if (result.Item1 != null)
            {
                Console.WriteLine("GetNotesByDestinationAsync executed successfully");
                Console.WriteLine("Total notes: {0}", result.Item1.Total);
                Console.WriteLine("Current page: {0}", result.Item1.CurrentPage);
                Console.WriteLine("Per page: {0}", result.Item1.PerPage);

                if (result.Item1.Data != null && result.Item1.Data.Length > 0)
                {
                    foreach (var note in result.Item1.Data)
                    {
                        Console.WriteLine("  Note ID: {0}, Contents: {1}", note.NoteId, note.Contents);
                    }
                }
                else
                {
                    Console.WriteLine("No notes found for this destination.");
                }
            }
            else
            {
                PrintFailResponse(result.Item2, "GetNotesByDestinationAsync");
            }
        }
    }
}
