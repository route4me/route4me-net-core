using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Response from accepting a scenario
    /// </summary>
    [DataContract]
    public sealed class AcceptScenarioResponse
    {
        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool? Status { get; set; }

        /// <summary>
        /// Job ID for tracking the acceptance process
        /// </summary>
        [DataMember(Name = "job_id", EmitDefaultValue = false)]
        public string JobId { get; set; }
    }
}
