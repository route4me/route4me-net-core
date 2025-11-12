using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for an optimization removing
    /// </summary>
    [DataContract]
    internal sealed class RemoveOptimizationRequest : GenericParameters
    {
        /// <value>If true will be redirected</value>
        [HttpQueryMember(Name = "redirect", EmitDefaultValue = false)]
        public int Redirect { get; set; }

        /// <value>The array of the optimization problem IDs to be removed</value>
        [System.Runtime.Serialization.DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
        public string[] OptimizationProblemIds { get; set; }
    }
}