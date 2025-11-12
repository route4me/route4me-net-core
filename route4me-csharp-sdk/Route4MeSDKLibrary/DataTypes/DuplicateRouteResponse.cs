using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response from a route duplicating process
    /// </summary>
    [DataContract]
    public sealed class DuplicateRouteResponse
    {
        /// If true, the route(s) duplicated successfully
        [System.Runtime.Serialization.DataMember(Name = "status")]
        public bool Status { get; set; }

        /// An array of the duplicated route IDs
        [System.Runtime.Serialization.DataMember(Name = "route_ids")]
        public string[] RouteIDs { get; set; }
    }
}