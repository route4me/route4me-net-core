using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of strategic optimizations with combined data
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public StrategicOptimizationCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for strategic optimization combined results
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of strategic optimization items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public StrategicOptimizationCombinedResource[] Items { get; set; }
    }
}
