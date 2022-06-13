using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKUnitTest.Types
{
    [DataContract]
    internal class MyAddressAndParametersHolder : GenericParameters
    {
        [DataMember] public Address[] Addresses { get; set; }

        [DataMember] public RouteParameters Parameters { get; set; }
    }
}