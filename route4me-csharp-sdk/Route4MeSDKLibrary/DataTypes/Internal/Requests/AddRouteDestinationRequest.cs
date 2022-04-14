using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the route destination adding process.
    /// </summary>
    [DataContract]
    internal sealed class AddRouteDestinationRequest : GenericParameters
    {
        /// <value>The route ID</value>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <value>The optimization ID</value>
        [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        /// <value>The array of the Address type objects</value>
        [System.Runtime.Serialization.DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Route4MeSDK.DataTypes.Address[] Addresses { get; set; }

        /// <value>If true, an address will be inserted at optimal position of a route</value>
        [System.Runtime.Serialization.DataMember(Name = "optimal_position", EmitDefaultValue = true)]
        public bool OptimalPosition { get; set; }
    }
}
