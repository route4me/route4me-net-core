using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Top-level response for POST /route-destinations/list/combined.
    /// Contains paginated data and column display configuration.
    /// </summary>
    [DataContract]
    public class RouteDestinationsCombinedResource
    {
        /// <summary>Combined destination data including items, pagination and grouping.</summary>
        [DataMember(Name = "data")]
        public RouteDestinationsCombinedData Data { get; set; }

        /// <summary>Column configuration for display customization.</summary>
        [DataMember(Name = "config")]
        public RouteDestinationsCombinedConfig Config { get; set; }
    }

    /// <summary>
    /// Combined data payload for the route destinations combined list response.
    /// </summary>
    [DataContract]
    public class RouteDestinationsCombinedData
    {
        /// <summary>Array of route destination items.</summary>
        [DataMember(Name = "items")]
        public RouteDestinationResource[] Items { get; set; }

        /// <summary>Zero-based index of the current page.</summary>
        [DataMember(Name = "current_page_index")]
        public int? CurrentPageIndex { get; set; }

        /// <summary>Total number of items across all pages.</summary>
        [DataMember(Name = "total_items_count")]
        public int? TotalItemsCount { get; set; }

        /// <summary>Group subtotals keyed by group value, if group_by is specified.</summary>
        [DataMember(Name = "groups_subtotals")]
        public object[] GroupsSubtotals { get; set; }

        /// <summary>Field used for grouping results, if any.</summary>
        [DataMember(Name = "group_by")]
        public string GroupBy { get; set; }

        /// <summary>Active sort criteria as an array of [field, direction] pairs.</summary>
        [DataMember(Name = "current_sorting")]
        public string[][] CurrentSorting { get; set; }

        /// <summary>Active filter data as a key-to-values map.</summary>
        [DataMember(Name = "current_filter_data")]
        public Dictionary<string, object> CurrentFilterData { get; set; }

        /// <summary>Aggregate totals for numeric fields.</summary>
        [DataMember(Name = "totals")]
        public object Totals { get; set; }
    }

    /// <summary>
    /// Column configuration returned in the combined destinations response.
    /// </summary>
    [DataContract]
    public class RouteDestinationsCombinedConfig
    {
        /// <summary>Column definitions controlling display and sort behaviour.</summary>
        [DataMember(Name = "columns")]
        public RouteDestinationColumnDefinition[] Columns { get; set; }

        /// <summary>Unique key identifying the saved column configuration.</summary>
        [DataMember(Name = "columns_configurator_key")]
        public string ColumnsConfiguratorKey { get; set; }

        /// <summary>API route names for reading and editing the column configuration.</summary>
        [DataMember(Name = "columns_configurator_route_names")]
        public RouteDestinationColumnsRouteNames ColumnsConfiguratorRouteNames { get; set; }
    }

    /// <summary>
    /// A single column definition used in combined and columns config responses.
    /// </summary>
    [DataContract]
    public class RouteDestinationColumnDefinition
    {
        /// <summary>Whether the column occupies full width.</summary>
        [DataMember(Name = "full_width")]
        public bool? FullWidth { get; set; }

        /// <summary>Whether the column supports sorting.</summary>
        [DataMember(Name = "sortable")]
        public bool? Sortable { get; set; }

        /// <summary>Column identifier (maps to a field name).</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>Cell rendering template string.</summary>
        [DataMember(Name = "cell_config")]
        public string CellConfig { get; set; }

        /// <summary>Human-readable column label.</summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }
    }

    /// <summary>
    /// Route name pair for the columns configurator endpoints.
    /// </summary>
    [DataContract]
    public class RouteDestinationColumnsRouteNames
    {
        /// <summary>Route name for the GET columns endpoint.</summary>
        [DataMember(Name = "get")]
        public string Get { get; set; }

        /// <summary>Route name for the PUT columns endpoint.</summary>
        [DataMember(Name = "edit")]
        public string Edit { get; set; }
    }
}
