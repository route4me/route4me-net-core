using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft location clustering response
    /// </summary>
    [DataContract]
    public sealed class DraftLocationClustering
    {
        /// <summary>
        /// Array of clusters
        /// </summary>
        [DataMember(Name = "clusters", EmitDefaultValue = false)]
        public ClusterItem[] Clusters { get; set; }

        /// <summary>
        /// Array of individual locations
        /// </summary>
        [DataMember(Name = "locations", EmitDefaultValue = false)]
        public LocationClusterPoint[] Locations { get; set; }

        /// <summary>
        /// Whether clusters were requested
        /// </summary>
        [DataMember(Name = "with_clusters", EmitDefaultValue = false)]
        public bool? WithClusters { get; set; }
    }

    /// <summary>
    /// Cluster item
    /// </summary>
    [DataContract]
    public sealed class ClusterItem
    {
        /// <summary>
        /// Cluster details
        /// </summary>
        [DataMember(Name = "cluster", EmitDefaultValue = false)]
        public ClusterDetails Cluster { get; set; }
    }

    /// <summary>
    /// Cluster details
    /// </summary>
    [DataContract]
    public sealed class ClusterDetails
    {
        /// <summary>
        /// Cluster center latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Cluster center longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }

        /// <summary>
        /// Bounding box of the cluster
        /// </summary>
        [DataMember(Name = "bounding_box", EmitDefaultValue = false)]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Number of locations in the cluster
        /// </summary>
        [DataMember(Name = "locations_count", EmitDefaultValue = false)]
        public int? LocationsCount { get; set; }
    }

    /// <summary>
    /// Bounding box coordinates
    /// </summary>
    [DataContract]
    public sealed class BoundingBox
    {
        /// <summary>
        /// Top latitude
        /// </summary>
        [DataMember(Name = "top", EmitDefaultValue = false)]
        public double? Top { get; set; }

        /// <summary>
        /// Left longitude
        /// </summary>
        [DataMember(Name = "left", EmitDefaultValue = false)]
        public double? Left { get; set; }

        /// <summary>
        /// Bottom latitude
        /// </summary>
        [DataMember(Name = "bottom", EmitDefaultValue = false)]
        public double? Bottom { get; set; }

        /// <summary>
        /// Right longitude
        /// </summary>
        [DataMember(Name = "right", EmitDefaultValue = false)]
        public double? Right { get; set; }
    }

    /// <summary>
    /// Location cluster point
    /// </summary>
    [DataContract]
    public sealed class LocationClusterPoint
    {
        /// <summary>
        /// Address ID
        /// </summary>
        [DataMember(Name = "address_id", EmitDefaultValue = false)]
        public int? AddressId { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }

        /// <summary>
        /// Address line 1
        /// </summary>
        [DataMember(Name = "address_1", EmitDefaultValue = false)]
        public string Address1 { get; set; }

        /// <summary>
        /// Address alias
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string AddressAlias { get; set; }
    }
}
