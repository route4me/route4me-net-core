using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of draft destinations with combined data
    /// </summary>
    [DataContract]
    public sealed class DraftDestinationCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DraftDestinationCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for draft destination combined results
    /// </summary>
    [DataContract]
    public sealed class DraftDestinationCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of draft destination items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public DraftDestinationResource[] Items { get; set; }
    }
}
