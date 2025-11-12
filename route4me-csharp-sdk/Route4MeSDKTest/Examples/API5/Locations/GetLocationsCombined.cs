using System;

using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get combined list of locations with data and configuration.
        /// This example demonstrates retrieving locations with pagination, filtering, and custom field selection.
        /// </summary>
        public void GetLocationsCombined()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                },
                Page = 1,
                PerPage = 10,
                SelectedFields = new[] { "address_id", "address_1", "address_alias", "lat", "lng" }
            };

            var result = route4Me.GetLocationsCombined(request, out var resultResponse);

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine($"Error: {string.Join(", ", resultResponse.Messages)}");
                return;
            }

            if (result?.Data?.Items != null)
            {
                Console.WriteLine($"Retrieved {result.Data.Items.Length} locations");
                Console.WriteLine($"Total items count: {result.Data.TotalItemsCount}");
                Console.WriteLine($"Current page index: {result.Data.CurrentPageIndex}");

                foreach (var location in result.Data.Items)
                {
                    Console.WriteLine($"  Address ID: {location.AddressId}, Address: {location.Address1}, Alias: {location.AddressAlias}");
                }
            }
            else
            {
                Console.WriteLine("No locations found.");
            }
        }
    }
}