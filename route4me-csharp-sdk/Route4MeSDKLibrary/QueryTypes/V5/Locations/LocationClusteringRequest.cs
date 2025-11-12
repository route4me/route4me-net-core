using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for location clustering
    /// Groups locations based on zoom level and geographic proximity
    /// </summary>
    [DataContract]
    public class LocationClusteringRequest : LocationCombinedRequest
    {
        /// <summary>
        /// Clustering configuration parameters
        /// </summary>
        [DataMember(Name = "clustering", EmitDefaultValue = false)]
        public ClusteringParameters Clustering { get; set; }

        /// <summary>
        /// Whether to include cluster information in the response
        /// </summary>
        [DataMember(Name = "with_clusters", EmitDefaultValue = false)]
        public bool? WithClusters { get; set; }
    }

    /// <summary>
    /// Parameters for location clustering algorithm
    /// </summary>
    [DataContract]
    public class ClusteringParameters
    {
        /// <summary>
        /// Zoom level for clustering (0-22, where higher values show more detail)
        /// </summary>
        [DataMember(Name = "zoom", EmitDefaultValue = false)]
        public int? Zoom { get; set; }
    }
}