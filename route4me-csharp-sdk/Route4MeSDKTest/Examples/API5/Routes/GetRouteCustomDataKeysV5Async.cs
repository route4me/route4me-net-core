using System;
using System.Threading.Tasks;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets all distinct custom data keys used across routes for the authenticated member's
        /// organization asynchronously using the GET /routes/custom-data/keys endpoint (API V5).
        /// </summary>
        public async Task GetRouteCustomDataKeysV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Act - retrieve all distinct custom data keys asynchronously
            var (result, resultResponse) = await route4Me.GetRouteCustomDataKeysAsync();

            Console.WriteLine("");

            if (resultResponse == null)
            {
                Console.WriteLine("GetRouteCustomDataKeysV5Async executed successfully");

                if (result != null && result.Length > 0)
                {
                    Console.WriteLine($"Found {result.Length} distinct custom data key(s):");
                    foreach (string key in result)
                    {
                        Console.WriteLine($"  {key}");
                    }
                }
                else
                {
                    Console.WriteLine("No custom data keys found for this organization.");
                }
            }
            else
            {
                PrintFailResponse(resultResponse, "GetRouteCustomDataKeysV5Async");
            }
        }
    }
}