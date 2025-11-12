using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.QueryTypes
{
    /// <summary>
    ///     The information about an address
    /// </summary>
    [DataContract]
    internal class AddressInfo : GenericParameters
    {
        /// <value>The destination ID</value>
        [System.Runtime.Serialization.DataMember(Name = "route_destination_id")]
        public long DestinationId { get; set; }

        /// <value>The destination's sequence number in a route</value>
        [System.Runtime.Serialization.DataMember(Name = "sequence_no")]
        public int SequenceNo { get; set; }

        /// <value>If true the destination is depot</value>
        [System.Runtime.Serialization.DataMember(Name = "is_depot")]
        public bool IsDepot { get; set; }
    }
}