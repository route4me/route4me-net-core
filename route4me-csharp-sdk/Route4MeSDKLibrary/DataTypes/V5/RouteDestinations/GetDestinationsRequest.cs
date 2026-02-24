using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Request body for POST /route-destinations/list and
    /// POST /route-destinations/list/combined.
    /// </summary>
    [DataContract]
    public class GetDestinationsRequest : GenericParameters
    {
        /// <summary>
        /// Destination-level filter criteria. At minimum the <c>filters</c> property must be present.
        /// </summary>
        [DataMember(Name = "filters")]
        public DestinationFilters Filters { get; set; }

        /// <summary>
        /// Sort criteria as an array of [fieldName, direction] pairs.
        /// Direction must be "asc" or "desc".
        /// Example: <c>[["arrival_time", "desc"], ["destination_alias", "asc"]]</c>
        /// </summary>
        [DataMember(Name = "order_by")]
        public string[][] OrderBy { get; set; }

        /// <summary>Page number (1-based). Defaults to 1.</summary>
        [DataMember(Name = "page")]
        public int? Page { get; set; }

        /// <summary>Number of items per page (1–500).</summary>
        [DataMember(Name = "per_page")]
        public int? PerPage { get; set; }

        /// <summary>Field name to group results by.</summary>
        [DataMember(Name = "group_by")]
        public string GroupBy { get; set; }

        /// <summary>IANA timezone identifier used for date-time display (e.g., "America/New_York").</summary>
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Subset of destination fields to include in the response.
        /// Omit to return all available fields.
        /// </summary>
        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }

        /// <summary>
        /// Additional route-level filters applied to narrow which routes'
        /// destinations are included in the result.
        /// </summary>
        [DataMember(Name = "route")]
        public FilterRoutesRequest Route { get; set; }
    }

    /// <summary>
    /// Destination-level filter criteria for <see cref="GetDestinationsRequest"/>.
    /// </summary>
    [DataContract]
    public class DestinationFilters
    {
        /// <summary>Free-text search across destination fields.</summary>
        [DataMember(Name = "search_query")]
        public string SearchQuery { get; set; }

        /// <summary>Filter by route ID (32-character hex string).</summary>
        [DataMember(Name = "route_id")]
        public string RouteId { get; set; }

        /// <summary>Filter by vehicle ID (32-character hex string).</summary>
        [DataMember(Name = "vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>Filter by driver/member ID.</summary>
        [DataMember(Name = "member_id")]
        public long? MemberId { get; set; }

        /// <summary>Filter by route creation date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "route_created_date")]
        public string[] RouteCreatedDate { get; set; }

        /// <summary>
        /// Filter by stop type. Valid values: PICKUP, DELIVERY, BREAK, MEETUP, SERVICE, VISIT, DRIVEBY.
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string[] AddressStopType { get; set; }

        /// <summary>Filter by stop status IDs.</summary>
        [DataMember(Name = "stop_status_id")]
        public string[] StopStatusId { get; set; }

        /// <summary>View mode selector (UI-specific).</summary>
        [DataMember(Name = "view_mode")]
        public string ViewMode { get; set; }

        /// <summary>Filter by customer purchase order number.</summary>
        [DataMember(Name = "customer_po")]
        public string CustomerPo { get; set; }

        /// <summary>Filter by optimization problem ID (32-character hex string).</summary>
        [DataMember(Name = "optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

        /// <summary>Filter by multiple optimization problem IDs.</summary>
        [DataMember(Name = "optimization_problem_ids")]
        public string[] OptimizationProblemIds { get; set; }

        /// <summary>Filter by customer ID (32-character hex string).</summary>
        [DataMember(Name = "customer_id")]
        public string CustomerId { get; set; }

        /// <summary>Local timezone identifier used for interpreting date-based filters.</summary>
        [DataMember(Name = "local_timezone")]
        public string LocalTimezone { get; set; }

        /// <summary>Filter by distance to next destination [min, max] (up to 2 values).</summary>
        [DataMember(Name = "distance_to_next_destination")]
        public double[] DistanceToNextDestination { get; set; }

        /// <summary>Filter by drive time to next destination in seconds [min, max] (up to 2 values).</summary>
        [DataMember(Name = "drive_time_to_next_destination")]
        public int[] DriveTimeToNextDestination { get; set; }

        /// <summary>Filter by on-site man-hours [min, max] (up to 2 values).</summary>
        [DataMember(Name = "man_hours")]
        public double[] ManHours { get; set; }

        /// <summary>Filter by customer billing type [min, max] (up to 2 values).</summary>
        [DataMember(Name = "customer_billing_type")]
        public int[] CustomerBillingType { get; set; }
    }
}
