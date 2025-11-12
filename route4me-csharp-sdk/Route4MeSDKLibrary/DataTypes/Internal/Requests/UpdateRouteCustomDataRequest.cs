using System.Collections.Generic;
using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the route custom data updating process
    /// </summary>
    [DataContract]
    internal sealed class UpdateRouteCustomDataRequest : GenericParameters
    {
        /// <value>A route ID to be updated</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>A route destination ID to be updated</value>
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public long? RouteDestinationId { get; set; }

        /// <value>The changed/new custom fields of a route destination</value>
        [System.Runtime.Serialization.DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomFields { get; set; }
    }
}