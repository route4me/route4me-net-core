using System;

using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.RouteDestinations;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Example: compute the optimal visit sequence for a set of lat/lng coordinates (API V5).
    /// </summary>
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Submits a list of coordinates and prints the optimised visit order returned by the API.
        /// The response contains 0-based indices referencing the original coordinate array.
        /// Uses POST /route-destinations/sequence.
        /// </summary>
        public void OptimizeDestinationSequenceV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);
            const string tag = "OptimizeDestinationSequenceV5";

            Console.WriteLine("");

            var request = new DestinationSequenceRequest
            {
                Coordinates = new[]
                {
                    new SequenceCoordinate { Lat = 50.4501f, Lng = 30.5234f },  // 0 – Kyiv
                    new SequenceCoordinate { Lat = 51.5074f, Lng = -0.1278f },  // 1 – London
                    new SequenceCoordinate { Lat = 48.8566f, Lng =  2.3522f },  // 2 – Paris
                    new SequenceCoordinate { Lat = 52.5200f, Lng = 13.4050f }   // 3 – Berlin
                }
            };

            Console.WriteLine("{0}: Submitting {1} coordinates for sequence optimisation...",
                tag, request.Coordinates.Length);

            var result = route4Me.GetDestinationSequence(request, out ResultResponse resultResponse);

            if (resultResponse == null && result?.OptimizedSequence != null)
            {
                Console.WriteLine("{0} executed successfully.", tag);
                Console.WriteLine("  Optimised visit order (0-based coordinate indices):");
                for (int i = 0; i < result.OptimizedSequence.Length; i++)
                {
                    var idx = result.OptimizedSequence[i];
                    Console.WriteLine("    stop {0}: coordinate[{1}] (lat={2}, lng={3})",
                        i + 1, idx,
                        request.Coordinates[idx].Lat,
                        request.Coordinates[idx].Lng);
                }
            }
            else
            {
                PrintFailResponse(resultResponse, tag);
            }
        }
    }
}
