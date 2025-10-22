using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for location heatmap
    /// Generates heatmap data for visualizing location density
    /// </summary>
    [DataContract]
    public class LocationHeatmapRequest : LocationCombinedRequest
    {
        /// <summary>
        /// Zoom level for heatmap granularity (0-22, where higher values show more detail)
        /// </summary>
        [DataMember(Name = "zoom", EmitDefaultValue = false)]
        public int? Zoom { get; set; }
    }
}
