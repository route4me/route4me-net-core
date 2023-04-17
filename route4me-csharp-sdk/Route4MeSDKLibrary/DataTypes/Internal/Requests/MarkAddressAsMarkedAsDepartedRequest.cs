using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for an address marking process as marked as departed.
    /// </summary>
    [DataContract]
    internal sealed class MarkAddressAsMarkedAsDepartedRequest : GenericParameters
    {
        /// <value>The route ID</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>The route destination ID</value>
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public long? RouteDestinationId { get; set; }

        /// <value>If true an address will be marked as marked as departed</value>
        [IgnoreDataMember]
        [System.Runtime.Serialization.DataMember(Name = "is_departed", EmitDefaultValue = false)] 
        public bool? IsDeparted { get; set; }

        /// <value>If true an address will be marked as marked as visited</value>
        [IgnoreDataMember]
        [System.Runtime.Serialization.DataMember(Name = "is_visited", EmitDefaultValue = false)]
        public bool? IsVisited { get; set; }

        /// <summary>
        ///    Last visited timestamp
        /// </summary>
        [DataMember(Name = "timestamp_last_visited", EmitDefaultValue = false)]
        public long? TimestampLastVisited { get; set; }

        /// <summary>
        ///     The position latitude (visited)
        /// </summary>
        [DataMember(Name = "visited_lat", EmitDefaultValue = false)]
        public double? VisitedLatitude { get; set; }

        /// <summary>
        ///     The position longitude (visited)
        /// </summary>
        [DataMember(Name = "visited_lng", EmitDefaultValue = false)]
        public double? VisitedLongitude { get; set; }

        /// <summary>
        ///    Last departed timestamp
        /// </summary>
        [DataMember(Name = "timestamp_last_departed", EmitDefaultValue = false)]
        public long? TimestampLastDeparted { get; set; }

        /// <summary>
        ///     The position latitude (departed)
        /// </summary>
        [DataMember(Name = "departed_lat", EmitDefaultValue = false)]
        public double? DepartedLatitude { get; set; }

        /// <summary>
        ///     The position longitude (departed)
        /// </summary>
        [DataMember(Name = "departed_lng", EmitDefaultValue = false)]
        public double? DepartedLongitude { get; set; }
    }
}
