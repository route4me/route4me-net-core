using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the process of adding the orders to a route.
    /// </summary>
    [DataContract]
    internal sealed class AddOrdersToRouteRequest : GenericParameters
    {
        /// <value>The route ID</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>If equal to 1, it will be redirected</value>
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public int? Redirect { get; set; }

        /// <value>The array of the addresses</value>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Route4MeSDK.DataTypes.Address[] Addresses { get; set; }

        /// <value>The route parameters</value>
        [DataMember(Name = "parameters", EmitDefaultValue = false)]
        public Route4MeSDK.DataTypes.RouteParameters Parameters { get; set; }
    }
}