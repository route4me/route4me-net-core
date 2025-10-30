using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Route filter parameters.
    /// </summary>
    [DataContract]
    public sealed class RouteFilterParameters : GenericParameters
    {
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilterParametersFilters Filters { get; set; }

        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public List<string[]> OrderBy { get; set; }

        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }
    }

    [DataContract]
    public class RouteFilterParametersFilters : GenericParameters
    {
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string Query { get; set; }

        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        [DataMember(Name = "route_tx_source", EmitDefaultValue = false)]
        public string RouteTxSource { get; set; }

        [DataMember(Name = "schedule_date", EmitDefaultValue = false)]
        public string[] ScheduleDate { get; set; }

        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string[] CreatedDate { get; set; }

        [DataMember(Name = "route_status", EmitDefaultValue = false)]
        public string[] RouteStatus { get; set; }

        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public long? ContactId { get; set; }

        [DataMember(Name = "vehicle_ids", EmitDefaultValue = false)]
        public string[] VehicleIds { get; set; }

        [DataMember(Name = "member_ids", EmitDefaultValue = false)]
        public long[] MemberIds { get; set; }

        [DataMember(Name = "depot_address_id", EmitDefaultValue = false)]
        public long? DepotAddressId { get; set; }

        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long[] CreatedTimestamp { get; set; }

        [DataMember(Name = "approved_to_execute", EmitDefaultValue = false)]
        public bool? ApprovedToExecute { get; set; }

        [DataMember(Name = "approved_to_execute_date", EmitDefaultValue = false)]
        public long[] ApprovedToExecuteDate { get; set; }

        [DataMember(Name = "destination_count", EmitDefaultValue = false)]
        public int[] DestinationCount { get; set; }

        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int[] VisitedCount { get; set; }

        [DataMember(Name = "notes_count", EmitDefaultValue = false)]
        public int[] NotesCount { get; set; }

        [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
        public double[] TripDistance { get; set; }

        [DataMember(Name = "actual_travel_distance", EmitDefaultValue = false)]
        public double[] ActualTravelDistance { get; set; }

        [DataMember(Name = "actual_travel_time", EmitDefaultValue = false)]
        public long[] ActualTravelTime { get; set; }

        [DataMember(Name = "route_duration", EmitDefaultValue = false)]
        public long[] RouteDuration { get; set; }

        [DataMember(Name = "planned_total_route_duration", EmitDefaultValue = false)]
        public long[] PlannedTotalRouteDuration { get; set; }

        [DataMember(Name = "route_progress", EmitDefaultValue = false)]
        public int[] RouteProgress { get; set; }

        [DataMember(Name = "is_roundtrip", EmitDefaultValue = false)]
        public bool? IsRoundtrip { get; set; }

        [DataMember(Name = "smart_optimization_id", EmitDefaultValue = false)]
        public string SmartOptimizationId { get; set; }

        [DataMember(Name = "is_master", EmitDefaultValue = false)]
        public bool IsMaster { get; set; }

        [DataMember(Name = "is_last_locked", EmitDefaultValue = false)]
        public bool? IsLastLocked { get; set; }

        [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
        public string AlgorithmType { get; set; }

        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public string OrganizationId { get; set; }

        [DataMember(Name = "master_route_id", EmitDefaultValue = false)]
        public string MasterRouteId { get; set; }

        [DataMember(Name = "route_start_time", EmitDefaultValue = false)]
        public long[] RouteStartTime { get; set; }

        [DataMember(Name = "route_end_time", EmitDefaultValue = false)]
        public long[] RouteEndTime { get; set; }

        [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
        public string OriginalRouteId { get; set; }

        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        [DataMember(Name = "destinations_arrived_on_time", EmitDefaultValue = false)]
        public int[] DestinationsArrivedOnTime { get; set; }

        [DataMember(Name = "destinations_arrived_early", EmitDefaultValue = false)]
        public int[] DestinationsArrivedEarly { get; set; }

        [DataMember(Name = "destinations_arrived_late", EmitDefaultValue = false)]
        public int[] DestinationsArrivedLate { get; set; }

        [DataMember(Name = "actual_weight_load_percent", EmitDefaultValue = false)]
        public int[] ActualWeightLoadPercent { get; set; }

        [DataMember(Name = "planned_weight_load_percent", EmitDefaultValue = false)]
        public int[] PlannedWeightLoadPercent { get; set; }

        [DataMember(Name = "actual_cube_load_percent", EmitDefaultValue = false)]
        public int[] ActualCubeLoadPercent { get; set; }

        [DataMember(Name = "planned_cube_load_percent", EmitDefaultValue = false)]
        public int[] PlannedCubeLoadPercent { get; set; }

        [DataMember(Name = "actual_revenue_load_percent", EmitDefaultValue = false)]
        public int[] ActualRevenueLoadPercent { get; set; }

        [DataMember(Name = "planned_revenue_load_percent", EmitDefaultValue = false)]
        public int[] PlannedRevenueLoadPercent { get; set; }

        [DataMember(Name = "actual_pieces_load_percent", EmitDefaultValue = false)]
        public int[] ActualPiecesLoadPercent { get; set; }

        [DataMember(Name = "planned_pieces_load_percent", EmitDefaultValue = false)]
        public int[] PlannedPiecesLoadPercent { get; set; }

        [DataMember(Name = "mobile_actual_travel_distance", EmitDefaultValue = false)]
        public double[] MobileActualTravelDistance { get; set; }

        [DataMember(Name = "telematics_actual_travel_distance", EmitDefaultValue = false)]
        public double[] TelematicsActualTravelDistance { get; set; }

        [DataMember(Name = "failed_destinations", EmitDefaultValue = false)]
        public int[] FailedDestinations { get; set; }

        [DataMember(Name = "skipped_destinations", EmitDefaultValue = false)]
        public int[] SkippedDestinations { get; set; }

        [DataMember(Name = "done_destinations", EmitDefaultValue = false)]
        public int[] DoneDestinations { get; set; }

        [DataMember(Name = "planned_destinations", EmitDefaultValue = false)]
        public int[] PlannedDestinations { get; set; }

        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public int[] Cube { get; set; }

        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public int[] Revenue { get; set; }

        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public int[] Pieces { get; set; }

        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public int[] Weight { get; set; }

        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }
    }
}