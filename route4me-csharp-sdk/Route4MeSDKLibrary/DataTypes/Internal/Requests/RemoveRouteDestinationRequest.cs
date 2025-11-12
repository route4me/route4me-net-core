using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for a route destination removing process.
    /// </summary>
    [DataContract]
    internal sealed class RemoveRouteDestinationRequest : GenericParameters
    {
        /// <value>The route ID</value>
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>The route destination ID</value>
        [HttpQueryMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public long RouteDestinationId { get; set; }
    }
}