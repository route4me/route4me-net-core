using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing scenarios
    /// </summary>
    [DataContract]
    public sealed class ListScenariosRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for scenarios
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public ScenarioFilters Filters { get; set; }
    }

    /// <summary>
    /// Filters for scenarios
    /// </summary>
    [DataContract]
    public sealed class ScenarioFilters
    {
        /// <summary>
        /// Search query
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Optimization ID
        /// </summary>
        [DataMember(Name = "optimization_id", EmitDefaultValue = false)]
        public string OptimizationId { get; set; }

        /// <summary>
        /// Days count filter (exact values)
        /// </summary>
        [DataMember(Name = "days_count", EmitDefaultValue = false)]
        public int[] DaysCount { get; set; }

        /// <summary>
        /// Total routes range [min, max]
        /// </summary>
        [DataMember(Name = "total_routes", EmitDefaultValue = false)]
        public int[] TotalRoutes { get; set; }

        /// <summary>
        /// Total visits range [min, max]
        /// </summary>
        [DataMember(Name = "total_visits", EmitDefaultValue = false)]
        public int[] TotalVisits { get; set; }

        /// <summary>
        /// Average route duration range [min, max]
        /// </summary>
        [DataMember(Name = "avg_route_duration", EmitDefaultValue = false)]
        public int[] AvgRouteDuration { get; set; }

        /// <summary>
        /// Total duration range [min, max]
        /// </summary>
        [DataMember(Name = "total_duration", EmitDefaultValue = false)]
        public int[] TotalDuration { get; set; }

        /// <summary>
        /// Total travel duration range [min, max]
        /// </summary>
        [DataMember(Name = "total_travel_duration", EmitDefaultValue = false)]
        public int[] TotalTravelDuration { get; set; }

        /// <summary>
        /// Total service duration range [min, max]
        /// </summary>
        [DataMember(Name = "total_service_duration", EmitDefaultValue = false)]
        public int[] TotalServiceDuration { get; set; }

        /// <summary>
        /// Total distance range [min, max]
        /// </summary>
        [DataMember(Name = "total_distance", EmitDefaultValue = false)]
        public double[] TotalDistance { get; set; }

        /// <summary>
        /// Average route distance range [min, max]
        /// </summary>
        [DataMember(Name = "avg_route_distance", EmitDefaultValue = false)]
        public double[] AvgRouteDistance { get; set; }

        /// <summary>
        /// Average time between destinations range [min, max]
        /// </summary>
        [DataMember(Name = "avg_time_between_destinations", EmitDefaultValue = false)]
        public int[] AvgTimeBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations range [min, max]
        /// </summary>
        [DataMember(Name = "avg_distance_between_destinations", EmitDefaultValue = false)]
        public double[] AvgDistanceBetweenDestinations { get; set; }

        /// <summary>
        /// Accepted timestamp range [min, max]
        /// </summary>
        [DataMember(Name = "accepted_timestamp", EmitDefaultValue = false)]
        public long[] AcceptedTimestamp { get; set; }

        /// <summary>
        /// Total created routes range [min, max]
        /// </summary>
        [DataMember(Name = "total_created_routes", EmitDefaultValue = false)]
        public int[] TotalCreatedRoutes { get; set; }

        /// <summary>
        /// Total failed routes range [min, max]
        /// </summary>
        [DataMember(Name = "total_failed_routes", EmitDefaultValue = false)]
        public int[] TotalFailedRoutes { get; set; }

        /// <summary>
        /// Total locations range [min, max]
        /// </summary>
        [DataMember(Name = "total_locations", EmitDefaultValue = false)]
        public int[] TotalLocations { get; set; }

        /// <summary>
        /// Routed locations range [min, max]
        /// </summary>
        [DataMember(Name = "routed_locations", EmitDefaultValue = false)]
        public int[] RoutedLocations { get; set; }

        /// <summary>
        /// Unrouted locations range [min, max]
        /// </summary>
        [DataMember(Name = "unrouted_locations", EmitDefaultValue = false)]
        public int[] UnroutedLocations { get; set; }
    }
}
