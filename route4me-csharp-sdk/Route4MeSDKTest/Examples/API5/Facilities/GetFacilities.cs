using System;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Example demonstrates how to retrieve a paginated list of facilities using the API V5 endpoint.
        /// Shows various pagination and filtering options.
        /// </summary>
        public void GetFacilities()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            Console.WriteLine("Retrieving all facilities (paginated)...\n");

            // Example 1: Get first page with 10 items per page
            var parameters = new FacilityGetParameters
            {
                Page = 1,
                PerPage = 10
            };

            var facilities = route4Me.FacilityManager.GetFacilities(
                parameters,
                out ResultResponse response
            );

            if (facilities != null && response == null)
            {
                Console.WriteLine("Facilities retrieved successfully!");
                Console.WriteLine($"  Total facilities: {facilities.Total}");
                Console.WriteLine($"  Current page: {facilities.CurrentPage}");
                Console.WriteLine($"  Per page: {facilities.PerPage}");
                Console.WriteLine($"  Total pages: {facilities.LastPage}");
                Console.WriteLine($"  Facilities on this page: {facilities.Data?.Length ?? 0}\n");

                if (facilities.Data != null && facilities.Data.Length > 0)
                {
                    Console.WriteLine("Facilities:");
                    for (int i = 0; i < facilities.Data.Length; i++)
                    {
                        var facility = facilities.Data[i];
                        Console.WriteLine($"  {i + 1}. {facility.FacilityAlias}");
                        Console.WriteLine($"     ID: {facility.FacilityId}");
                        Console.WriteLine($"     Address: {facility.Address}");
                        Console.WriteLine($"     Status: {facility.Status}");
                        Console.WriteLine($"     Coordinates: ({facility.Coordinates?.Lat}, {facility.Coordinates?.Lng})");

                        if (facility.FacilityTypes != null && facility.FacilityTypes.Length > 0)
                        {
                            Console.WriteLine($"     Types: {string.Join(", ", Array.ConvertAll(facility.FacilityTypes, t => t.FacilityTypeId.ToString()))}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No facilities found.");
                }

                // Example 2: If there are multiple pages, demonstrate getting the second page
                if (facilities.LastPage > 1)
                {
                    Console.WriteLine("\nRetrieving second page...");
                    var page2Parameters = new FacilityGetParameters
                    {
                        Page = 2,
                        PerPage = 10
                    };

                    var page2Facilities = route4Me.FacilityManager.GetFacilities(
                        page2Parameters,
                        out ResultResponse page2Response
                    );

                    if (page2Facilities != null && page2Response == null)
                    {
                        Console.WriteLine($"Page 2 retrieved with {page2Facilities.Data?.Length ?? 0} facilities");
                    }
                }

                // Example 3: Get all facilities with larger page size
                Console.WriteLine("\nRetrieving facilities with larger page size...");
                var largePageParameters = new FacilityGetParameters
                {
                    Page = 1,
                    PerPage = 100
                };

                var largePage = route4Me.FacilityManager.GetFacilities(
                    largePageParameters,
                    out ResultResponse largePageResponse
                );

                if (largePage != null && largePageResponse == null)
                {
                    Console.WriteLine($"Retrieved {largePage.Data?.Length ?? 0} facilities in one page");
                }
            }
            else
            {
                Console.WriteLine("✗ Failed to retrieve facilities");
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