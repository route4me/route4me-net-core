using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from the route deleting process
    /// </summary>
    [DataContract]
    internal sealed class DeleteRouteResponse
    {
        /// <value>If true, the route was deleted successfuly</value>
        [System.Runtime.Serialization.DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        /// <value>The array of the error strings</value>
        [System.Runtime.Serialization.DataMember(Name = "errors")]
        public List<string> Errors { get; set; }

        /// <value>The deleted route ID</value>
        [System.Runtime.Serialization.DataMember(Name = "route_id")]
        public string RouteId { get; set; }

        /// <value>The array of the deleted routes IDs</value>
        [System.Runtime.Serialization.DataMember(Name = "route_ids")]
        public string[] RouteIds { get; set; }
    }
}