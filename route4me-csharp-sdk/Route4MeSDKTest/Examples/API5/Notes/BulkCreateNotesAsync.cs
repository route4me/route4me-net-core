using System;
using System.Threading.Tasks;
using Route4MeSDK.QueryTypes.V5.Notes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Bulk create notes asynchronously for multiple routes or destinations
        /// </summary>
        public async void BulkCreateNotesAsync()
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
                        StrNoteContents = "Async bulk note 1 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = addressId1,
                        DeviceType = "web",
                        StrUpdateType = "dropoff",
                        DevLat = 40.7636197,
                        DevLng = -73.9744388,
                        RemoteSpeed = 55.0,
                        RemoteAltitude = 150.0
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = routeId,
                        StrNoteContents = "Async bulk note 2 created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        AddressId = addressId2,
                        DeviceType = "iphone",
                        StrUpdateType = "pickup",
                        UtcTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    },
                    new NoteStoreBulkItem
                    {
                        RouteId = routeId,
                        StrNoteContents = "Async bulk note 3 - route-level note",
                        DeviceType = "android_tablet"
                    }
                },
                DeviceType = "api"
            };

            // Execute bulk create asynchronously
            var result = await notesManager.BulkCreateNotesAsync(bulkRequest);

            // Output the result
            Console.WriteLine("");

            if (result.Item1 != null && result.Item1.Status)
            {
                Console.WriteLine("BulkCreateNotesAsync executed successfully");
                Console.WriteLine("Status: {0}", result.Item1.Status);
                Console.WriteLine("Async: {0}", result.Item1.Async);
                Console.WriteLine("");
                Console.WriteLine("Note: Bulk creation is asynchronous. Notes will be created in the background.");
            }
            else
            {
                Console.WriteLine("BulkCreateNotesAsync error");
                if (result.Item2 != null && result.Item2.Messages != null)
                {
                    foreach (var message in result.Item2.Messages)
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
