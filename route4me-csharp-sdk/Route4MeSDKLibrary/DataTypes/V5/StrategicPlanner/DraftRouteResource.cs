using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Draft route resource
    /// </summary>
    [DataContract]
    public sealed class DraftRouteResource
    {
        /// <summary>
        /// Route draft ID (hex32 format)
        /// </summary>
        [DataMember(Name = "route_draft_id", EmitDefaultValue = false)]
        public string RouteDraftId { get; set; }

        /// <summary>
        /// Draft route name
        /// </summary>
        [DataMember(Name = "draft_route_name", EmitDefaultValue = false)]
        public string DraftRouteName { get; set; }

        /// <summary>
        /// Scenario ID
        /// </summary>
        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public string ScenarioId { get; set; }

        /// <summary>
        /// Week number
        /// </summary>
        [DataMember(Name = "number_week", EmitDefaultValue = false)]
        public int? NumberWeek { get; set; }

        /// <summary>
        /// Day of week (formatted string)
        /// </summary>
        [DataMember(Name = "week_day", EmitDefaultValue = false)]
        public string WeekDay { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }

        /// <summary>
        /// Start time (formatted string)
        /// </summary>
        [DataMember(Name = "start_time", EmitDefaultValue = false)]
        public string StartTime { get; set; }

        /// <summary>
        /// Total duration in seconds
        /// </summary>
        [DataMember(Name = "total_duration", EmitDefaultValue = false)]
        public int? TotalDuration { get; set; }

        /// <summary>
        /// Total service duration in seconds
        /// </summary>
        [DataMember(Name = "total_service_duration", EmitDefaultValue = false)]
        public int? TotalServiceDuration { get; set; }

        /// <summary>
        /// Total travel duration in seconds
        /// </summary>
        [DataMember(Name = "total_travel_duration", EmitDefaultValue = false)]
        public int? TotalTravelDuration { get; set; }

        /// <summary>
        /// Total break duration in seconds
        /// </summary>
        [DataMember(Name = "total_break_duration", EmitDefaultValue = false)]
        public int? TotalBreakDuration { get; set; }

        /// <summary>
        /// Total wait duration in seconds
        /// </summary>
        [DataMember(Name = "total_wait_duration", EmitDefaultValue = false)]
        public int? TotalWaitDuration { get; set; }

        /// <summary>
        /// Vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// Member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        /// Stops per hour
        /// </summary>
        [DataMember(Name = "sporh", EmitDefaultValue = false)]
        public double? Sporh { get; set; }

        /// <summary>
        /// Total distance
        /// </summary>
        [DataMember(Name = "total_distance", EmitDefaultValue = false)]
        public double? TotalDistance { get; set; }

        /// <summary>
        /// Distance unit
        /// </summary>
        [DataMember(Name = "total_distance_unit", EmitDefaultValue = false)]
        public string TotalDistanceUnit { get; set; }

        /// <summary>
        /// Total number of destinations
        /// </summary>
        [DataMember(Name = "total_destinations", EmitDefaultValue = false)]
        public int? TotalDestinations { get; set; }

        /// <summary>
        /// Average time between stops in seconds
        /// </summary>
        [DataMember(Name = "avg_time_between_stops", EmitDefaultValue = false)]
        public int? AvgTimeBetweenStops { get; set; }

        /// <summary>
        /// Average distance between stops
        /// </summary>
        [DataMember(Name = "avg_distance_between_stops", EmitDefaultValue = false)]
        public double? AvgDistanceBetweenStops { get; set; }

        /// <summary>
        /// Average distance between stops unit
        /// </summary>
        [DataMember(Name = "avg_distance_between_stops_unit", EmitDefaultValue = false)]
        public string AvgDistanceBetweenStopsUnit { get; set; }

        /// <summary>
        /// Route tag
        /// </summary>
        [DataMember(Name = "route_tag", EmitDefaultValue = false)]
        public string RouteTag { get; set; }

        /// <summary>
        /// Total weight
        /// </summary>
        [DataMember(Name = "total_weight", EmitDefaultValue = false)]
        public double? TotalWeight { get; set; }

        /// <summary>
        /// Weight unit
        /// </summary>
        [DataMember(Name = "total_weight_unit", EmitDefaultValue = false)]
        public string TotalWeightUnit { get; set; }

        /// <summary>
        /// Total cost
        /// </summary>
        [DataMember(Name = "total_cost", EmitDefaultValue = false)]
        public double? TotalCost { get; set; }

        /// <summary>
        /// Cost unit
        /// </summary>
        [DataMember(Name = "total_cost_unit", EmitDefaultValue = false)]
        public string TotalCostUnit { get; set; }

        /// <summary>
        /// Total revenue
        /// </summary>
        [DataMember(Name = "total_revenue", EmitDefaultValue = false)]
        public double? TotalRevenue { get; set; }

        /// <summary>
        /// Revenue unit
        /// </summary>
        [DataMember(Name = "total_revenue_unit", EmitDefaultValue = false)]
        public string TotalRevenueUnit { get; set; }

        /// <summary>
        /// Total cube
        /// </summary>
        [DataMember(Name = "total_cube", EmitDefaultValue = false)]
        public double? TotalCube { get; set; }

        /// <summary>
        /// Cube unit
        /// </summary>
        [DataMember(Name = "total_cube_unit", EmitDefaultValue = false)]
        public string TotalCubeUnit { get; set; }

        /// <summary>
        /// Total pieces
        /// </summary>
        [DataMember(Name = "total_pieces", EmitDefaultValue = false)]
        public int? TotalPieces { get; set; }
    }
}
