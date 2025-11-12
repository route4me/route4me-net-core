using System;

using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5.Notes;

using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Bulk create notes for multiple routes or destinations
        /// </summary>
        public void BulkCreateNotes()
        {
            // Create the manager with the API key
            var notesManager = new NotesManagerV5(ActualApiKey);

            // First, create a test route to add notes to
            RunSingleDriverRoundTrip();

            if (SDRT_route == null || SDRT_route.Addresses == null || SDRT_route.Addresses.Length < 2)
            {
                Console.WriteLine("Failed to create test route");
                return;
            }

            var routeId = SDRT_route_id;
            var addressId1 = SDRT_route.Addresses[0].RouteDestinationId;
            var addressId2 = SDRT_route.Addresses[1].RouteDestinationId;

            // Create bulk notes request
            var bulkRequest = new NoteStoreBulkRequest
            {
                Notes = new[]
                {
                    new NoteStoreBulkItem
                    {
                        RouteId = routeId,
                        StrNoteContents = "First bulk note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = addressId1,
                        DeviceType = "web",
                        StrUpdateType = "dropoff",
                        DevLat = 40.7636197,
                        DevLng = -73.9744388
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = routeId,
                        StrNoteContents = "Second bulk note created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = addressId2,
                        DeviceType = "web",
                        StrUpdateType = "pickup"
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = routeId,
                        StrNoteContents = "Third bulk note - general route note",
                        DeviceType = "api"
                    }
                },
                DeviceType = "web"
            };

            // Execute bulk create
            var result = notesManager.BulkCreateNotes(bulkRequest, out ResultResponse resultResponse);

            // Output the result
            Console.WriteLine("");

            if (result != null && result.Status)
            {
                Console.WriteLine("BulkCreateNotes executed successfully");
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Async: {0}", result.Async);
                Console.WriteLine("");
                Console.WriteLine("Note: Bulk creation is asynchronous. Notes will be created in the background.");
            }
            else
            {
                Console.WriteLine("BulkCreateNotes error");
                if (resultResponse != null && resultResponse.Messages != null)
                {
                    foreach (var message in resultResponse.Messages)
                    {
                        Console.WriteLine("{0}: {1}", message.Key, string.Join(", ", message.Value));
                    }
                }
            }

            // Clean up test route
            RemoveTestOptimizations();
        }
    }
}