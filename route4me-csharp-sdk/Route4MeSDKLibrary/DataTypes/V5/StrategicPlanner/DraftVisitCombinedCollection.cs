using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of draft visits with combined data
    /// </summary>
    [DataContract]
    public sealed class DraftVisitCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DraftVisitCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for draft visit combined results
    /// </summary>
    [DataContract]
    public sealed class DraftVisitCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of draft visit items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public DraftVisitResource[] Items { get; set; }
    }
}
