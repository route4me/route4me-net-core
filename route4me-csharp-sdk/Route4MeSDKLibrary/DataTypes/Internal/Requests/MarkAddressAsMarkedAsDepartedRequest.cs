using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for an adress marking process as marked as departed.
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
        [System.Runtime.Serialization.DataMember(Name = "is_departed")]
        public bool IsDeparted { get; set; }

        /// <value>If true an address will be marked as marked as visited</value>
        [IgnoreDataMember]
        [System.Runtime.Serialization.DataMember(Name = "is_visited")]
        public bool IsVisited { get; set; }
    }
}
