using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Paginated collection of draft routes with combined data
    /// </summary>
    [DataContract]
    public sealed class DraftRouteCombinedCollection
    {
        /// <summary>
        /// Data containing items and pagination metadata
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DraftRouteCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for columns and display
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public CombinedConfigCollection Config { get; set; }
    }

    /// <summary>
    /// Data wrapper for draft route combined results
    /// </summary>
    [DataContract]
    public sealed class DraftRouteCombinedData : CombinedDataCollection
    {
        /// <summary>
        /// Array of draft route items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public DraftRouteResource[] Items { get; set; }
    }
}
