using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.Internal.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for manually resequencing of a route
    /// </summary>
    [DataContract]
    internal sealed class ManuallyResequenceRouteRequest : GenericParameters
    {
        /// <value>The route ID to be resequenced</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>The manually resequenced addresses</value>
        [System.Runtime.Serialization.DataMember(Name = "addresses")]
        public AddressInfo[] Addresses { get; set; }
    }
}