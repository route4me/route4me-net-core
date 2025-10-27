using System;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Demonstrates complete CRUD operations for Location Types.
        /// This example shows how to Create, Read, Update, and Delete location types.
        /// Location types are used to categorize locations (e.g., "Warehouse", "Customer", "Distribution Center").
        /// </summary>
        public void LocationTypesCRUD()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            Console.WriteLine("=== Location Types CRUD Example ===\n");

            // 1. CREATE - Create a new location type
            Console.WriteLine("1. Creating a new location type...");
            var createRequest = new StoreLocationTypeRequest
            {
                Name = "Example Warehouse " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Warehouse locations for inventory storage",
                IsParentType = true,  // Can have child locations
                IsChildType = false   // Cannot be a child
            };

            var created = route4Me.CreateLocationType(createRequest, out var createResponse);

            if (createResponse != null && !createResponse.Status)
            {
                Console.WriteLine($"Error creating location type: {string.Join(", ", createResponse.Messages)}");
                return;
            }

            Console.WriteLine($"  Created location type: {created.Name}");
            Console.WriteLine($"  ID: {created.LocationTypeId}");
            Console.WriteLine($"  Is Parent Type: {created.IsParentType}");
            Console.WriteLine($"  Is Child Type: {created.IsChildType}");
            Console.WriteLine();

            // 2. READ - Get all location types
            Console.WriteLine("2. Getting all location types...");
            var getRequest = new GetLocationTypesRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new LocationTypeFilters
                {
                    SearchQuery = "",
                    OnlyCustomTypes = true // Only show user-created types
                }
            };

            var allTypes = route4Me.GetLocationTypes(getRequest, out var getResponse);

            if (getResponse != null && !getResponse.Status)
            {
                Console.WriteLine($"Error getting location types: {string.Join(", ", getResponse.Messages)}");
            }
            else if (allTypes?.Data != null)
            {
                Console.WriteLine($"  Found {allTypes.Data.Length} location types");
                Console.WriteLine($"  Total: {allTypes.Meta?.Total}");
                Console.WriteLine($"  Current page: {allTypes.Meta?.CurrentPage} of {allTypes.Meta?.LastPage}");
            }
            Console.WriteLine();

            // 3. READ BY ID - Get specific location type
            Console.WriteLine("3. Getting location type by ID...");
            var retrieved = route4Me.GetLocationTypeById(created.LocationTypeId, out var getByIdResponse);

            if (getByIdResponse != null && !getByIdResponse.Status)
            {
                Console.WriteLine($"Error getting location type by ID: {string.Join(", ", getByIdResponse.Messages)}");
            }
            else
            {
                Console.WriteLine($"  Retrieved: {retrieved.Name}");
                Console.WriteLine($"  Description: {retrieved.Description}");
            }
            Console.WriteLine();

            // 4. UPDATE - Modify the location type
            Console.WriteLine("4. Updating the location type...");
            var updateRequest = new StoreLocationTypeRequest
            {
                Name = "Updated Warehouse " + Guid.NewGuid().ToString().Substring(0, 8),
                Description = "Updated warehouse locations with new description",
                IsParentType = false, // Changed from true
                IsChildType = true    // Changed from false
            };

            var updated = route4Me.UpdateLocationType(created.LocationTypeId, updateRequest, out var updateResponse);

            if (updateResponse != null && !updateResponse.Status)
            {
                Console.WriteLine($"Error updating location type: {string.Join(", ", updateResponse.Messages)}");
            }
            else
            {
                Console.WriteLine($"  Updated name: {updated.Name}");
                Console.WriteLine($"  Updated description: {updated.Description}");
                Console.WriteLine($"  Is Parent Type: {updated.IsParentType} (was {created.IsParentType})");
                Console.WriteLine($"  Is Child Type: {updated.IsChildType} (was {created.IsChildType})");
            }
            Console.WriteLine();

            // 5. DELETE - Remove the location type
            Console.WriteLine("5. Deleting the location type...");
            var deleted = route4Me.DeleteLocationType(created.LocationTypeId, out var deleteResponse);

            if (deleteResponse != null && !deleteResponse.Status)
            {
                Console.WriteLine($"Error deleting location type: {string.Join(", ", deleteResponse.Messages)}");
            }
            else
            {
                Console.WriteLine($"  Deletion status: {(deleted.Status ? "Success" : "Failed")}");
            }
            Console.WriteLine();

            // 6. VERIFY DELETION - Try to get the deleted type
            Console.WriteLine("6. Verifying deletion...");
            var notFound = route4Me.GetLocationTypeById(created.LocationTypeId, out var verifyResponse);

            if (verifyResponse != null && !verifyResponse.Status)
            {
                Console.WriteLine("  Confirmed: Location type has been deleted (not found)");
            }
            else
            {
                Console.WriteLine("  Warning: Location type still exists after deletion");
            }

            Console.WriteLine("\n=== CRUD Example Complete ===");
        }
    }
}
