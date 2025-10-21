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

            Console.WriteLine("Creating a new facility...\n");

            var facilityRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Test Facility " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "123 Industrial Blvd, Chicago, IL 60601",
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
                Console.WriteLine($"  Created at: {createdFacility.CreatedAt}");
                
                if (createdFacility.FacilityTypes != null && createdFacility.FacilityTypes.Length > 0)
                {
                    Console.WriteLine($"  Facility Types: {createdFacility.FacilityTypes.Length}");
                    foreach (var type in createdFacility.FacilityTypes)
                    {
                        Console.WriteLine($"    - Type ID: {type.FacilityTypeId}, Default: {type.IsDefault}");
                    }
                }

                Console.WriteLine("\nCleaning up test facility...");
                if (facilitiesToRemove == null)
                {
                    facilitiesToRemove = new System.Collections.Generic.List<string>();
                }
                facilitiesToRemove.Add(createdFacility.FacilityId);
                RemoveTestFacilities();
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

