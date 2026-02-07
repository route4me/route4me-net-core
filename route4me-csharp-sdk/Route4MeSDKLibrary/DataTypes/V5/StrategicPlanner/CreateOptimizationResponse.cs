using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Response from creating a strategic optimization
    /// </summary>
    [DataContract]
    public sealed class CreateOptimizationResponse
    {
        /// <summary>
        /// Success message
        /// </summary>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// Created optimization ID
        /// </summary>
        [DataMember(Name = "optimization_id", EmitDefaultValue = false)]
        public string OptimizationId { get; set; }

        /// <summary>
        /// Status flag
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool? Status { get; set; }
    }

    /// <summary>
    /// Response from creating or updating a strategic optimization
    /// </summary>
    [DataContract]
    public sealed class CreateOrUpdateStrategicOptimizationResponse
    {
        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Created draft locations
        /// </summary>
        [DataMember(Name = "locations", EmitDefaultValue = false)]
        public DraftLocationResource[] Locations { get; set; }
    }
}
