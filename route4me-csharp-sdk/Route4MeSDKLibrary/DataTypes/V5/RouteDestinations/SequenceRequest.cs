using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// A single lat/lng coordinate for sequence optimisation.
    /// </summary>
    [DataContract]
    public class SequenceCoordinate
    {
        /// <summary>Latitude of the location.</summary>
        [DataMember(Name = "lat")]
        public float Lat { get; set; }

        /// <summary>Longitude of the location.</summary>
        [DataMember(Name = "lng")]
        public float Lng { get; set; }
    }

    /// <summary>
    /// Request body for POST /route-destinations/sequence.
    /// Provides an ordered list of coordinates to be optimised.
    /// </summary>
    [DataContract]
    public class DestinationSequenceRequest : GenericParameters
    {
        /// <summary>
        /// Array of coordinates to optimise the visit sequence for.
        /// Each entry must include a valid latitude and longitude.
        /// </summary>
        [DataMember(Name = "coordinates")]
        public SequenceCoordinate[] Coordinates { get; set; }
    }

    /// <summary>
    /// A single optimised sequence result item.
    /// </summary>
    [DataContract]
    public class DestinationSequenceResponse
    {
        /// <summary>
        /// Array of input coordinate indices in the optimised visit order.
        /// The values are 0-based positions referencing the original <c>coordinates</c> array.
        /// </summary>
        [DataMember(Name = "optimized_sequence")]
        public int[] OptimizedSequence { get; set; }
    }

    /// <summary>
    /// Response wrapper for POST /route-destinations/sequence.
    /// </summary>
    [DataContract]
    public class DestinationSequenceListResponse
    {
        /// <summary>Array of sequence result items.</summary>
        [DataMember(Name = "items")]
        public DestinationSequenceResponse[] Items { get; set; }
    }
}
