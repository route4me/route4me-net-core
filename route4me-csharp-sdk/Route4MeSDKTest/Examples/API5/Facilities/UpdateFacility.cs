using System;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to update an existing facility using the API V5 endpoint.
        /// Shows updating various facility attributes including name, address, status, and contact information.
        /// </summary>
        public void UpdateFacility()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // First, create a test facility to update
            Console.WriteLine("Creating a test facility...");
            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Original Facility " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "123 Original St, Chicago, IL 60601",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
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
            Console.WriteLine($"  Original name: {createdFacility.FacilityAlias}");
            Console.WriteLine($"  Original status: {createdFacility.Status}");

            // Now update the facility
            Console.WriteLine("\nUpdating facility...");
            var updateRequest = new FacilityUpdateRequest
            {
                FacilityAlias = "Updated Facility " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "456 Updated Ave, Chicago, IL 60602",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8800,
                    Lng = -87.6300
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment
                    {
                        FacilityTypeId = 1,
                        IsDefault = true
                    }
                },
                Status = 2 
            };

            var updatedFacility = route4Me.FacilityManager.UpdateFacility(
                createdFacility.FacilityId,
                updateRequest,
                out ResultResponse updateResponse
            );

            if (updatedFacility != null && updateResponse == null)
            {
                Console.WriteLine("Facility updated successfully!");
                Console.WriteLine($"  Facility ID: {updatedFacility.FacilityId}");
                Console.WriteLine($"  New name: {updatedFacility.FacilityAlias}");
                Console.WriteLine($"  New address: {updatedFacility.Address}");
                Console.WriteLine($"  New status: {updatedFacility.Status}");
                Console.WriteLine($"  New coordinates: ({updatedFacility.Coordinates?.Lat}, {updatedFacility.Coordinates?.Lng})");
                Console.WriteLine($"  Updated at: {updatedFacility.UpdatedAt}");
            }
            else
            {
                Console.WriteLine("✗ Failed to update facility");
                if (updateResponse != null && updateResponse.Messages != null)
                {
                    foreach (var msg in updateResponse.Messages)
                    {
                        Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                    }
                }
            }

            // Verify the update by retrieving the facility
            Console.WriteLine("\nVerifying update...");
            var verifyFacility = route4Me.FacilityManager.GetFacility(
                createdFacility.FacilityId, 
                out ResultResponse verifyResponse
            );

            if (verifyFacility != null)
            {
                Console.WriteLine($"Update verified!");
                Console.WriteLine($"  Confirmed name: {verifyFacility.FacilityAlias}");
                Console.WriteLine($"  Confirmed status: {verifyFacility.Status}");
            }

            // Clean up
            Console.WriteLine("\nCleaning up test facility...");
            if (facilitiesToRemove == null)
            {
                facilitiesToRemove = new System.Collections.Generic.List<string>();
            }
            facilitiesToRemove.Add(createdFacility.FacilityId);
            RemoveTestFacilities();
        }
    }
}

