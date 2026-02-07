using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Strategic optimization combined resource with extended statistics
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationCombinedResource
    {
        /// <summary>
        /// Optimization ID
        /// </summary>
        [DataMember(Name = "optimization_id", EmitDefaultValue = false)]
        public string OptimizationId { get; set; }

        /// <summary>
        /// Optimization name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Number of locations
        /// </summary>
        [DataMember(Name = "locations_count", EmitDefaultValue = false)]
        public int? LocationsCount { get; set; }

        /// <summary>
        /// Number of depots
        /// </summary>
        [DataMember(Name = "depots_count", EmitDefaultValue = false)]
        public int? DepotsCount { get; set; }

        /// <summary>
        /// Number of scenarios
        /// </summary>
        [DataMember(Name = "scenarios_count", EmitDefaultValue = false)]
        public int? ScenariosCount { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        public string Created { get; set; }

        /// <summary>
        /// Average duration across scenarios
        /// </summary>
        [DataMember(Name = "average_duration", EmitDefaultValue = false)]
        public int? AverageDuration { get; set; }

        /// <summary>
        /// Average distance across scenarios
        /// </summary>
        [DataMember(Name = "average_distance", EmitDefaultValue = false)]
        public double? AverageDistance { get; set; }

        /// <summary>
        /// Distance unit
        /// </summary>
        [DataMember(Name = "average_distance_unit", EmitDefaultValue = false)]
        public string AverageDistanceUnit { get; set; }

        /// <summary>
        /// Average number of routes
        /// </summary>
        [DataMember(Name = "average_routes", EmitDefaultValue = false)]
        public double? AverageRoutes { get; set; }

        /// <summary>
        /// Average number of destinations
        /// </summary>
        [DataMember(Name = "average_destinations", EmitDefaultValue = false)]
        public double? AverageDestinations { get; set; }

        /// <summary>
        /// Average route duration
        /// </summary>
        [DataMember(Name = "average_route_duration", EmitDefaultValue = false)]
        public int? AverageRouteDuration { get; set; }

        /// <summary>
        /// Average route distance
        /// </summary>
        [DataMember(Name = "average_route_distance", EmitDefaultValue = false)]
        public double? AverageRouteDistance { get; set; }

        /// <summary>
        /// Average route distance unit
        /// </summary>
        [DataMember(Name = "average_route_distance_unit", EmitDefaultValue = false)]
        public string AverageRouteDistanceUnit { get; set; }

        /// <summary>
        /// Average travel duration
        /// </summary>
        [DataMember(Name = "average_travel_duration", EmitDefaultValue = false)]
        public int? AverageTravelDuration { get; set; }

        /// <summary>
        /// Average service duration
        /// </summary>
        [DataMember(Name = "average_service_duration", EmitDefaultValue = false)]
        public int? AverageServiceDuration { get; set; }

        /// <summary>
        /// Average time between destinations
        /// </summary>
        [DataMember(Name = "average_time_between_destinations", EmitDefaultValue = false)]
        public int? AverageTimeBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations
        /// </summary>
        [DataMember(Name = "average_distance_between_destinations", EmitDefaultValue = false)]
        public double? AverageDistanceBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations unit
        /// </summary>
        [DataMember(Name = "average_distance_between_destinations_unit", EmitDefaultValue = false)]
        public string AverageDistanceBetweenDestinationsUnit { get; set; }

        /// <summary>
        /// Source (e.g., "SFTP", "API", "Upload")
        /// </summary>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Average route weight
        /// </summary>
        [DataMember(Name = "avg_route_weight", EmitDefaultValue = false)]
        public double? AvgRouteWeight { get; set; }

        /// <summary>
        /// Weight unit
        /// </summary>
        [DataMember(Name = "avg_route_weight_unit", EmitDefaultValue = false)]
        public string AvgRouteWeightUnit { get; set; }

        /// <summary>
        /// Average route cost
        /// </summary>
        [DataMember(Name = "avg_route_cost", EmitDefaultValue = false)]
        public double? AvgRouteCost { get; set; }

        /// <summary>
        /// Cost unit
        /// </summary>
        [DataMember(Name = "avg_route_cost_unit", EmitDefaultValue = false)]
        public string AvgRouteCostUnit { get; set; }

        /// <summary>
        /// Average route revenue
        /// </summary>
        [DataMember(Name = "avg_route_revenue", EmitDefaultValue = false)]
        public double? AvgRouteRevenue { get; set; }

        /// <summary>
        /// Revenue unit
        /// </summary>
        [DataMember(Name = "avg_route_revenue_unit", EmitDefaultValue = false)]
        public string AvgRouteRevenueUnit { get; set; }

        /// <summary>
        /// Average route pieces
        /// </summary>
        [DataMember(Name = "avg_route_pieces", EmitDefaultValue = false)]
        public double? AvgRoutePieces { get; set; }

        /// <summary>
        /// Average route cube
        /// </summary>
        [DataMember(Name = "avg_route_cube", EmitDefaultValue = false)]
        public double? AvgRouteCube { get; set; }

        /// <summary>
        /// Cube unit
        /// </summary>
        [DataMember(Name = "avg_route_cube_unit", EmitDefaultValue = false)]
        public string AvgRouteCubeUnit { get; set; }

        /// <summary>
        /// Member who created the optimization
        /// </summary>
        [DataMember(Name = "created_by_member", EmitDefaultValue = false)]
        public string CreatedByMember { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Creation state
        /// </summary>
        [DataMember(Name = "creation_state", EmitDefaultValue = false)]
        public int? CreationState { get; set; }

        /// <summary>
        /// Failed job information
        /// </summary>
        [DataMember(Name = "failed_job", EmitDefaultValue = false)]
        public string FailedJob { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [DataMember(Name = "error_message", EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// User-friendly error message
        /// </summary>
        [DataMember(Name = "user_error", EmitDefaultValue = false)]
        public string UserError { get; set; }

        /// <summary>
        /// Number of failed scenarios
        /// </summary>
        [DataMember(Name = "failed_scenarios_count", EmitDefaultValue = false)]
        public int? FailedScenariosCount { get; set; }

        /// <summary>
        /// Number of optimized scenarios
        /// </summary>
        [DataMember(Name = "optimized_scenarios_count", EmitDefaultValue = false)]
        public int? OptimizedScenariosCount { get; set; }
    }
}
