using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Strategic optimization resource
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationResource
    {
        /// <summary>
        /// Strategic optimization ID (hex32 format)
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long? CreatedTimestamp { get; set; }

        /// <summary>
        /// Optimization name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Number of locations in the optimization
        /// </summary>
        [DataMember(Name = "locations_count", EmitDefaultValue = false)]
        public int? LocationsCount { get; set; }

        /// <summary>
        /// Number of scenarios in the optimization
        /// </summary>
        [DataMember(Name = "scenarios_count", EmitDefaultValue = false)]
        public int? ScenariosCount { get; set; }

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
        /// Distance unit (e.g., "mi", "km")
        /// </summary>
        [DataMember(Name = "average_distance_unit", EmitDefaultValue = false)]
        public string AverageDistanceUnit { get; set; }

        /// <summary>
        /// Average weight per route
        /// </summary>
        [DataMember(Name = "avg_route_weight", EmitDefaultValue = false)]
        public double? AvgRouteWeight { get; set; }

        /// <summary>
        /// Weight unit
        /// </summary>
        [DataMember(Name = "avg_route_weight_unit", EmitDefaultValue = false)]
        public string AvgRouteWeightUnit { get; set; }

        /// <summary>
        /// Average cost per route
        /// </summary>
        [DataMember(Name = "avg_route_cost", EmitDefaultValue = false)]
        public double? AvgRouteCost { get; set; }

        /// <summary>
        /// Cost unit
        /// </summary>
        [DataMember(Name = "avg_route_cost_unit", EmitDefaultValue = false)]
        public string AvgRouteCostUnit { get; set; }

        /// <summary>
        /// Average revenue per route
        /// </summary>
        [DataMember(Name = "avg_route_revenue", EmitDefaultValue = false)]
        public double? AvgRouteRevenue { get; set; }

        /// <summary>
        /// Revenue unit
        /// </summary>
        [DataMember(Name = "avg_route_revenue_unit", EmitDefaultValue = false)]
        public string AvgRouteRevenueUnit { get; set; }

        /// <summary>
        /// Average pieces per route
        /// </summary>
        [DataMember(Name = "avg_route_pieces", EmitDefaultValue = false)]
        public double? AvgRoutePieces { get; set; }

        /// <summary>
        /// Average cube per route
        /// </summary>
        [DataMember(Name = "avg_route_cube", EmitDefaultValue = false)]
        public double? AvgRouteCube { get; set; }

        /// <summary>
        /// Cube unit
        /// </summary>
        [DataMember(Name = "avg_route_cube_unit", EmitDefaultValue = false)]
        public string AvgRouteCubeUnit { get; set; }

        /// <summary>
        /// Source of the optimization (e.g., "SFTP", "API", "Upload")
        /// </summary>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Member who created the optimization
        /// </summary>
        [DataMember(Name = "created_by_member", EmitDefaultValue = false)]
        public string CreatedByMember { get; set; }
    }
}
