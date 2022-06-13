using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Types
{
    [DataContract]
    internal class AddressInfo : GenericParameters
    {
        [DataMember(Name = "route_destination_id")]
        public long DestinationId { get; set; }

        [DataMember(Name = "sequence_no")] public int SequenceNo { get; set; }

        [DataMember(Name = "is_depot")] public bool IsDepot { get; set; }
    }
}