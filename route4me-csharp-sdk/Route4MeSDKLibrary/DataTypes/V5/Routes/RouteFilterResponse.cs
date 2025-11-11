using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Routes filter response
    /// </summary>
    [DataContract]
    public class RouteFilterResponse
    {
        /// <summary>
        ///     An array of the routes.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteFilterResponseData[] Data { get; set; }

        /// <summary>
        ///     Links to the response pages.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public PageLinks Links { get; set; }

        /// <summary>
        ///     Route meta info
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public PageMeta Meta { get; set; }

        /// <summary>
        ///     An array of the duplicated route IDs.
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIDs { get; set; }


    }

    [DataContract]
    public class RouteFilterResponseData
    {
        /// <summary>
        ///     The route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string RouteID { get; set; }

        /// <summary>
        ///     The name of this route. this route name will be accessible in the search API,
        ///     and also will be displayed on the mobile device of a user.
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string RouteName { get; set; }

        /// <summary>
        ///     When the route was created in Unix EPOCH format.
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public long? CreatedTimestamp { get; set; }

        /// <summary>
        ///     When the route was created in human readable format.
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string CreatedDate { get; set; }

        /// <summary>
        ///     The date the route is scheduled for.
        /// </summary>
        [DataMember(Name = "schedule_date", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string ScheduleDate { get; set; }

        /// <summary>
        ///     If true, the route is approved to be executed.
        /// </summary>
        [DataMember(Name = "approved_to_execute", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool ApprovedToExecute { get; set; }

        /// <summary>
        ///     The date the route was approved to be executed.
        /// </summary>
        [DataMember(Name = "approved_to_execute_date", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string ApprovedToExecuteDate { get; set; }

        /// <summary>
        ///     Destination count.
        /// </summary>
        [DataMember(Name = "destination_count", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? DestinationCount { get; set; }

        /// <summary>
        ///     How many route addresses are visited.
        /// </summary>
        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? VisitedCount { get; set; }

        /// <summary>
        ///     Notes count in the route.
        /// </summary>
        [DataMember(Name = "notes_count", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public int? NotesCount { get; set; }

        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     The member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     Total route's trip distance (the unit is given by distance_unit).
        /// </summary>
        [DataMember(Name = "trip_distance", EmitDefaultValue = false)]
        public double? TripDistance { get; set; }

        /// <summary>
        ///     Actual travel distance.
        /// </summary>
        [DataMember(Name = "actual_travel_distance", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double? ActualTravelDistance { get; set; }

        /// <summary>
        ///     Actual travel time (time span).
        /// </summary>
        [DataMember(Name = "actual_travel_time", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string ActualTravelTime { get; set; }

        /// <summary>
        ///     Total route duration in human readable format (e.g. 'HH:mm:ss').
        /// </summary>
        [DataMember(Name = "route_duration", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string RouteDuration { get; set; }

        /// <summary>
        ///     Route progress
        /// </summary>
        [DataMember(Name = "route_progress", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public double RouteProgress { get; set; }

        /// <summary>
        ///     Route status
        /// </summary>
        [DataMember(Name = "route_status", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string RouteStatus { get; set; }

        /// <summary>
        ///     If true, the tour type is round trip.
        /// </summary>
        [DataMember(Name = "is_roundtrip", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool IsRoundtrip { get; set; }

        /// <summary>
        ///     Route TX source
        /// </summary>
        [DataMember(Name = "route_tx_source", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string RouteTxSource { get; set; }

        /// <summary>
        ///     Smart optimization ID
        /// </summary>
        [DataMember(Name = "smart_optimization_id", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string SmartOptimizationId { get; set; }

        /// <summary>
        ///     The distance measurement unit for the route.
        /// </summary>
        /// <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        [DataMember(Name = "distance_unit", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string DistanceUnit { get; set; }

        /// <summary>
        ///     Depot address ID
        /// </summary>
        [DataMember(Name = "depot_address_id", EmitDefaultValue = false)]
        public long? DepotAddressId { get; set; }

        /// <summary>
        ///     If true, the route is master.
        /// </summary>
        [DataMember(Name = "is_master", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool IsMaster { get; set; }

        /// <summary>
        ///     If true, the route trip is finished at last destination .
        /// </summary>
        [DataMember(Name = "is_last_locked", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public bool IsLastLocked { get; set; }

        /// <summary>
        ///     Optimization problem ID
        /// </summary>
        [DataMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        ///     The algorithm type to use when optimizing the route. See <see cref="DataTypes.AlgorithmType" />
        /// </summary>
        [DataMember(Name = "algorithm_type", EmitDefaultValue = false)]
        public AlgorithmType AlgorithmType { get; set; }

        /// <summary>
        ///     Organization ID
        /// </summary>
        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganizationId { get; set; }

        /// <summary>
        ///     When the route finished (or canceled) (day time in seconds).
        /// </summary>
        [DataMember(Name = "route_end_time", EmitDefaultValue = false)]
        public string RouteEndTime { get; set; }

        /// <summary>
        ///     Original route ID
        /// </summary>
        [DataMember(Name = "original_route_id", EmitDefaultValue = false)]
        public string OriginalRouteId { get; set; }

        /// <summary>
        ///     Route member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        ///     Facility ID's
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }
    }
}