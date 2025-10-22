using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Response for location territories endpoint
    /// Contains territories with location counts and pagination
    /// </summary>
    [DataContract]
    public class LocationTerritoriesCollection
    {
        /// <summary>
        /// Array of territory items
        /// </summary>
        [DataMember(Name = "items")]
        public LocationTerritoryResource[] Items { get; set; }

        /// <summary>
        /// Current page index (0-based)
        /// </summary>
        [DataMember(Name = "current_page_index")]
        public int? CurrentPageIndex { get; set; }

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        [DataMember(Name = "total_items_count")]
        public int? TotalItemsCount { get; set; }

        /// <summary>
        /// Current sorting configuration as array of [field, direction] pairs
        /// </summary>
        [DataMember(Name = "current_sorting")]
        public string[][] CurrentSorting { get; set; }

        /// <summary>
        /// Subtotal information for grouped data
        /// </summary>
        [DataMember(Name = "subtotals")]
        public LocationTerritorySubtotal[] Subtotals { get; set; }
    }

    /// <summary>
    /// Territory information with location count
    /// </summary>
    [DataContract]
    public class LocationTerritoryResource
    {
        /// <summary>
        /// Unique identifier for the territory
        /// </summary>
        [DataMember(Name = "territory_id")]
        public string TerritoryId { get; set; }

        /// <summary>
        /// Display name of the territory
        /// </summary>
        [DataMember(Name = "territory_name")]
        public string TerritoryName { get; set; }

        /// <summary>
        /// Color code for territory display (hex format)
        /// </summary>
        [DataMember(Name = "territory_color")]
        public string TerritoryColor { get; set; }

        /// <summary>
        /// Member ID associated with the territory
        /// </summary>
        [DataMember(Name = "member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        /// Number of locations within this territory
        /// </summary>
        [DataMember(Name = "locations_count")]
        public int? LocationsCount { get; set; }
    }

    /// <summary>
    /// Subtotal configuration for grouped territory data
    /// </summary>
    [DataContract]
    public class LocationTerritorySubtotal
    {
        /// <summary>
        /// Display label for the subtotal
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Field name used for subtotal calculation
        /// </summary>
        [DataMember(Name = "field")]
        public string Field { get; set; }
    }
}
