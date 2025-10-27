using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    /// <summary>
    /// Optimization profile with complete configuration settings
    /// </summary>
    [DataContract]
    public class OptimizationProfile
    {
        /// <summary>
        /// Optimization profile ID
        /// </summary>
        [DataMember(Name = "optimization_profile_id", EmitDefaultValue = false)]
        public string OptimizationProfileId { get; set; }

        /// <summary>
        /// Profile name
        /// </summary>
        [DataMember(Name = "profile_name", EmitDefaultValue = false)]
        public string ProfileName { get; set; }

        /// <summary>
        /// Is default profile
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Created timestamp
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Updated timestamp
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// Append date to route name
        /// </summary>
        [DataMember(Name = "append_date_to_route_name", EmitDefaultValue = false)]
        public bool? AppendDateToRouteName { get; set; }

        /// <summary>
        /// Assigned driver ID
        /// </summary>
        [DataMember(Name = "driver_id", EmitDefaultValue = false)]
        public int? DriverId { get; set; }

        /// <summary>
        /// Assigned vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// Avoid highways
        /// </summary>
        [DataMember(Name = "avoid_highways", EmitDefaultValue = false)]
        public bool? AvoidHighways { get; set; }

        /// <summary>
        /// Avoid tolls
        /// </summary>
        [DataMember(Name = "avoid_tolls", EmitDefaultValue = false)]
        public bool? AvoidTolls { get; set; }

        /// <summary>
        /// Avoidance zones enabled
        /// </summary>
        [DataMember(Name = "avoidance_zones_enabled", EmitDefaultValue = false)]
        public bool? AvoidanceZonesEnabled { get; set; }

        /// <summary>
        /// Avoidance zones IDs
        /// </summary>
        [DataMember(Name = "avoidance_zones_ids", EmitDefaultValue = false)]
        public string[] AvoidanceZonesIds { get; set; }

        /// <summary>
        /// Breaks enabled
        /// </summary>
        [DataMember(Name = "breaks_enabled", EmitDefaultValue = false)]
        public bool? BreaksEnabled { get; set; }

        /// <summary>
        /// Breaks mode
        /// </summary>
        [DataMember(Name = "breaks_mode", EmitDefaultValue = false)]
        public int? BreaksMode { get; set; }

        /// <summary>
        /// Breaks mode parameters
        /// </summary>
        [DataMember(Name = "breaks_mode_params", EmitDefaultValue = false)]
        public int[] BreaksModeParams { get; set; }

        /// <summary>
        /// Breaks repeats number
        /// </summary>
        [DataMember(Name = "breaks_repeats_number", EmitDefaultValue = false)]
        public int? BreaksRepeatsNumber { get; set; }

        /// <summary>
        /// Breaks duration
        /// </summary>
        [DataMember(Name = "breaks_duration", EmitDefaultValue = false)]
        public int? BreaksDuration { get; set; }

        /// <summary>
        /// Bundling settings enabled
        /// </summary>
        [DataMember(Name = "bundling_settings_is_enabled", EmitDefaultValue = false)]
        public bool? BundlingSettingsIsEnabled { get; set; }

        /// <summary>
        /// Bundling settings mode
        /// </summary>
        [DataMember(Name = "bundling_settings_mode", EmitDefaultValue = false)]
        public int? BundlingSettingsMode { get; set; }

        /// <summary>
        /// Bundling settings mode parameters
        /// </summary>
        [DataMember(Name = "bundling_settings_mode_params", EmitDefaultValue = false)]
        public int[] BundlingSettingsModeParams { get; set; }

        /// <summary>
        /// Bundling settings items mode
        /// </summary>
        [DataMember(Name = "bundling_settings_items_mode", EmitDefaultValue = false)]
        public int? BundlingSettingsItemsMode { get; set; }

        /// <summary>
        /// Bundling settings items mode parameters
        /// </summary>
        [DataMember(Name = "bundling_settings_items_mode_params", EmitDefaultValue = false)]
        public int[] BundlingSettingsItemsModeParams { get; set; }

        /// <summary>
        /// Bundling settings first item mode
        /// </summary>
        [DataMember(Name = "bundling_settings_first_item_mode", EmitDefaultValue = false)]
        public int? BundlingSettingsFirstItemMode { get; set; }

        /// <summary>
        /// Bundling settings first item mode parameters
        /// </summary>
        [DataMember(Name = "bundling_settings_first_item_mode_params", EmitDefaultValue = false)]
        public int[] BundlingSettingsFirstItemModeParams { get; set; }

        /// <summary>
        /// Bundling settings merge mode
        /// </summary>
        [DataMember(Name = "bundling_settings_merge_mode", EmitDefaultValue = false)]
        public int? BundlingSettingsMergeMode { get; set; }

        /// <summary>
        /// Multi-depot enabled
        /// </summary>
        [DataMember(Name = "multidepot_enabled", EmitDefaultValue = false)]
        public bool? MultidepotEnabled { get; set; }

        /// <summary>
        /// Depot ID
        /// </summary>
        [DataMember(Name = "depot_id", EmitDefaultValue = false)]
        public string DepotId { get; set; }

        /// <summary>
        /// Depot title
        /// </summary>
        [DataMember(Name = "depot_title", EmitDefaultValue = false)]
        public string DepotTitle { get; set; }

        /// <summary>
        /// Depot latitude
        /// </summary>
        [DataMember(Name = "depot_latitude", EmitDefaultValue = false)]
        public double? DepotLatitude { get; set; }

        /// <summary>
        /// Depot longitude
        /// </summary>
        [DataMember(Name = "depot_longitude", EmitDefaultValue = false)]
        public double? DepotLongitude { get; set; }

        /// <summary>
        /// Depot timezone
        /// </summary>
        [DataMember(Name = "depot_timezone", EmitDefaultValue = false)]
        public string DepotTimezone { get; set; }

        /// <summary>
        /// Facility ID
        /// </summary>
        [DataMember(Name = "facility_id", EmitDefaultValue = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Ignore time windows
        /// </summary>
        [DataMember(Name = "ignore_time_windows", EmitDefaultValue = false)]
        public bool? IgnoreTimeWindows { get; set; }

        /// <summary>
        /// Max distance
        /// </summary>
        [DataMember(Name = "max_distance", EmitDefaultValue = false)]
        public int? MaxDistance { get; set; }

        /// <summary>
        /// Max distance between destinations
        /// </summary>
        [DataMember(Name = "max_distance_between_destinations", EmitDefaultValue = false)]
        public int? MaxDistanceBetweenDestinations { get; set; }

        /// <summary>
        /// Max pieces
        /// </summary>
        [DataMember(Name = "max_pieces", EmitDefaultValue = false)]
        public int? MaxPieces { get; set; }

        /// <summary>
        /// Max revenue
        /// </summary>
        [DataMember(Name = "max_revenue", EmitDefaultValue = false)]
        public int? MaxRevenue { get; set; }

        /// <summary>
        /// Max stops count
        /// </summary>
        [DataMember(Name = "max_stops_count", EmitDefaultValue = false)]
        public int? MaxStopsCount { get; set; }

        /// <summary>
        /// Max volume
        /// </summary>
        [DataMember(Name = "max_volume", EmitDefaultValue = false)]
        public double? MaxVolume { get; set; }

        /// <summary>
        /// Max weight
        /// </summary>
        [DataMember(Name = "max_weight", EmitDefaultValue = false)]
        public double? MaxWeight { get; set; }

        /// <summary>
        /// Mixed fleet enabled
        /// </summary>
        [DataMember(Name = "mixed_fleet_is_enabled", EmitDefaultValue = false)]
        public bool? MixedFleetIsEnabled { get; set; }

        /// <summary>
        /// Mixed fleet vehicles IDs
        /// </summary>
        [DataMember(Name = "mixed_fleet_vehicles_ids", EmitDefaultValue = false)]
        public string[] MixedFleetVehiclesIds { get; set; }

        /// <summary>
        /// Optimization mode
        /// </summary>
        [DataMember(Name = "optimization_mode", EmitDefaultValue = false)]
        public string OptimizationMode { get; set; }

        /// <summary>
        /// Balance mode
        /// </summary>
        [DataMember(Name = "balance_mode", EmitDefaultValue = false)]
        public string BalanceMode { get; set; }

        /// <summary>
        /// Balance routes number
        /// </summary>
        [DataMember(Name = "balance_routes_number", EmitDefaultValue = false)]
        public int? BalanceRoutesNumber { get; set; }

        /// <summary>
        /// Routes max number
        /// </summary>
        [DataMember(Name = "routes_max_number", EmitDefaultValue = false)]
        public int? RoutesMaxNumber { get; set; }

        /// <summary>
        /// Prioritize distance
        /// </summary>
        [DataMember(Name = "prioritize_distance", EmitDefaultValue = false)]
        public int? PrioritizeDistance { get; set; }

        /// <summary>
        /// Prioritize travel time
        /// </summary>
        [DataMember(Name = "prioritize_travel_time", EmitDefaultValue = false)]
        public int? PrioritizeTravelTime { get; set; }

        /// <summary>
        /// Prioritize waiting time
        /// </summary>
        [DataMember(Name = "prioritize_waiting_time", EmitDefaultValue = false)]
        public int? PrioritizeWaitingTime { get; set; }

        /// <summary>
        /// Optimization type
        /// </summary>
        [DataMember(Name = "optimization_type", EmitDefaultValue = false)]
        public string OptimizationType { get; set; }

        /// <summary>
        /// Route duration
        /// </summary>
        [DataMember(Name = "route_duration", EmitDefaultValue = false)]
        public int? RouteDuration { get; set; }

        /// <summary>
        /// Route end mode
        /// </summary>
        [DataMember(Name = "route_end_mode", EmitDefaultValue = false)]
        public string RouteEndMode { get; set; }

        /// <summary>
        /// Route name
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        /// Enable flexible start time
        /// </summary>
        [DataMember(Name = "enable_flexible_start_time", EmitDefaultValue = false)]
        public bool? EnableFlexibleStartTime { get; set; }

        /// <summary>
        /// Depot local time
        /// </summary>
        [DataMember(Name = "depot_local_time", EmitDefaultValue = false)]
        public int? DepotLocalTime { get; set; }

        /// <summary>
        /// Flexible start time earliest start
        /// </summary>
        [DataMember(Name = "flexible_start_time_earliest_start", EmitDefaultValue = false)]
        public int? FlexibleStartTimeEarliestStart { get; set; }

        /// <summary>
        /// Flexible start time latest start
        /// </summary>
        [DataMember(Name = "flexible_start_time_latest_start", EmitDefaultValue = false)]
        public int? FlexibleStartTimeLatestStart { get; set; }

        /// <summary>
        /// Service time override enabled
        /// </summary>
        [DataMember(Name = "service_time_override_is_enabled", EmitDefaultValue = false)]
        public bool? ServiceTimeOverrideIsEnabled { get; set; }

        /// <summary>
        /// Service time override duration
        /// </summary>
        [DataMember(Name = "service_time_override_duration", EmitDefaultValue = false)]
        public int? ServiceTimeOverrideDuration { get; set; }

        /// <summary>
        /// Service time slowdown
        /// </summary>
        [DataMember(Name = "service_time_slowdown", EmitDefaultValue = false)]
        public int? ServiceTimeSlowdown { get; set; }

        /// <summary>
        /// Travel time slowdown
        /// </summary>
        [DataMember(Name = "travel_time_slowdown", EmitDefaultValue = false)]
        public int? TravelTimeSlowdown { get; set; }

        /// <summary>
        /// Turn avoidance
        /// </summary>
        [DataMember(Name = "turn_avoidance", EmitDefaultValue = false)]
        public string TurnAvoidance { get; set; }

        /// <summary>
        /// Use depot as advanced constraint
        /// </summary>
        [DataMember(Name = "use_depot_as_advanced_constraint", EmitDefaultValue = false)]
        public bool? UseDepotAsAdvancedConstraint { get; set; }

        /// <summary>
        /// Resolve drivers by skills
        /// </summary>
        [DataMember(Name = "resolve_drivers_by_skills", EmitDefaultValue = false)]
        public bool? ResolveDriversBySkills { get; set; }

        /// <summary>
        /// Speed cap
        /// </summary>
        [DataMember(Name = "speed_cap", EmitDefaultValue = false)]
        public double? SpeedCap { get; set; }

        /// <summary>
        /// Speed cap unit
        /// </summary>
        [DataMember(Name = "speed_cap_unit", EmitDefaultValue = false)]
        public string SpeedCapUnit { get; set; }

        // Legacy properties for backward compatibility
        /// <summary>
        /// Optimization profile ID (legacy property)
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id 
        { 
            get => OptimizationProfileId; 
            set => OptimizationProfileId = value; 
        }

        /// <summary>
        /// Profile name (legacy property)
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name 
        { 
            get => ProfileName; 
            set => ProfileName = value; 
        }
    }
}