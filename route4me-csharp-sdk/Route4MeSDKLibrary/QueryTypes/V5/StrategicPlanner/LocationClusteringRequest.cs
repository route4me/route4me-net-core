using System.Runtime.Serialization;

using Route4MeSDK.DataTypes.V5.StrategicPlanner;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for location clustering
    /// </summary>
    [DataContract]
    public sealed class LocationClusteringRequest : ListLocationsRequest
    {
        /// <summary>
        /// Clustering parameters
        /// </summary>
        [DataMember(Name = "clustering", EmitDefaultValue = false)]
        public ClusteringParameters Clustering { get; set; }

        /// <summary>
        /// Whether to return clusters
        /// </summary>
        [DataMember(Name = "with_clusters", EmitDefaultValue = false)]
        public bool? WithClusters { get; set; }
    }

    /// <summary>
    /// Clustering parameters
    /// </summary>
    [DataContract]
    public sealed class ClusteringParameters
    {
        /// <summary>
        /// Zoom level (0-22)
        /// </summary>
        [DataMember(Name = "zoom", EmitDefaultValue = false)]
        public int? Zoom { get; set; }
    }

    /// <summary>
    /// Request for location heatmap
    /// </summary>
    [DataContract]
    public sealed class LocationHeatmapRequest : ListLocationsRequest
    {
        /// <summary>
        /// Zoom level (0-22)
        /// </summary>
        [DataMember(Name = "zoom", EmitDefaultValue = false)]
        public int? Zoom { get; set; }
    }
}
