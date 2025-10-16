using System;
using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a note asynchronously using the API 5 endpoint.
        /// </summary>
        public async void CreateNoteAsync()
        {
            var notesManager = new NotesManagerV5(ActualApiKey);

            // Ensure we have a test route
            if (!RunOptimizationSingleDriverRoute10StopsV5())
            {
                Console.WriteLine("Cannot create test route for note creation.");
                return;
            }

            var request = new NoteStoreRequest
            {
                RouteId = SD10Stops_route_id_V5,
                StrNoteContents = "Async test note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var result = await notesManager.CreateNoteAsync(request);

            if (result.Item1 != null)
            {
                Console.WriteLine("CreateNoteAsync executed successfully");
                Console.WriteLine("Note ID: {0}", result.Item1.NoteId);
                Console.WriteLine("Contents: {0}", result.Item1.Contents);
            }
            else
            {
                PrintFailResponse(result.Item2, "CreateNoteAsync");
            }
        }
    }
}
