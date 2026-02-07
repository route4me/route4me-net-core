using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft location heatmap data collection
    /// </summary>
    [DataContract]
    public sealed class DraftLocationHeatmapCollection
    {
        /// <summary>
        /// Array of heatmap data points [lat, lng, intensity]
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public double[][] Data { get; set; }

        /// <summary>
        /// Field names for the data array
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}
