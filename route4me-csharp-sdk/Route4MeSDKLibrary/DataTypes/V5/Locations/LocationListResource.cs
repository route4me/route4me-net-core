using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Response for location list endpoint
    /// Contains location items and total count
    /// </summary>
    [DataContract]
    public class LocationListResource
    {
        /// <summary>
        /// Array of location items
        /// </summary>
        [DataMember(Name = "items")]
        public LocationListItem[] Items { get; set; }

        /// <summary>
        /// Total number of items matching the query
        /// </summary>
        [DataMember(Name = "total_items_count")]
        public int? TotalItemsCount { get; set; }
    }

    /// <summary>
    /// Individual location item with basic address information
    /// </summary>
    [DataContract]
    public class LocationListItem
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