using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Response for location heatmap endpoint
    /// </summary>
    [DataContract]
    public class LocationHeatmapCollection
    {
        /// <summary>
        /// Array of heatmap data points [lat, lng, intensity]
        /// </summary>
        [DataMember(Name = "data")]
        public double[][] Data { get; set; }

        /// <summary>
        /// Field names for the data array
        /// </summary>
        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }
    }
}