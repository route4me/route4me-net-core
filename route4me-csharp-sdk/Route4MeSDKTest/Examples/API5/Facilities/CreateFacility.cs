using System;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to create a new facility using the API V5 endpoint.
        /// Facilities represent physical locations like warehouses, distribution centers, or depots.
        /// </summary>
        public void CreateFacility()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var facilityRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Test Facility " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "123 Industrial Blvd, Chicago, IL 45678",
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
                facilityRequest, 
                out ResultResponse resultResponse
            );

            if (createdFacility != null && resultResponse == null)
            {
                Console.WriteLine("Facility created successfully!");
                Console.WriteLine($"  Facility ID: {createdFacility.FacilityId}");
                Console.WriteLine($"  Name: {createdFacility.FacilityAlias}");
                Console.WriteLine($"  Address: {createdFacility.Address}");
                Console.WriteLine($"  Status: {createdFacility.Status}");
                Console.WriteLine($"  Coordinates: ({createdFacility.Coordinates?.Lat}, {createdFacility.Coordinates?.Lng})");

                if (facilitiesToRemove == null)
                {
                    facilitiesToRemove = new System.Collections.Generic.List<string>();
                }
                facilitiesToRemove.Add(createdFacility.FacilityId);

                Console.WriteLine("\nRetrieving the created facility...");
                var retrievedFacility = route4Me.FacilityManager.GetFacility(
                    createdFacility.FacilityId, 
                    out ResultResponse getResponse
                );

                if (retrievedFacility != null)
                {
                    Console.WriteLine($"Facility retrieved: {retrievedFacility.FacilityAlias}");
                }

                Console.WriteLine("\nUpdating the facility...");
                var updateRequest = new FacilityUpdateRequest
                {
                    FacilityAlias = "Updated Facility " + Guid.NewGuid().ToString().Substring(0, 8),
                    Address = createdFacility.Address,
                    Coordinates = new FacilityCoordinates { Lat = 41.8781, Lng = -87.6298 },
                    FacilityTypes = new FacilityTypeAssignment[]
                    {
                        new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                    },
                    Status = 2
                };

                var updatedFacility = route4Me.FacilityManager.UpdateFacility(
                    createdFacility.FacilityId,
                    updateRequest,
                    out ResultResponse updateResponse
                );

                if (updatedFacility != null)
                {
                    Console.WriteLine($"Facility updated: {updatedFacility.FacilityAlias}");
                    Console.WriteLine($"  New status: {updatedFacility.Status}");
                }
                else
                {
                    Console.WriteLine($"✗ Failed to update facility");
                    if (updateResponse != null && updateResponse.Messages != null)
                    {
                        foreach (var msg in updateResponse.Messages)
                        {
                            Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                        }
                    }
                }

                Console.WriteLine("\nRetrieving facility types...");
                var facilityTypes = route4Me.FacilityManager.GetFacilityTypes(out ResultResponse typesResponse);

                if (facilityTypes != null && facilityTypes.Count > 0)
                {
                    Console.WriteLine($"Found {facilityTypes.Count} facility type(s):");
                    foreach (var type in facilityTypes)
                    {
                        Console.WriteLine($"  - ID: {type.FacilityTypeId}, Name: {type.FacilityTypeAlias}");
                    }
                }
                else
                {
                    Console.WriteLine($"✗ Failed to get facility types");
                    if (typesResponse != null && typesResponse.Messages != null)
                    {
                        foreach (var msg in typesResponse.Messages)
                        {
                            Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                        }
                    }
                }

                Console.WriteLine("\nRetrieving a single facility type by ID...");
                int facilityTypeId = 1; 
                var singleFacilityType = route4Me.FacilityManager.GetFacilityType(
                    facilityTypeId, 
                    out ResultResponse singleTypeResponse
                );

                if (singleFacilityType != null)
                {
                    Console.WriteLine($"Retrieved facility type by ID {facilityTypeId}:");
                    Console.WriteLine($"  - ID: {singleFacilityType.FacilityTypeId}");
                    Console.WriteLine($"  - Name: {singleFacilityType.FacilityTypeAlias}");
                }
                else
                {
                    Console.WriteLine($"✗ Failed to get facility type by ID {facilityTypeId}");
                    if (singleTypeResponse != null && singleTypeResponse.Messages != null)
                    {
                        foreach (var msg in singleTypeResponse.Messages)
                        {
                            Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                        }
                    }
                }

                Console.WriteLine("\nRetrieving all facilities (before delete)...");
                var getFacilitiesParamsBefore = new FacilityGetParameters
                {
                    Page = 1,
                    PerPage = 100
                };

                var facilitiesBefore = route4Me.FacilityManager.GetFacilities(
                    getFacilitiesParamsBefore, 
                    out ResultResponse facilitiesResponseBefore
                );

                if (facilitiesBefore != null && facilitiesBefore.Data != null)
                {
                    Console.WriteLine($"Total facilities BEFORE delete: {facilitiesBefore.Total}");
                }

                Console.WriteLine("\nDeleting the facility...");
                RemoveTestFacilities();

                Console.WriteLine("\nVerifying facility was deleted...");
                var verifyDeleted = route4Me.FacilityManager.GetFacility(
                    createdFacility.FacilityId, 
                    out ResultResponse verifyResponse
                );

                if (verifyDeleted == null)
                {
                    Console.WriteLine($"Facility deletion verified - facility no longer exists");
                }
                else
                {
                    Console.WriteLine($"✗ WARNING: Facility still exists after delete!");
                }

                Console.WriteLine("\nRetrieving all facilities (after delete)...");
                System.Threading.Thread.Sleep(1000);
                
                var getFacilitiesParamsAfter = new FacilityGetParameters
                {
                    Page = 1,
                    PerPage = 100
                };

                var facilitiesAfter = route4Me.FacilityManager.GetFacilities(
                    getFacilitiesParamsAfter, 
                    out ResultResponse facilitiesResponseAfter
                );

                if (facilitiesAfter != null && facilitiesAfter.Data != null)
                {
                    Console.WriteLine($"Total facilities AFTER delete: {facilitiesAfter.Total}");
                    if (facilitiesBefore.Total > facilitiesAfter.Total)
                    {
                        Console.WriteLine($"Delete verified! Count decreased by {facilitiesBefore.Total - facilitiesAfter.Total}");
                    }
                    else
                    {
                        Console.WriteLine($"⚠ Count unchanged (API may have delay in reflecting deletions)");
                    }
                }
            }
            else
            {
                Console.WriteLine("✗ Failed to create facility");
                if (resultResponse != null && resultResponse.Messages != null)
                {
                    foreach (var message in resultResponse.Messages)
                    {
                        Console.WriteLine($"  Error: {message.Key} - {string.Join(", ", message.Value)}");
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to remove test facilities created during examples
        /// </summary>
        private void RemoveTestFacilities()
        {
            if (facilitiesToRemove == null || facilitiesToRemove.Count == 0)
                return;

            var route4Me = new Route4MeManagerV5(ActualApiKey);

            foreach (var facilityId in facilitiesToRemove)
            {
                var result = route4Me.FacilityManager.DeleteFacility(facilityId, out ResultResponse response);
                
                if (result != null && response == null)
                {
                    Console.WriteLine($"Deleted facility: {facilityId}");
                }
                else
                {
                    Console.WriteLine($"✗ Failed to delete facility: {facilityId}");
                    if (response != null && response.Messages != null)
                    {
                        foreach (var msg in response.Messages)
                        {
                            Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                        }
                    }
                }
            }

            facilitiesToRemove.Clear();
        }
    }
}

