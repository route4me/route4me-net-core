using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// AI-generated scenario configuration resource
    /// </summary>
    [DataContract]
    public sealed class GenerateScenarioResource
    {
        /// <summary>
        /// Scheduler name
        /// </summary>
        [DataMember(Name = "scheduler_name", EmitDefaultValue = false)]
        public string SchedulerName { get; set; }

        /// <summary>
        /// Scheduler configuration
        /// </summary>
        [DataMember(Name = "scheduler", EmitDefaultValue = false)]
        public GeneratedSchedulerConfig Scheduler { get; set; }

        /// <summary>
        /// Number of routes to generate
        /// </summary>
        [DataMember(Name = "scheduler_generate_routes", EmitDefaultValue = false)]
        public int? SchedulerGenerateRoutes { get; set; }

        /// <summary>
        /// Maximum route duration
        /// </summary>
        [DataMember(Name = "route_max_duration", EmitDefaultValue = false)]
        public int? RouteMaxDuration { get; set; }

        /// <summary>
        /// Route time
        /// </summary>
        [DataMember(Name = "route_time", EmitDefaultValue = false)]
        public int? RouteTime { get; set; }

        /// <summary>
        /// Round trip
        /// </summary>
        [DataMember(Name = "rt", EmitDefaultValue = false)]
        public int? Rt { get; set; }

        /// <summary>
        /// Is flexible start time
        /// </summary>
        [DataMember(Name = "is_flexible_start_time", EmitDefaultValue = false)]
        public bool? IsFlexibleStartTime { get; set; }

        /// <summary>
        /// Flexible start time configuration
        /// </summary>
        [DataMember(Name = "flexible_start_time", EmitDefaultValue = false)]
        public FlexibleStartTime FlexibleStartTime { get; set; }

        /// <summary>
        /// Slowdowns configuration
        /// </summary>
        [DataMember(Name = "slowdowns", EmitDefaultValue = false)]
        public Slowdowns Slowdowns { get; set; }

        /// <summary>
        /// Timezone
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        /// Vehicle capacity
        /// </summary>
        [DataMember(Name = "vehicle_capacity", EmitDefaultValue = false)]
        public int? VehicleCapacity { get; set; }

        /// <summary>
        /// Vehicle maximum distance in miles
        /// </summary>
        [DataMember(Name = "vehicle_max_distance_mi", EmitDefaultValue = false)]
        public int? VehicleMaxDistanceMi { get; set; }

        /// <summary>
        /// Keep routes consistency
        /// </summary>
        [DataMember(Name = "keep_routes_consistency", EmitDefaultValue = false)]
        public bool? KeepRoutesConsistency { get; set; }

        /// <summary>
        /// Balance configuration
        /// </summary>
        [DataMember(Name = "balance", EmitDefaultValue = false)]
        public BalanceConfig Balance { get; set; }

        /// <summary>
        /// Lock last N destinations
        /// </summary>
        [DataMember(Name = "lock_last", EmitDefaultValue = false)]
        public int? LockLast { get; set; }

        /// <summary>
        /// Breaks configuration
        /// </summary>
        [DataMember(Name = "breaks", EmitDefaultValue = false)]
        public BreakConfig[] Breaks { get; set; }

        /// <summary>
        /// Maximum daily routes
        /// </summary>
        [DataMember(Name = "max_daily_routes", EmitDefaultValue = false)]
        public string MaxDailyRoutes { get; set; }

        /// <summary>
        /// Maximum routes per period
        /// </summary>
        [DataMember(Name = "max_routes_per_period", EmitDefaultValue = false)]
        public string MaxRoutesPerPeriod { get; set; }

        /// <summary>
        /// Vehicle maximum cargo weight
        /// </summary>
        [DataMember(Name = "vehicle_max_cargo_weight", EmitDefaultValue = false)]
        public string VehicleMaxCargoWeight { get; set; }

        /// <summary>
        /// Vehicle maximum cargo volume
        /// </summary>
        [DataMember(Name = "vehicle_max_cargo_volume", EmitDefaultValue = false)]
        public string VehicleMaxCargoVolume { get; set; }

        /// <summary>
        /// Maximum tour size
        /// </summary>
        [DataMember(Name = "max_tour_size", EmitDefaultValue = false)]
        public string MaxTourSize { get; set; }

        /// <summary>
        /// Subtour maximum revenue
        /// </summary>
        [DataMember(Name = "subtour_max_revenue", EmitDefaultValue = false)]
        public string SubtourMaxRevenue { get; set; }
    }

    /// <summary>
    /// Generated scheduler configuration
    /// </summary>
    [DataContract]
    public sealed class GeneratedSchedulerConfig
    {
        /// <summary>
        /// Start date
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }

        /// <summary>
        /// Number of cycles
        /// </summary>
        [DataMember(Name = "cycles", EmitDefaultValue = false)]
        public int? Cycles { get; set; }

        /// <summary>
        /// Cycle length in days
        /// </summary>
        [DataMember(Name = "cycle_length", EmitDefaultValue = false)]
        public int? CycleLength { get; set; }

        /// <summary>
        /// Blackout days
        /// </summary>
        [DataMember(Name = "blackout_days", EmitDefaultValue = false)]
        public string[] BlackoutDays { get; set; }

        /// <summary>
        /// Blackout dates
        /// </summary>
        [DataMember(Name = "blackout_dates", EmitDefaultValue = false)]
        public string[] BlackoutDates { get; set; }
    }

    /// <summary>
    /// Flexible start time configuration
    /// </summary>
    [DataContract]
    public sealed class FlexibleStartTime
    {
        /// <summary>
        /// Earliest start time
        /// </summary>
        [DataMember(Name = "earliest_start", EmitDefaultValue = false)]
        public int? EarliestStart { get; set; }

        /// <summary>
        /// Latest start time
        /// </summary>
        [DataMember(Name = "latest_start", EmitDefaultValue = false)]
        public int? LatestStart { get; set; }
    }

    /// <summary>
    /// Slowdowns configuration
    /// </summary>
    [DataContract]
    public sealed class Slowdowns
    {
        /// <summary>
        /// Service time slowdown percentage
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public int? ServiceTime { get; set; }

        /// <summary>
        /// Travel time slowdown percentage
        /// </summary>
        [DataMember(Name = "travel_time", EmitDefaultValue = false)]
        public int? TravelTime { get; set; }
    }

    /// <summary>
    /// Balance configuration
    /// </summary>
    [DataContract]
    public sealed class BalanceConfig
    {
        /// <summary>
        /// Balance mode
        /// </summary>
        [DataMember(Name = "mode", EmitDefaultValue = false)]
        public string Mode { get; set; }
    }

    /// <summary>
    /// Break configuration
    /// </summary>
    [DataContract]
    public sealed class BreakConfig
    {
        /// <summary>
        /// Break mode
        /// </summary>
        [DataMember(Name = "mode", EmitDefaultValue = false)]
        public int? Mode { get; set; }

        /// <summary>
        /// Break duration
        /// </summary>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public int? Duration { get; set; }

        /// <summary>
        /// Mode parameters
        /// </summary>
        [DataMember(Name = "mode_params", EmitDefaultValue = false)]
        public int[] ModeParams { get; set; }

        /// <summary>
        /// Number of times the break repeats
        /// </summary>
        [DataMember(Name = "repeats_number", EmitDefaultValue = false)]
        public int? RepeatsNumber { get; set; }
    }
}
