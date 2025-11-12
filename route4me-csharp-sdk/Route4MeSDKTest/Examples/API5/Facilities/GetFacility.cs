using System;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to retrieve a single facility by ID using the API V5 endpoint.
        /// </summary>
        public void GetFacility()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // First, create a test facility to retrieve
            Console.WriteLine("Creating a test facility...");
            var createRequest = new Route4MeSDKLibrary.QueryTypes.V5.Facilities.FacilityCreateRequest
            {
                FacilityAlias = "Test Facility for Get " + Guid.NewGuid().ToString().Substring(0, 8),
                Address = "456 Main St, Springfield, IL 62701",
                Coordinates = new Route4MeSDKLibrary.DataTypes.V5.Facilities.FacilityCoordinates
                {
                    Lat = 39.7817,
                    Lng = -89.6501
                },
                FacilityTypes = new Route4MeSDKLibrary.QueryTypes.V5.Facilities.FacilityTypeAssignment[]
                {
                    new Route4MeSDKLibrary.QueryTypes.V5.Facilities.FacilityTypeAssignment
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

            // Now retrieve the facility by ID
            Console.WriteLine("\nRetrieving facility by ID...");
            var retrievedFacility = route4Me.FacilityManager.GetFacility(
                createdFacility.FacilityId,
                out ResultResponse getResponse
            );

            if (retrievedFacility != null && getResponse == null)
            {
                Console.WriteLine("Facility retrieved successfully!");
                Console.WriteLine($"  Facility ID: {retrievedFacility.FacilityId}");
                Console.WriteLine($"  Name: {retrievedFacility.FacilityAlias}");
                Console.WriteLine($"  Address: {retrievedFacility.Address}");
                Console.WriteLine($"  Status: {retrievedFacility.Status}");
                Console.WriteLine($"  Coordinates: ({retrievedFacility.Coordinates?.Lat}, {retrievedFacility.Coordinates?.Lng})");

                if (retrievedFacility.FacilityTypes != null && retrievedFacility.FacilityTypes.Length > 0)
                {
                    Console.WriteLine($"  Facility Types: {retrievedFacility.FacilityTypes.Length}");
                    foreach (var type in retrievedFacility.FacilityTypes)
                    {
                        Console.WriteLine($"    - Type ID: {type.FacilityTypeId}, Default: {type.IsDefault}");
                    }
                }
            }
            else
            {
                Console.WriteLine("✗ Failed to retrieve facility");
                if (getResponse != null && getResponse.Messages != null)
                {
                    foreach (var msg in getResponse.Messages)
                    {
                        Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                    }
                }
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