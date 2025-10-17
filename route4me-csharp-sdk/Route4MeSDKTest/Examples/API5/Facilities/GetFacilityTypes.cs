using System;
using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to retrieve facility types using the API V5 endpoint.
        /// Shows both getting all types and retrieving a specific type by ID.
        /// </summary>
        public void GetFacilityTypes()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            Console.WriteLine("Retrieving all facility types...\n");

            var facilityTypes = route4Me.FacilityManager.GetFacilityTypes(
                out ResultResponse response
            );

            if (facilityTypes != null && response == null)
            {
                Console.WriteLine("Facility types retrieved successfully!");
                Console.WriteLine($"  Total types available: {facilityTypes.Data?.Length ?? 0}\n");

                if (facilityTypes.Data != null && facilityTypes.Data.Length > 0)
                {
                    Console.WriteLine("Available facility types:");
                    for (int i = 0; i < facilityTypes.Data.Length; i++)
                    {
                        var type = facilityTypes.Data[i];
                        Console.WriteLine($"  {i + 1}. ID: {type.FacilityTypeId}");
                        Console.WriteLine($"     Name: {type.FacilityTypeAlias}");
                        Console.WriteLine();
                    }

                    if (facilityTypes.Data.Length > 0)
                    {
                        int firstTypeId = facilityTypes.Data[0].FacilityTypeId;
                        Console.WriteLine($"Retrieving specific facility type (ID: {firstTypeId})...\n");

                        var singleType = route4Me.FacilityManager.GetFacilityType(
                            firstTypeId, 
                            out ResultResponse singleTypeResponse
                        );

                        if (singleType != null && singleTypeResponse == null)
                        {
                            Console.WriteLine("Single facility type retrieved successfully!");
                            Console.WriteLine($"  ID: {singleType.FacilityTypeId}");
                            Console.WriteLine($"  Name: {singleType.FacilityTypeAlias}");
                        }
                        else
                        {
                            Console.WriteLine($"✗ Failed to retrieve facility type by ID {firstTypeId}");
                            if (singleTypeResponse != null && singleTypeResponse.Messages != null)
                            {
                                foreach (var msg in singleTypeResponse.Messages)
                                {
                                    Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                                }
                            }
                        }
                    }

                    Console.WriteLine("\n\nDemonstrating error handling with invalid facility type ID...");
                    var invalidType = route4Me.FacilityManager.GetFacilityType(
                        0, 
                        out ResultResponse invalidResponse
                    );

                    if (invalidType == null && invalidResponse != null)
                    {
                        Console.WriteLine("Error handling working correctly!");
                        Console.WriteLine($"  Status: {invalidResponse.Status}");
                        if (invalidResponse.Messages != null)
                        {
                            foreach (var msg in invalidResponse.Messages)
                            {
                                Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No facility types found in the system.");
                }
            }
            else
            {
                Console.WriteLine("✗ Failed to retrieve facility types");
                if (response != null && response.Messages != null)
                {
                    foreach (var msg in response.Messages)
                    {
                        Console.WriteLine($"  Error: {msg.Key} - {string.Join(", ", msg.Value)}");
                    }
                }
            }
        }
    }
}

