using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Cluster
    /// </summary>
    [DataContract]
    public class Cluster
    {
        /// <summary>
        ///     Get hash
        /// </summary>
        [DataMember(Name = "geohash", EmitDefaultValue = false)]
        public string GeoHash { get; set; }

        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Lat { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Lng { get; set; }

        /// <summary>
        ///     Boundary box.
        /// </summary>
        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public List<List<double>> BoundaryBox { get; set; }

    }

    /// <summary>
    /// Cluster
    /// </summary>
    [DataContract]
    public class ClusterItem
    {
        /// <summary>
        ///     Get hash
        /// </summary>
        [DataMember(Name = "cluster", EmitDefaultValue = false)]
        public Cluster Cluster { get; set; }

        /// <summary>
        ///    A number of the returned addresses.
        /// </summary>
        [DataMember(Name = "address_count", EmitDefaultValue = false)]
        public double AddressCount { get; set; }
    }
}