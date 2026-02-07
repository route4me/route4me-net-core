using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of draft locations with combined data
    /// </summary>
    [DataContract]
    public sealed class DraftLocationCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DraftLocationCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for draft location combined results
    /// </summary>
    [DataContract]
    public sealed class DraftLocationCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of draft location items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public DraftLocationCombinedResource[] Items { get; set; }
    }
}
