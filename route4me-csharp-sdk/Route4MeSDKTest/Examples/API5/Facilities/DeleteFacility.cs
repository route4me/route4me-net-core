using System;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to delete a facility using the API V5 endpoint.
        /// Shows the complete delete workflow including verification.
        /// </summary>
        public void DeleteFacility()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // First, create a test facility to delete
            Console.WriteLine("Creating a test facility to delete...");
            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Facility to Delete " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "789 Temporary Rd, Chicago, IL 60603",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8750,
                    Lng = -87.6250
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment
                    {
                        FacilityTypeId = 1,
                        IsDefault = true
                    }
                },
                Status = 1
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(
                createRequest,
                out ResultResponse createResponse
            );

            if (createdFacility == null || createResponse != null)
            {
                Console.WriteLine("✗ Failed to create test facility");
                if (createResponse != null && createResponse.Messages != null)
                {
                    foreach (var msg in createResponse.Messages)
                    {
                        Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                    }
                }
                return;
            }

            Console.WriteLine($"Test facility created: {createdFacility.FacilityId}");
            Console.WriteLine($"  Name: {createdFacility.FacilityAlias}");

            // Get facility count before deletion
            var beforeParameters = new FacilityGetParameters
            {
                Page = 1,
                PerPage = 100
            };

            var facilitiesBefore = route4Me.FacilityManager.GetFacilities(
                beforeParameters,
                out ResultResponse beforeResponse
            );

            int countBefore = facilitiesBefore?.Total ?? 0;
            Console.WriteLine($"\nTotal facilities before deletion: {countBefore}");

            // Now delete the facility
            Console.WriteLine($"\nDeleting facility {createdFacility.FacilityId}...");
            var deleteResult = route4Me.FacilityManager.DeleteFacility(
                createdFacility.FacilityId,
                out ResultResponse deleteResponse
            );

            if (deleteResult != null && deleteResponse == null)
            {
                Console.WriteLine("Facility deleted successfully!");
                Console.WriteLine($"  Remaining facilities returned: {deleteResult.Length}");
            }
            else
            {
                Console.WriteLine("✗ Failed to delete facility");
                if (deleteResponse != null && deleteResponse.Messages != null)
                {
                    foreach (var msg in deleteResponse.Messages)
                    {
                        Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                    }
                }
                return;
            }

            // Verify deletion by trying to retrieve the facility
            Console.WriteLine("\nVerifying deletion...");
            System.Threading.Thread.Sleep(500); // Brief pause for API sync

            var verifyFacility = route4Me.FacilityManager.GetFacility(
                createdFacility.FacilityId,
                out ResultResponse verifyResponse
            );

            if (verifyFacility == null)
            {
                Console.WriteLine("Deletion verified - facility no longer exists");
            }
            else
            {
                Console.WriteLine("⚠ WARNING: Facility still exists after delete");
                Console.WriteLine($"  Facility status: {verifyFacility.Status}");
            }

            // Check facility count after deletion
            System.Threading.Thread.Sleep(1000); // Allow time for API to update

            var afterParameters = new FacilityGetParameters
            {
                Page = 1,
                PerPage = 100
            };

            var facilitiesAfter = route4Me.FacilityManager.GetFacilities(
                afterParameters,
                out ResultResponse afterResponse
            );

            int countAfter = facilitiesAfter?.Total ?? 0;
            Console.WriteLine($"\nTotal facilities after deletion: {countAfter}");

            if (countBefore > countAfter)
            {
                Console.WriteLine($"Facility count decreased by {countBefore - countAfter}");
            }
            else
            {
                Console.WriteLine("⚠ Note: Facility count unchanged (API may have delay in reflecting deletions)");
            }
        }
    }
}