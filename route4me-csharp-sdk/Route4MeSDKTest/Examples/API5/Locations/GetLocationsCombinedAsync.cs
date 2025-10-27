using System;
using System.Threading.Tasks;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get combined list of locations asynchronously.
        /// This async example demonstrates retrieving locations with pagination and filtering.
        /// </summary>
        public async Task GetLocationsCombinedAsync()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new LocationCombinedRequest
            {
                Filters = new LocationFilters
                {
                    SearchQuery = ""
                },
                Page = 1,
                PerPage = 10
            };

            var response = await route4Me.GetLocationsCombinedAsync(request);
            var result = response.Item1;
            var resultResponse = response.Item2;

            if (resultResponse != null && !resultResponse.Status)
            {
                Console.WriteLine($"Error: {string.Join(", ", resultResponse.Messages)}");
                return;
            }

            if (result?.Data?.Items != null)
            {
                Console.WriteLine($"Retrieved {result.Data.Items.Length} locations (async)");
                Console.WriteLine($"Total items count: {result.Data.TotalItemsCount}");

                foreach (var location in result.Data.Items)
                {
                    Console.WriteLine($"  Address ID: {location.AddressId}, Address: {location.Address1}");
                }
            }
            else
            {
                Console.WriteLine("No locations found.");
            }
        }
    }
}
