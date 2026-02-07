using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of scenarios with combined data
    /// </summary>
    [DataContract]
    public sealed class ScenarioCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public ScenarioCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for scenario combined results
    /// </summary>
    [DataContract]
    public sealed class ScenarioCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of scenario items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public ScenarioCombinedResource[] Items { get; set; }
    }
}
