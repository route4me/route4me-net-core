using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from the route deleting process
    /// </summary>
    [DataContract]
    internal sealed class DeleteRouteResponse
    {
        /// <value>The array of the deleted routes IDs</value>
        [System.Runtime.Serialization.DataMember(Name = "route_ids")]
        public string[] RouteIds { get; set; }
    }
}
