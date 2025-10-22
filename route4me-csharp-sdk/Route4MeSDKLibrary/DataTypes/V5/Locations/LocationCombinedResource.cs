using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Combined resource based on user config
    /// Contains both location data and configuration for display
    /// </summary>
    [DataContract]
    public class LocationCombinedResource
    {
        /// <summary>
        /// Location data with items, pagination, and grouping information
        /// </summary>
        [DataMember(Name = "data")]
        public LocationCombinedData Data { get; set; }

        /// <summary>
        /// Configuration for column display and customization
        /// </summary>
        [DataMember(Name = "config")]
        public LocationConfig Config { get; set; }
    }

    /// <summary>
    /// Configuration for location display settings
    /// </summary>
    [DataContract]
    public class LocationConfig
    {
        /// <summary>
        /// Array of column definitions for display
        /// </summary>
        [DataMember(Name = "columns")]
        public LocationColumn[] Columns { get; set; }

        /// <summary>
        /// Key identifying the column configuration
        /// </summary>
        [DataMember(Name = "columns_configurator_key")]
        public string ColumnsConfiguratorKey { get; set; }

        /// <summary>
        /// Route names for column configurator endpoints
        /// </summary>
        [DataMember(Name = "columns_configurator_route_names")]
        public LocationColumnsConfiguratorRouteNames ColumnsConfiguratorRouteNames { get; set; }
    }

    /// <summary>
    /// Combined location data with items, pagination, filtering, and grouping
    /// </summary>
    [DataContract]
    public class LocationCombinedData
    {
        /// <summary>
        /// Array of location items
        /// </summary>
        [DataMember(Name = "items")]
        public LocationCombinedDataItem[] Items { get; set; }

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

        [DataMember(Name = "groups_subtotals")]
        public Dictionary<string, object> GroupsSubtotals { get; set; }

        [DataMember(Name = "extended_groups_subtotals")]
        public Dictionary<string, object> ExtendedGroupsSubtotals { get; set; }

        [DataMember(Name = "group_by")]
        public string GroupBy { get; set; }

        [DataMember(Name = "current_sorting")]
        public Dictionary<string, string> CurrentSorting { get; set; }

        [DataMember(Name = "current_filter_data")]
        public Dictionary<string, string> CurrentFilterData { get; set; }

        [DataMember(Name = "totals")]
        public Dictionary<string, string> Totals { get; set; }

        [DataMember(Name = "columns")]
        public LocationColumn[] Columns { get; set; }

        [DataMember(Name = "columns_configurator_key")]
        public string ColumnsConfiguratorKey { get; set; }

        [DataMember(Name = "columns_configurator_route_names")]
        public LocationColumnsConfiguratorRouteNames ColumnsConfiguratorRouteNames { get; set; }
    }

    /// <summary>
    /// Column definition for location display
    /// </summary>
    [DataContract]
    public class LocationColumn
    {
        /// <summary>
        /// Indicates if column should take full width
        /// </summary>
        [DataMember(Name = "full_width")]
        public bool? FullWidth { get; set; }

        /// <summary>
        /// Indicates if column is sortable
        /// </summary>
        [DataMember(Name = "sortable")]
        public bool? Sortable { get; set; }

        /// <summary>
        /// Unique identifier for the column
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Configuration string for cell rendering
        /// </summary>
        [DataMember(Name = "cell_config")]
        public string CellConfig { get; set; }

        /// <summary>
        /// Display label for the column
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }
    }

    /// <summary>
    /// Route names for column configurator API endpoints
    /// </summary>
    [DataContract]
    public class LocationColumnsConfiguratorRouteNames
    {
        /// <summary>
        /// Route name for GET endpoint
        /// </summary>
        [DataMember(Name = "get")]
        public string Get { get; set; }

        /// <summary>
        /// Route name for EDIT endpoint
        /// </summary>
        [DataMember(Name = "edit")]
        public string Edit { get; set; }
    }

    /// <summary>
    /// Individual location item with address details
    /// </summary>
    [DataContract]
    public class LocationCombinedDataItem
    {
        /// <summary>
        /// Unique identifier for the address
        /// </summary>
        [DataMember(Name = "address_id")]
        public int? AddressId { get; set; }

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
        /// Name of external connection (e.g., third-party integration)
        /// </summary>
        [DataMember(Name = "external_connection_name")]
        public string ExternalConnectionName { get; set; }

        /// <summary>
        /// Token for external connection authentication
        /// </summary>
        [DataMember(Name = "external_connection_token")]
        public string ExternalConnectionToken { get; set; }
    }
}
