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
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>Free-text search query (alias for <see cref="Query"/>).</summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>Nested route field filters.</summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public FilterRoutesFilters Filters { get; set; }

        /// <summary>Route IDs to exclude from results.</summary>
        [DataMember(Name = "exclude_ids", EmitDefaultValue = false)]
        public string[] ExcludeIds { get; set; }

        /// <summary>Timezone to apply for date-based filters.</summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>Field to group results by.</summary>
        [DataMember(Name = "group_by", EmitDefaultValue = false)]
        public string GroupBy { get; set; }

        /// <summary>Only return routes that have a value for these fields.</summary>
        [DataMember(Name = "exist_fields", EmitDefaultValue = false)]
        public string[] ExistFields { get; set; }
    }

    /// <summary>
    /// Route field filters used within <see cref="FilterRoutesRequest"/>.
    /// </summary>
    [DataContract]
    public class FilterRoutesFilters
    {
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public object RouteId { get; set; }

        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public object VehicleId { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public int? ContactId { get; set; }

        [DataMember(Name = "vehicle_ids", EmitDefaultValue = false)]
        public string[] VehicleIds { get; set; }

        [DataMember(Name = "member_ids", EmitDefaultValue = false)]
        public int[] MemberIds { get; set; }

        [DataMember(Name = "depot_address_id", EmitDefaultValue = false)]
        public int? DepotAddressId { get; set; }

        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public object OptimizationProblemId { get; set; }

        [DataMember(Name = "optimization_problem_ids", EmitDefaultValue = false)]
        public string[] OptimizationProblemIds { get; set; }

        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public object CustomerId { get; set; }

        [DataMember(Name = "relation_customer_id", EmitDefaultValue = false)]
        public object RelationCustomerId { get; set; }

        [DataMember(Name = "contract_id", EmitDefaultValue = false)]
        public object ContractId { get; set; }

        [DataMember(Name = "route_tx_source", EmitDefaultValue = false)]
        public string RouteTxSource { get; set; }

        /// <summary>Filter by scheduled date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "schedule_date", EmitDefaultValue = false)]
        public string[] ScheduleDate { get; set; }

        /// <summary>Filter by route creation date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string[] CreatedDate { get; set; }

        /// <summary>Filter by dispatch date range [from, to] in "YYYY-MM-DD" format.</summary>
        [DataMember(Name = "dispatched_date", EmitDefaultValue = false)]
        public string[] DispatchedDate { get; set; }

        [DataMember(Name = "route_status", EmitDefaultValue = false)]
        public string[] RouteStatus { get; set; }

        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>Filter by creation Unix timestamp range [min, max].</summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public int[] CreatedTimestamp { get; set; }

        [DataMember(Name = "approved_to_execute", EmitDefaultValue = false)]
        public bool? ApprovedToExecute { get; set; }

        [DataMember(Name = "approved_to_execute_date", EmitDefaultValue = false)]
        public int[] ApprovedToExecuteDate { get; set; }

        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int[] VisitedCount { get; set; }

        [DataMember(Name = "notes_count", EmitDefaultValue = false)]
        public int[] NotesCount { get; set; }

        [DataMember(Name = "note_count", EmitDefaultValue = false)]
        public int[] NoteCount { get; set; }

        [DataMember(Name = "actual_travel_distance", EmitDefaultValue = false)]
        public double[] ActualTravelDistance { get; set; }

        [DataMember(Name = "actual_travel_time", EmitDefaultValue = false)]
        public int[] ActualTravelTime { get; set; }

        [DataMember(Name = "route_duration", EmitDefaultValue = false)]
        public int[] RouteDuration { get; set; }

        [DataMember(Name = "planned_total_route_duration", EmitDefaultValue = false)]
        public int[] PlannedTotalRouteDuration { get; set; }

        [DataMember(Name = "is_roundtrip", EmitDefaultValue = false)]
        public bool? IsRoundtrip { get; set; }

        [DataMember(Name = "is_unrouted", EmitDefaultValue = false)]
        public bool? IsUnrouted { get; set; }

        [DataMember(Name = "smart_optimization_id", EmitDefaultValue = false)]
        public object SmartOptimizationId { get; set; }

        [DataMember(Name = "is_master", EmitDefaultValue = false)]
        public bool? IsMaster { get; set; }

        [DataMember(Name = "is_last_locked", EmitDefaultValue = false)]
        public bool? IsLastLocked { get; set; }

        [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
        public string AlgorithmType { get; set; }

        [DataMember(Name = "master_route_id", EmitDefaultValue = false)]
        public object MasterRouteId { get; set; }

        [DataMember(Name = "route_start_time", EmitDefaultValue = false)]
        public int[] RouteStartTime { get; set; }

        [DataMember(Name = "route_end_time", EmitDefaultValue = false)]
        public int[] RouteEndTime { get; set; }

        [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
        public object OriginalRouteId { get; set; }

        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public object StrategicOptimizationId { get; set; }

        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public object ScenarioId { get; set; }

        [DataMember(Name = "strategic_draft_route_id", EmitDefaultValue = false)]
        public object StrategicDraftRouteId { get; set; }

        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>Free-text search query (inside filters).</summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>Free-text search query alias (inside filters).</summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        [DataMember(Name = "destination_count", EmitDefaultValue = false)]
        public double[] DestinationCount { get; set; }

        [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
        public double[] TripDistance { get; set; }

        /// <summary>Filter by datatable trip distance [min, max].</summary>
        [DataMember(Name = "datatable_trip_distance", EmitDefaultValue = false)]
        public double[] DatatableTripDistance { get; set; }

        /// <summary>Filter by route duration in hours [min, max].</summary>
        [DataMember(Name = "route_duration_hours", EmitDefaultValue = false)]
        public double[] RouteDurationHours { get; set; }

        /// <summary>Filter by addresses deviation count [min, max].</summary>
        [DataMember(Name = "addresses_deviation_count", EmitDefaultValue = false)]
        public double[] AddressesDeviationCount { get; set; }

        /// <summary>Filter by actual average service time [min, max].</summary>
        [DataMember(Name = "actual_avg_service_time", EmitDefaultValue = false)]
        public double[] ActualAvgServiceTime { get; set; }

        /// <summary>Filter by projected average service time [min, max].</summary>
        [DataMember(Name = "projected_avg_service_time", EmitDefaultValue = false)]
        public double[] ProjectedAvgServiceTime { get; set; }

        /// <summary>Filter by number of destinations arrived late [min, max].</summary>
        [DataMember(Name = "destinations_arrived_late", EmitDefaultValue = false)]
        public double[] DestinationsArrivedLate { get; set; }

        /// <summary>Filter by number of destinations arrived early [min, max].</summary>
        [DataMember(Name = "destinations_arrived_early", EmitDefaultValue = false)]
        public double[] DestinationsArrivedEarly { get; set; }

        /// <summary>Filter by number of destinations arrived on time [min, max].</summary>
        [DataMember(Name = "destinations_arrived_on_time", EmitDefaultValue = false)]
        public double[] DestinationsArrivedOnTime { get; set; }

        /// <summary>Filter by driver open timestamp [min, max].</summary>
        [DataMember(Name = "driver_open_timestamp", EmitDefaultValue = false)]
        public double[] DriverOpenTimestamp { get; set; }

        /// <summary>Filter by planned end timestamp [min, max].</summary>
        [DataMember(Name = "planned_end_timestamp", EmitDefaultValue = false)]
        public double[] PlannedEndTimestamp { get; set; }

        /// <summary>Filter by route cost [min, max].</summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double[] Cost { get; set; }

        /// <summary>Filter by actual start timestamp [min, max].</summary>
        [DataMember(Name = "actual_start_timestamp", EmitDefaultValue = false)]
        public double[] ActualStartTimestamp { get; set; }

        /// <summary>Filter by mobile actual travel distance [min, max].</summary>
        [DataMember(Name = "mobile_actual_travel_distance", EmitDefaultValue = false)]
        public double[] MobileActualTravelDistance { get; set; }

        /// <summary>Filter by mobile actual travel timestamp [min, max].</summary>
        [DataMember(Name = "mobile_actual_travel_timestamp", EmitDefaultValue = false)]
        public double[] MobileActualTravelTimestamp { get; set; }

        /// <summary>Filter by telematics actual travel distance [min, max].</summary>
        [DataMember(Name = "telematics_actual_travel_distance", EmitDefaultValue = false)]
        public double[] TelematicsActualTravelDistance { get; set; }

        /// <summary>Filter by telematics actual travel timestamp [min, max].</summary>
        [DataMember(Name = "telematics_actual_travel_timestamp", EmitDefaultValue = false)]
        public double[] TelematicsActualTravelTimestamp { get; set; }

        /// <summary>Filter by number of failed destinations [min, max].</summary>
        [DataMember(Name = "failed_destinations", EmitDefaultValue = false)]
        public double[] FailedDestinations { get; set; }

        /// <summary>Filter by number of skipped destinations [min, max].</summary>
        [DataMember(Name = "skipped_destinations", EmitDefaultValue = false)]
        public double[] SkippedDestinations { get; set; }

        /// <summary>Filter by number of done destinations [min, max].</summary>
        [DataMember(Name = "done_destinations", EmitDefaultValue = false)]
        public double[] DoneDestinations { get; set; }

        /// <summary>Filter by number of planned destinations [min, max].</summary>
        [DataMember(Name = "planned_destinations", EmitDefaultValue = false)]
        public double[] PlannedDestinations { get; set; }

        [DataMember(Name = "route_progress", EmitDefaultValue = false)]
        public double[] RouteProgress { get; set; }

        /// <summary>Filter by datatable route progress percentage [min, max].</summary>
        [DataMember(Name = "datatable_route_progress", EmitDefaultValue = false)]
        public double[] DatatableRouteProgress { get; set; }

        /// <summary>Filter by actual weight load percent [min, max].</summary>
        [DataMember(Name = "actual_weight_load_percent", EmitDefaultValue = false)]
        public double[] ActualWeightLoadPercent { get; set; }

        /// <summary>Filter by planned weight load percent [min, max].</summary>
        [DataMember(Name = "planned_weight_load_percent", EmitDefaultValue = false)]
        public double[] PlannedWeightLoadPercent { get; set; }

        /// <summary>Filter by actual cube load percent [min, max].</summary>
        [DataMember(Name = "actual_cube_load_percent", EmitDefaultValue = false)]
        public double[] ActualCubeLoadPercent { get; set; }

        /// <summary>Filter by planned cube load percent [min, max].</summary>
        [DataMember(Name = "planned_cube_load_percent", EmitDefaultValue = false)]
        public double[] PlannedCubeLoadPercent { get; set; }

        /// <summary>Filter by actual revenue load percent [min, max].</summary>
        [DataMember(Name = "actual_revenue_load_percent", EmitDefaultValue = false)]
        public double[] ActualRevenueLoadPercent { get; set; }

        /// <summary>Filter by planned revenue load percent [min, max].</summary>
        [DataMember(Name = "planned_revenue_load_percent", EmitDefaultValue = false)]
        public double[] PlannedRevenueLoadPercent { get; set; }

        /// <summary>Filter by actual pieces load percent [min, max].</summary>
        [DataMember(Name = "actual_pieces_load_percent", EmitDefaultValue = false)]
        public double[] ActualPiecesLoadPercent { get; set; }

        /// <summary>Filter by planned pieces load percent [min, max].</summary>
        [DataMember(Name = "planned_pieces_load_percent", EmitDefaultValue = false)]
        public double[] PlannedPiecesLoadPercent { get; set; }

        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double[] Revenue { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double[] Weight { get; set; }

        /// <summary>Filter by cube capacity [min, max].</summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double[] Cube { get; set; }

        /// <summary>Filter by pieces count [min, max].</summary>
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public double[] Pieces { get; set; }

        /// <summary>Filter by actual revenue [min, max].</summary>
        [DataMember(Name = "actual_revenue", EmitDefaultValue = false)]
        public double[] ActualRevenue { get; set; }

        /// <summary>Filter by actual weight [min, max].</summary>
        [DataMember(Name = "actual_weight", EmitDefaultValue = false)]
        public double[] ActualWeight { get; set; }

        /// <summary>Filter by actual cube [min, max].</summary>
        [DataMember(Name = "actual_cube", EmitDefaultValue = false)]
        public double[] ActualCube { get; set; }

        /// <summary>Filter by actual pieces [min, max].</summary>
        [DataMember(Name = "actual_pieces", EmitDefaultValue = false)]
        public double[] ActualPieces { get; set; }

        [DataMember(Name = "planned_end_date", EmitDefaultValue = false)]
        public string[] PlannedEndDate { get; set; }

        /// <summary>Filter by planned total wait hours [min, max].</summary>
        [DataMember(Name = "planned_total_wait_hours", EmitDefaultValue = false)]
        public double[] PlannedTotalWaitHours { get; set; }

        /// <summary>Filter by planned total service hours [min, max].</summary>
        [DataMember(Name = "planned_total_service_hours", EmitDefaultValue = false)]
        public double[] PlannedTotalServiceHours { get; set; }

        /// <summary>Filter by planned total break hours [min, max].</summary>
        [DataMember(Name = "planned_total_break_hours", EmitDefaultValue = false)]
        public double[] PlannedTotalBreakHours { get; set; }

        /// <summary>Filter by projected average service hours [min, max].</summary>
        [DataMember(Name = "projected_avg_service_hours", EmitDefaultValue = false)]
        public double[] ProjectedAvgServiceHours { get; set; }

        /// <summary>Filter by last active timestamp [min, max] (Unix timestamps).</summary>
        [DataMember(Name = "last_active_timestamp", EmitDefaultValue = false)]
        public int[] LastActiveTimestamp { get; set; }

        /// <summary>
        /// Filter by custom data key/value pairs.
        /// Each entry is an object with a <c>key</c> (string) and a <c>value</c> (string or string[]).
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public RouteFilterCustomDataItem[] CustomData { get; set; }

        [DataMember(Name = "master_route_source", EmitDefaultValue = false)]
        public object MasterRouteSource { get; set; }
    }

    /// <summary>
    /// A custom data filter item used within <see cref="FilterRoutesFilters.CustomData"/>.
    /// </summary>
    [DataContract]
    public class RouteFilterCustomDataItem
    {
        /// <summary>Custom data field key (max 50 characters).</summary>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        /// <summary>
        /// Custom data field value. Can be a single string or an array of strings.
        /// Use <see langword="object"/> to support both <see cref="string"/> and <see cref="string[]"/>.
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public object Value { get; set; }
    }
}
