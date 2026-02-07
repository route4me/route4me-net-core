using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Scenario resource with details and statistics
    /// </summary>
    [DataContract]
    public sealed class ScenarioResource
    {
        /// <summary>
        /// Scenario ID (hex32 format)
        /// </summary>
        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public string ScenarioId { get; set; }

        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Optimization parameters used for this scenario
        /// </summary>
        [DataMember(Name = "optimization_parameters", EmitDefaultValue = false)]
        public object OptimizationParameters { get; set; }

        /// <summary>
        /// Scenario description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Total number of routes
        /// </summary>
        [DataMember(Name = "total_routes", EmitDefaultValue = false)]
        public int? TotalRoutes { get; set; }

        /// <summary>
        /// Total number of visits
        /// </summary>
        [DataMember(Name = "total_visits", EmitDefaultValue = false)]
        public int? TotalVisits { get; set; }

        /// <summary>
        /// Total number of locations
        /// </summary>
        [DataMember(Name = "total_locations", EmitDefaultValue = false)]
        public int? TotalLocations { get; set; }

        /// <summary>
        /// Number of routed locations
        /// </summary>
        [DataMember(Name = "routed_locations", EmitDefaultValue = false)]
        public int? RoutedLocations { get; set; }

        /// <summary>
        /// Number of unrouted locations
        /// </summary>
        [DataMember(Name = "unrouted_locations", EmitDefaultValue = false)]
        public int? UnroutedLocations { get; set; }

        /// <summary>
        /// Percentage of routed locations
        /// </summary>
        [DataMember(Name = "routed_locations_percent", EmitDefaultValue = false)]
        public double? RoutedLocationsPercent { get; set; }

        /// <summary>
        /// Percentage of unrouted locations
        /// </summary>
        [DataMember(Name = "unrouted_locations_percent", EmitDefaultValue = false)]
        public double? UnroutedLocationsPercent { get; set; }

        /// <summary>
        /// Average route duration
        /// </summary>
        [DataMember(Name = "avg_route_duration", EmitDefaultValue = false)]
        public int? AvgRouteDuration { get; set; }

        /// <summary>
        /// Total duration
        /// </summary>
        [DataMember(Name = "total_duration", EmitDefaultValue = false)]
        public int? TotalDuration { get; set; }

        /// <summary>
        /// Total travel duration
        /// </summary>
        [DataMember(Name = "total_travel_duration", EmitDefaultValue = false)]
        public int? TotalTravelDuration { get; set; }

        /// <summary>
        /// Total service duration
        /// </summary>
        [DataMember(Name = "total_service_duration", EmitDefaultValue = false)]
        public int? TotalServiceDuration { get; set; }

        /// <summary>
        /// Total wait duration
        /// </summary>
        [DataMember(Name = "total_wait_duration", EmitDefaultValue = false)]
        public int? TotalWaitDuration { get; set; }

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
        /// Average route distance
        /// </summary>
        [DataMember(Name = "avg_route_distance", EmitDefaultValue = false)]
        public double? AvgRouteDistance { get; set; }

        /// <summary>
        /// Average route distance unit
        /// </summary>
        [DataMember(Name = "avg_route_distance_unit", EmitDefaultValue = false)]
        public string AvgRouteDistanceUnit { get; set; }

        /// <summary>
        /// Number of days in the cycle
        /// </summary>
        [DataMember(Name = "days_count", EmitDefaultValue = false)]
        public int? DaysCount { get; set; }

        /// <summary>
        /// Average time between destinations
        /// </summary>
        [DataMember(Name = "avg_time_between_destinations", EmitDefaultValue = false)]
        public int? AvgTimeBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations
        /// </summary>
        [DataMember(Name = "avg_distance_between_destinations", EmitDefaultValue = false)]
        public double? AvgDistanceBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations unit
        /// </summary>
        [DataMember(Name = "avg_distance_between_destinations_unit", EmitDefaultValue = false)]
        public string AvgDistanceBetweenDestinationsUnit { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Status value
        /// </summary>
        [DataMember(Name = "status_value", EmitDefaultValue = false)]
        public string StatusValue { get; set; }

        /// <summary>
        /// Timestamp when scenario was accepted
        /// </summary>
        [DataMember(Name = "accepted_timestamp", EmitDefaultValue = false)]
        public long? AcceptedTimestamp { get; set; }

        /// <summary>
        /// Total created routes
        /// </summary>
        [DataMember(Name = "total_created_routes", EmitDefaultValue = false)]
        public int? TotalCreatedRoutes { get; set; }

        /// <summary>
        /// Total failed routes
        /// </summary>
        [DataMember(Name = "total_failed_routes", EmitDefaultValue = false)]
        public int? TotalFailedRoutes { get; set; }

        /// <summary>
        /// Ad-hoc created routes
        /// </summary>
        [DataMember(Name = "ad_hoc_created_routes", EmitDefaultValue = false)]
        public int? AdHocCreatedRoutes { get; set; }

        /// <summary>
        /// Master created routes
        /// </summary>
        [DataMember(Name = "master_created_routes", EmitDefaultValue = false)]
        public int? MasterCreatedRoutes { get; set; }

        /// <summary>
        /// Percentage of created routes
        /// </summary>
        [DataMember(Name = "created_routes_percent", EmitDefaultValue = false)]
        public double? CreatedRoutesPercent { get; set; }

        /// <summary>
        /// Percentage of failed routes
        /// </summary>
        [DataMember(Name = "failed_routes_percent", EmitDefaultValue = false)]
        public double? FailedRoutesPercent { get; set; }

        /// <summary>
        /// Percentage of ad-hoc created routes
        /// </summary>
        [DataMember(Name = "ad_hoc_created_routes_percent", EmitDefaultValue = false)]
        public double? AdHocCreatedRoutesPercent { get; set; }

        /// <summary>
        /// Percentage of master created routes
        /// </summary>
        [DataMember(Name = "master_created_routes_percent", EmitDefaultValue = false)]
        public double? MasterCreatedRoutesPercent { get; set; }

        /// <summary>
        /// Percentage of travel duration
        /// </summary>
        [DataMember(Name = "travel_duration_percent", EmitDefaultValue = false)]
        public double? TravelDurationPercent { get; set; }

        /// <summary>
        /// Percentage of wait duration
        /// </summary>
        [DataMember(Name = "wait_duration_percent", EmitDefaultValue = false)]
        public double? WaitDurationPercent { get; set; }

        /// <summary>
        /// Percentage of service duration
        /// </summary>
        [DataMember(Name = "service_duration_percent", EmitDefaultValue = false)]
        public double? ServiceDurationPercent { get; set; }

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

        /// <summary>
        /// Dates when routes were created
        /// </summary>
        [DataMember(Name = "routed_dates", EmitDefaultValue = false)]
        public string[] RoutedDates { get; set; }
    }
}
