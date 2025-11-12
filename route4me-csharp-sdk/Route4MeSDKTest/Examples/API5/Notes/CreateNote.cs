using System;

using Route4MeSDK.DataTypes.V5.Notes;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a note using the API 5 endpoint.
        /// </summary>
        public void CreateNote()
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
                StrNoteContents = "Test note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AddressId = (long)SD10Stops_route_V5.Addresses[1].RouteDestinationId,
                DeviceType = "web"
            };

            var result = notesManager.CreateNote(request, out var resultResponse);

            if (result != null)
            {
                Console.WriteLine("CreateNote executed successfully");
                Console.WriteLine("Note ID: {0}", result.NoteId);
                Console.WriteLine("Contents: {0}", result.Contents);
            }
            else
            {
                PrintFailResponse(resultResponse, "CreateNote");
            }
        }
    }
}