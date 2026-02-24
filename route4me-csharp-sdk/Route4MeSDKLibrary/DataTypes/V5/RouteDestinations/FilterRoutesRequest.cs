using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Route-level filter criteria that can be nested inside <see cref="GetDestinationsRequest.Route"/>.
    /// Mirrors the <c>FilterRoutesRequest</c> schema from the route-destinations API.
    /// </summary>
    [DataContract]
    public class FilterRoutesRequest
    {
        /// <summary>Free-text search query across route fields.</summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>Free-text search query (alias for <see cref="Query"/>).</summary>
        [DataMember(Name = "search_query")]
        public string SearchQuery { get; set; }

        /// <summary>Nested route field filters.</summary>
        [DataMember(Name = "filters")]
        public FilterRoutesFilters Filters { get; set; }

        /// <summary>Route IDs to exclude from results.</summary>
        [DataMember(Name = "exclude_ids")]
        public string[] ExcludeIds { get; set; }

        /// <summary>Timezone to apply for date-based filters.</summary>
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }

        /// <summary>Field to group results by.</summary>
        [DataMember(Name = "group_by")]
        public string GroupBy { get; set; }

        /// <summary>Only return routes that have a value for these fields.</summary>
        [DataMember(Name = "exist_fields")]
        public string[] ExistFields { get; set; }
    }

    /// <summary>
    /// Route field filters used within <see cref="FilterRoutesRequest"/>.
    /// </summary>
    [DataContract]
    public class FilterRoutesFilters
    {
        [DataMember(Name = "route_id")]
        public object RouteId { get; set; }

        [DataMember(Name = "vehicle_id")]
        public object VehicleId { get; set; }

        [DataMember(Name = "member_id")]
        public int? MemberId { get; set; }

        [DataMember(Name = "contact_id")]
        public int? ContactId { get; set; }

        [DataMember(Name = "vehicle_ids")]
        public string[] VehicleIds { get; set; }

        [DataMember(Name = "member_ids")]
        public int[] MemberIds { get; set; }

        [DataMember(Name = "depot_address_id")]
        public int? DepotAddressId { get; set; }

        [DataMember(Name = "optimization_problem_id")]
        public object OptimizationProblemId { get; set; }

        [DataMember(Name = "optimization_problem_ids")]
        public string[] OptimizationProblemIds { get; set; }

        [DataMember(Name = "customer_id")]
        public object CustomerId { get; set; }

        [DataMember(Name = "relation_customer_id")]
        public object RelationCustomerId { get; set; }

        [DataMember(Name = "contract_id")]
        public object ContractId { get; set; }

        [DataMember(Name = "route_tx_source")]
        public string RouteTxSource { get; set; }

        /// <summary>Filter by scheduled date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "schedule_date")]
        public string[] ScheduleDate { get; set; }

        /// <summary>Filter by route creation date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "created_date")]
        public string[] CreatedDate { get; set; }

        /// <summary>Filter by dispatch date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "dispatched_date")]
        public string[] DispatchedDate { get; set; }

        [DataMember(Name = "route_status")]
        public string[] RouteStatus { get; set; }

        [DataMember(Name = "route_name")]
        public string RouteName { get; set; }

        /// <summary>Filter by creation Unix timestamp range [min, max].</summary>
        [DataMember(Name = "created_timestamp")]
        public int[] CreatedTimestamp { get; set; }

        [DataMember(Name = "approved_to_execute")]
        public bool? ApprovedToExecute { get; set; }

        [DataMember(Name = "approved_to_execute_date")]
        public int[] ApprovedToExecuteDate { get; set; }

        [DataMember(Name = "visited_count")]
        public int[] VisitedCount { get; set; }

        [DataMember(Name = "notes_count")]
        public int[] NotesCount { get; set; }

        [DataMember(Name = "note_count")]
        public int[] NoteCount { get; set; }

        [DataMember(Name = "actual_travel_distance")]
        public double[] ActualTravelDistance { get; set; }

        [DataMember(Name = "actual_travel_time")]
        public int[] ActualTravelTime { get; set; }

        [DataMember(Name = "route_duration")]
        public int[] RouteDuration { get; set; }

        [DataMember(Name = "planned_total_route_duration")]
        public int[] PlannedTotalRouteDuration { get; set; }

        [DataMember(Name = "is_roundtrip")]
        public bool? IsRoundtrip { get; set; }

        [DataMember(Name = "is_unrouted")]
        public bool? IsUnrouted { get; set; }

        [DataMember(Name = "smart_optimization_id")]
        public object SmartOptimizationId { get; set; }

        [DataMember(Name = "is_master")]
        public bool? IsMaster { get; set; }

        [DataMember(Name = "is_last_locked")]
        public bool? IsLastLocked { get; set; }

        [DataMember(Name = "algorithm_type")]
        public string AlgorithmType { get; set; }

        [DataMember(Name = "master_route_id")]
        public object MasterRouteId { get; set; }

        [DataMember(Name = "route_start_time")]
        public int[] RouteStartTime { get; set; }

        [DataMember(Name = "route_end_time")]
        public int[] RouteEndTime { get; set; }

        [DataMember(Name = "original_route_id")]
        public object OriginalRouteId { get; set; }

        [DataMember(Name = "strategic_optimization_id")]
        public object StrategicOptimizationId { get; set; }

        [DataMember(Name = "scenario_id")]
        public object ScenarioId { get; set; }

        [DataMember(Name = "strategic_draft_route_id")]
        public object StrategicDraftRouteId { get; set; }

        [DataMember(Name = "root_member_id")]
        public int? RootMemberId { get; set; }

        [DataMember(Name = "destination_count")]
        public double[] DestinationCount { get; set; }

        [DataMember(Name = "trip_distance")]
        public double[] TripDistance { get; set; }

        [DataMember(Name = "route_progress")]
        public double[] RouteProgress { get; set; }

        [DataMember(Name = "revenue")]
        public double[] Revenue { get; set; }

        [DataMember(Name = "weight")]
        public double[] Weight { get; set; }

        [DataMember(Name = "planned_end_date")]
        public string[] PlannedEndDate { get; set; }

        [DataMember(Name = "master_route_source")]
        public object MasterRouteSource { get; set; }
    }
}
