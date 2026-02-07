using System;

using Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example of managing strategic planner locations (CRUD operations)
        /// </summary>
        public void ManageStrategicPlannerLocations()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // 1. Get location by ID
            Console.WriteLine("Getting location details...");
            var locationId = "YOUR_LOCATION_ID"; // Replace with actual location ID
            var location = route4Me.StrategicPlanner.GetLocation(locationId, out var resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine($"Location: {location.Alias} at {location.Address}");
                Console.WriteLine($"Days: {string.Join(", ", location.Days ?? Array.Empty<string>())}");
            }

            // 2. Update location
            Console.WriteLine("\nUpdating location...");
            var updateRequest = new LocationUpdateRequest
            {
                Alias = "Updated Customer Name",
                Days = new[] { "mon", "wed", "fri" },
                TimeWindowStart = 32400, // 9:00 AM
                TimeWindowEnd = 57600    // 4:00 PM
            };

            var updated = route4Me.StrategicPlanner.UpdateLocation(locationId, updateRequest, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine("Location updated successfully");
            }

            // 3. Patch location (partial update)
            Console.WriteLine("\nPatching location...");
            var patchRequest = new LocationUpdateRequest
            {
                Alias = "Partially Updated Name"
            };

            var patched = route4Me.StrategicPlanner.PatchLocation(locationId, patchRequest, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine("Location patched successfully");
            }

            // 4. List locations with filters
            Console.WriteLine("\nListing locations...");
            var listRequest = new ListLocationsRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new LocationFilters
                {
                    IsDepot = false,
                    Days = new[] { 1, 3, 5 } // Mon, Wed, Fri
                }
            };

            var locations = route4Me.StrategicPlanner.GetLocationsCombined(listRequest, out resultResponse);

            if (resultResponse == null)
            {
                Console.WriteLine($"Found {locations.Data.TotalItemsCount} locations");
                Console.WriteLine($"Retrieved {locations.Data.Items?.Length ?? 0} items on this page");
            }
        }

        /// <summary>
        /// Example of bulk updating locations
        /// </summary>
        public void BulkUpdateLocations()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new BulkUpdateLocationsRequest
            {
                Ids = new[] { "LOCATION_ID_1", "LOCATION_ID_2", "LOCATION_ID_3" },
                Data = new LocationBulkUpdateData
                {
                    Days = new[] { "mon", "tue", "wed", "thu", "fri" },
                    TimeWindowStart = 28800, // 8:00 AM
                    TimeWindowEnd = 61200    // 5:00 PM
                }
            };

            var success = route4Me.StrategicPlanner.BulkUpdateLocations(request, out var resultResponse);

            if (success)
            {
                Console.WriteLine("Locations updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update locations: " + 
                    string.Join(", ", resultResponse?.Messages?.Values ?? Array.Empty<string[]>()));
            }
        }

        /// <summary>
        /// Example of bulk deleting locations
        /// </summary>
        public void BulkDeleteLocations()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new BulkDeleteLocationsRequest
            {
                Ids = new[] { "LOCATION_ID_1", "LOCATION_ID_2" }
            };

            var success = route4Me.StrategicPlanner.BulkDeleteLocations(request, out var resultResponse);

            if (success)
            {
                Console.WriteLine("Locations deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete locations: " + 
                    string.Join(", ", resultResponse?.Messages?.Values ?? Array.Empty<string[]>()));
            }
        }
    }
}
