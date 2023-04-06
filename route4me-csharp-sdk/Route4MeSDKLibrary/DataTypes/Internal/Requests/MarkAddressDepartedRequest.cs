using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for a address marking as the departed process.
    /// </summary>
    [DataContract]
    internal sealed class MarkAddressDepartedRequest : GenericParameters
    {
        /// <value>The route ID</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>The route destination ID</value>
        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public long? AddressId { get; set; }

        /// <value>If true an addres will be marked as departed</value>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "is_departed", EmitDefaultValue = false)]
        public bool? IsDeparted { get; set; }

        /// <value>If true an addres will be marked as visited</value>
        [IgnoreDataMember]
        [HttpQueryMemberAttribute(Name = "is_visited", EmitDefaultValue = false)]
        public bool? IsVisited { get; set; }

        /// <value>The member ID</value>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }
    }
}
