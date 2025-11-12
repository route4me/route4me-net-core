using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response object from a route destination removing process.
    /// </summary>
    [DataContract]
    internal sealed class RemoveRouteDestinationResponse
    {
        /// <value>If true the destination was removed successfully</value>
        [System.Runtime.Serialization.DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        /// <value>Removed route destination ID</value>
        [System.Runtime.Serialization.DataMember(Name = "route_destination_id")]
        public long RouteDestinationId { get; set; }
    }
}