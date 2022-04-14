using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the updating process of a route destination
    /// </summary>
    [DataContract]
    internal sealed class UpdateRouteDestinationRequest : Route4MeSDK.DataTypes.Address
    {
        /// <value>A route ID to be updated</value>
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public new string RouteId { get; set; }

        /// <value>A optimization ID to be updated</value>
        [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public new string OptimizationProblemId { get; set; }

        /// <value>A route destination ID to be updated</value>
        [HttpQueryMemberAttribute(Name = "route_destination_id", EmitDefaultValue = false)]
        public new long? RouteDestinationId { get; set; }
    }
}
