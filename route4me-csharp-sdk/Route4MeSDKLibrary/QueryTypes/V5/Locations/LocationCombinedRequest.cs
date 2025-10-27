using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for combined location list
    /// Supports filtering, pagination, sorting, and field selection
    /// </summary>
    [DataContract]
    public class LocationCombinedRequest : GenericParameters
    {
        /// <summary>
        /// Filter criteria for locations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public LocationFilters Filters { get; set; }

        /// <summary>
        /// Geographic areas to filter by
        /// </summary>
        [DataMember(Name = "selected_areas", EmitDefaultValue = false)]
        public object SelectedAreas { get; set; }

        /// <summary>
        /// Sort order as array of [field, direction] pairs (e.g., [["name", "asc"]])
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public string[][] OrderBy { get; set; }

        /// <summary>
        /// Page number (1-based)
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        /// Field name to group results by
        /// </summary>
        [DataMember(Name = "group_by", EmitDefaultValue = false)]
        public string GroupBy { get; set; }

        /// <summary>
        /// Array of field names to include in the response
        /// </summary>
        [DataMember(Name = "selected_fields", EmitDefaultValue = false)]
        public string[] SelectedFields { get; set; }
    }

    /// <summary>
    /// Filter criteria for location queries
    /// Provides comprehensive filtering options including search, geographic boundaries, and metadata
    /// </summary>
    [DataContract]
    public class LocationFilters
    {
        /// <summary>
        /// Text search query for location fields
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// View mode for location display
        /// </summary>
        [DataMember(Name = "view_mode", EmitDefaultValue = false)]
        public string ViewMode { get; set; }

        [DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
        public string[] AddressStopType { get; set; }

        [DataMember(Name = "address_id", EmitDefaultValue = false)]
        public int[] AddressId { get; set; }

        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int[] RootMemberId { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int[] MemberId { get; set; }

        [DataMember(Name = "external_connection_id", EmitDefaultValue = false)]
        public int[] ExternalConnectionId { get; set; }

        [DataMember(Name = "external_vendor_id", EmitDefaultValue = false)]
        public int[] ExternalVendorId { get; set; }

        [DataMember(Name = "external_object_id", EmitDefaultValue = false)]
        public string[] ExternalObjectId { get; set; }

        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public string[] CustomerId { get; set; }

        [DataMember(Name = "assigned_to", EmitDefaultValue = false)]
        public int[] AssignedTo { get; set; }

        [DataMember(Name = "address_cube", EmitDefaultValue = false)]
        public int[] AddressCube { get; set; }

        [DataMember(Name = "address_pieces", EmitDefaultValue = false)]
        public int[] AddressPieces { get; set; }

        [DataMember(Name = "address_priority", EmitDefaultValue = false)]
        public int[] AddressPriority { get; set; }

        [DataMember(Name = "address_revenue", EmitDefaultValue = false)]
        public int[] AddressRevenue { get; set; }

        [DataMember(Name = "address_weight", EmitDefaultValue = false)]
        public int[] AddressWeight { get; set; }

        [DataMember(Name = "in_route_count", EmitDefaultValue = false)]
        public int[] InRouteCount { get; set; }

        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public int[] ServiceTime { get; set; }

        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int[] VisitedCount { get; set; }

        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool[] IsDepot { get; set; }

        [DataMember(Name = "is_pickup", EmitDefaultValue = false)]
        public bool[] IsPickup { get; set; }

        [DataMember(Name = "is_assigned", EmitDefaultValue = false)]
        public bool[] IsAssigned { get; set; }

        [DataMember(Name = "is_external", EmitDefaultValue = false)]
        public bool[] IsExternal { get; set; }

        [DataMember(Name = "parent_location_id", EmitDefaultValue = false)]
        public int[] ParentLocationId { get; set; }

        [DataMember(Name = "location_type_id", EmitDefaultValue = false)]
        public string[] LocationTypeId { get; set; }

        [DataMember(Name = "display", EmitDefaultValue = false)]
        public string[] Display { get; set; }

        [DataMember(Name = "territories", EmitDefaultValue = false)]
        public string[] Territories { get; set; }

        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string[] CreatedDate { get; set; }

        [DataMember(Name = "routed_date", EmitDefaultValue = false)]
        public string[] RoutedDate { get; set; }

        [DataMember(Name = "visited_date", EmitDefaultValue = false)]
        public string[] VisitedDate { get; set; }

        [DataMember(Name = "bounding_box", EmitDefaultValue = false)]
        public LocationBoundingBoxFilter BoundingBox { get; set; }

        [DataMember(Name = "selected_areas", EmitDefaultValue = false)]
        public LocationSelectedArea[] SelectedAreas { get; set; }

        [DataMember(Name = "excluded_ids", EmitDefaultValue = false)]
        public int[] ExcludedIds { get; set; }
    }

    /// <summary>
    /// Geographic bounding box filter for location queries
    /// </summary>
    [DataContract]
    public class LocationBoundingBoxFilter
    {
        /// <summary>
        /// Top (northern) latitude boundary
        /// </summary>
        [DataMember(Name = "top", EmitDefaultValue = false)]
        public double? Top { get; set; }

        /// <summary>
        /// Left (western) longitude boundary
        /// </summary>
        [DataMember(Name = "left", EmitDefaultValue = false)]
        public double? Left { get; set; }

        /// <summary>
        /// Bottom (southern) latitude boundary
        /// </summary>
        [DataMember(Name = "bottom", EmitDefaultValue = false)]
        public double? Bottom { get; set; }

        /// <summary>
        /// Right (eastern) longitude boundary
        /// </summary>
        [DataMember(Name = "right", EmitDefaultValue = false)]
        public double? Right { get; set; }
    }

    /// <summary>
    /// Geographic area selection (polygon, circle, etc.)
    /// </summary>
    [DataContract]
    public class LocationSelectedArea
    {
        /// <summary>
        /// Type of geographic area (e.g., "polygon", "circle")
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Coordinate array defining the area boundary
        /// </summary>
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public double[] Coordinates { get; set; }
    }

    /// <summary>
    /// Date range filter for time-based queries
    /// </summary>
    [DataContract]
    public class LocationDateRange
    {
        /// <summary>
        /// Start date (ISO 8601 format)
        /// </summary>
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        /// <summary>
        /// End date (ISO 8601 format)
        /// </summary>
        [DataMember(Name = "end", EmitDefaultValue = false)]
        public string End { get; set; }
    }
}
