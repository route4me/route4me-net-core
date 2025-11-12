using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the orders adding process to an optimization.
    /// </summary>
    [DataContract]
    internal sealed class AddOrdersToOptimizationRequest : GenericParameters
    {
        /// <value>The optimization problem ID</value>
        [HttpQueryMemberAttribute(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        /// <value>If equal to 1, it will be redirected</value>
        [HttpQueryMemberAttribute(Name = "redirect", EmitDefaultValue = false)]
        public int? Redirect { get; set; }

        /// <value>The array of the addresses</value>
        [System.Runtime.Serialization.DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses { get; set; }

        /// <value>The route parameters</value>
        [System.Runtime.Serialization.DataMember(Name = "parameters", EmitDefaultValue = false)]
        public RouteParameters Parameters { get; set; }
    }
}