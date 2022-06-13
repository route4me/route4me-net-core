using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Types
{
    [DataContract]
    internal class AddressesOrderInfo : GenericParameters
    {
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        [DataMember(Name = "addresses")] public AddressInfo[] Addresses { get; set; }
    }
}