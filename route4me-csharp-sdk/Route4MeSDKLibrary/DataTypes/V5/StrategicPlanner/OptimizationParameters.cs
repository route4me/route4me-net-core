using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Optimization configuration parameters for strategic planning
    /// </summary>
    [DataContract]
    public sealed class OptimizationParameters
    {
        /// <summary>
        /// Indicates if the request is an upload
        /// </summary>
        [DataMember(Name = "is_upload", EmitDefaultValue = false)]
        public bool? IsUpload { get; set; }

        /// <summary>
        /// Round trip flag
        /// </summary>
        [DataMember(Name = "rt", EmitDefaultValue = false)]
        public bool? RoundTrip { get; set; }

        /// <summary>
        /// Disable optimization flag
        /// </summary>
        [DataMember(Name = "disable_optimization", EmitDefaultValue = false)]
        public bool? DisableOptimization { get; set; }

        /// <summary>
        /// Name of the route
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        /// Unix timestamp for the route date
        /// </summary>
        [DataMember(Name = "route_date", EmitDefaultValue = false)]
        public long? RouteDate { get; set; }

        /// <summary>
        /// Route start time in seconds since midnight
        /// </summary>
        [DataMember(Name = "route_time", EmitDefaultValue = false)]
        public int? RouteTime { get; set; }

        /// <summary>
        /// Optimization type: "Distance", "Time", "timeWithTraffic"
        /// </summary>
        [DataMember(Name = "optimize", EmitDefaultValue = false)]
        public string Optimize { get; set; }

        /// <summary>
        /// Maximum vehicle capacity
        /// </summary>
        [DataMember(Name = "vehicle_capacity", EmitDefaultValue = false)]
        public double? VehicleCapacity { get; set; }

        /// <summary>
        /// Unit of distance: "mi" or "km"
        /// </summary>
        [DataMember(Name = "distance_unit", EmitDefaultValue = false)]
        public string DistanceUnit { get; set; }

        /// <summary>
        /// Mode of travel: "Driving", "Walking", "Bicycling"
        /// </summary>
        [DataMember(Name = "travel_mode", EmitDefaultValue = false)]
        public string TravelMode { get; set; }

        /// <summary>
        /// Use different matrices for vehicle profiles
        /// </summary>
        [DataMember(Name = "use_different_matrices_for_vehicle_profiles", EmitDefaultValue = false)]
        public bool? UseDifferentMatricesForVehicleProfiles { get; set; }

        /// <summary>
        /// Use vehicle skills for mixed fleet
        /// </summary>
        [DataMember(Name = "use_vehicle_skills_for_mixed_fleet", EmitDefaultValue = false)]
        public bool? UseVehicleSkillsForMixedFleet { get; set; }

        /// <summary>
        /// Use driver skills for mixed fleet
        /// </summary>
        [DataMember(Name = "use_driver_skills_for_mixed_fleet", EmitDefaultValue = false)]
        public bool? UseDriverSkillsForMixedFleet { get; set; }

        /// <summary>
        /// An array of the available vehicle IDs
        /// </summary>
        [DataMember(Name = "vehicle_ids", EmitDefaultValue = false)]
        public string[] VehicleIds { get; set; }

        /// <summary>
        /// Scheduler configuration
        /// </summary>
        [DataMember(Name = "scheduler", EmitDefaultValue = false)]
        public SchedulerConfiguration Scheduler { get; set; }

        /// <summary>
        /// Name of the scheduler
        /// </summary>
        [DataMember(Name = "scheduler_name", EmitDefaultValue = false)]
        public string SchedulerName { get; set; }

        /// <summary>
        /// Timezone for the schedule (e.g., "America/New_York")
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        /// Advanced constraints for optimization
        /// </summary>
        [DataMember(Name = "advanced_constraints", EmitDefaultValue = false)]
        public AdvancedConstraint[] AdvancedConstraints { get; set; }
    }

    /// <summary>
    /// Advanced constraint for optimization
    /// </summary>
    [DataContract]
    public sealed class AdvancedConstraint
    {
        /// <summary>
        /// Maximum cargo weight
        /// </summary>
        [DataMember(Name = "max_cargo_weight", EmitDefaultValue = false)]
        public double? MaxCargoWeight { get; set; }

        /// <summary>
        /// Maximum vehicle capacity
        /// </summary>
        [DataMember(Name = "max_capacity", EmitDefaultValue = false)]
        public int? MaxCapacity { get; set; }

        /// <summary>
        /// Tags for constraint filtering
        /// </summary>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public string[] Tags { get; set; }
    }
}
