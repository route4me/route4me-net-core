using System;

using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get territories with location counts.
        /// This example demonstrates how to retrieve territories and see how many locations are in each territory.
        /// </summary>
        public void GetLocationsTerritories()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationTerritoriesRequest
            {
                Filters = new LocationFilters(),
                TerritoriesPage = 1,
                TerritoriesPerPage = 20,
                TerritoriesSearchQuery = "", // Optional: search for specific territory names
                TerritoriesOrderBy = new[] { new[] { "territory_name", "asc" } }
            };

            var result = route4Me.GetLocationsTerritories(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine($"Error: {string.Join(", ", resultResponse.Messages)}");
                return;
            }

            if (result?.Items != null)
            {
                Console.WriteLine($"Retrieved {result.Items.Length} territories");
                Console.WriteLine($"Total items count: {result.TotalItemsCount}");
                Console.WriteLine($"Current page index: {result.CurrentPageIndex}");
                Console.WriteLine();

                foreach (var territory in result.Items)
                {
                    Console.WriteLine($"Territory: {territory.TerritoryName}");
                    Console.WriteLine($"  ID: {territory.TerritoryId}");
                    Console.WriteLine($"  Color: {territory.TerritoryColor}");
                    Console.WriteLine($"  Member ID: {territory.MemberId}");
                    Console.WriteLine($"  Locations count: {territory.LocationsCount}");
                    Console.WriteLine();
                }

                if (result.Subtotals != null)
                {
                    Console.WriteLine("Subtotals:");
                    foreach (var subtotal in result.Subtotals)
                    {
                        Console.WriteLine($"  {subtotal.Label}: {subtotal.Field}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No territories found.");
            }
        }
    }
}