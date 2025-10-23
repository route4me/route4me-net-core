using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Response for location clustering endpoint
    /// </summary>
    [DataContract]
    public class LocationClusteringResource
    {
        [DataMember(Name = "clusters")]
        public LocationCluster[] Clusters { get; set; }

        [DataMember(Name = "locations")]
        public LocationClusterItem[] Locations { get; set; }

        [DataMember(Name = "with_clusters")]
        public bool? WithClusters { get; set; }
    }

    /// <summary>
    /// Container for cluster data
    /// </summary>
    [DataContract]
    public class LocationCluster
    {
        /// <summary>
        /// Cluster data with location and bounding box
        /// </summary>
        [DataMember(Name = "cluster")]
        public LocationClusterData Cluster { get; set; }
    }

    /// <summary>
    /// Cluster information with center coordinates and bounds
    /// </summary>
    [DataContract]
    public class LocationClusterData
    {
        /// <summary>
        /// Latitude of cluster center
        /// </summary>
        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude of cluster center
        /// </summary>
        [DataMember(Name = "lng")]
        public double? Lng { get; set; }

        /// <summary>
        /// Geographic bounding box containing all locations in this cluster
        /// </summary>
        [DataMember(Name = "bounding_box")]
        public LocationBoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Number of locations in this cluster
        /// </summary>
        [DataMember(Name = "locations_count")]
        public int? LocationsCount { get; set; }
    }

    /// <summary>
    /// Geographic bounding box defined by corner coordinates
    /// </summary>
    [DataContract]
    public class LocationBoundingBox
    {
        /// <summary>
        /// Top (northern) latitude boundary
        /// </summary>
        [DataMember(Name = "top")]
        public double? Top { get; set; }

        /// <summary>
        /// Left (western) longitude boundary
        /// </summary>
        [DataMember(Name = "left")]
        public double? Left { get; set; }

        /// <summary>
        /// Bottom (southern) latitude boundary
        /// </summary>
        [DataMember(Name = "bottom")]
        public double? Bottom { get; set; }

        /// <summary>
        /// Right (eastern) longitude boundary
        /// </summary>
        [DataMember(Name = "right")]
        public double? Right { get; set; }
    }

    /// <summary>
    /// Individual location within a cluster
    /// </summary>
    [DataContract]
    public class LocationClusterItem
    {
        /// <summary>
        /// Unique identifier for the address
        /// </summary>
        [DataMember(Name = "address_id")]
        public int? AddressId { get; set; }

        /// <summary>
        /// Latitude coordinate
        /// </summary>
        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude coordinate
        /// </summary>
        [DataMember(Name = "lng")]
        public double? Lng { get; set; }

        /// <summary>
        /// Primary address line
        /// </summary>
        [DataMember(Name = "address_1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Custom alias or label for the address
        /// </summary>
        [DataMember(Name = "address_alias")]
        public string AddressAlias { get; set; }
    }
}
