using System;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Gets all distinct custom data keys used across routes for the authenticated member's
        /// organization using the GET /routes/custom-data/keys endpoint (API V5).
        /// </summary>
        public void GetRouteCustomDataKeysV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            // Act - retrieve all distinct custom data keys
            string[] result = route4Me.GetRouteCustomDataKeys(out ResultResponse resultResponse);

            Console.WriteLine("");

            if (resultResponse == null)
            {
                Console.WriteLine("GetRouteCustomDataKeysV5 executed successfully");

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
                PrintFailResponse(resultResponse, "GetRouteCustomDataKeysV5");
            }
        }
    }
}
