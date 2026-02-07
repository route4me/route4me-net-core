using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing draft routes
    /// </summary>
    [DataContract]
    public sealed class ListRoutesRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for routes
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilters Filters { get; set; }

        /// <summary>
        /// Group results by field (e.g., "number_week", "none")
        /// </summary>
        [DataMember(Name = "group_by", EmitDefaultValue = false)]
        public string GroupBy { get; set; }
    }

    /// <summary>
    /// Filters for draft routes
    /// </summary>
    [DataContract]
    public sealed class RouteFilters
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
        /// Scenario ID
        /// </summary>
        [DataMember(Name = "scenario_id", EmitDefaultValue = false)]
        public string ScenarioId { get; set; }

        /// <summary>
        /// Week numbers (exact values)
        /// </summary>
        [DataMember(Name = "number_week", EmitDefaultValue = false)]
        public int[] NumberWeek { get; set; }

        /// <summary>
        /// Week days (exact values: 0-6 for Sunday-Saturday)
        /// </summary>
        [DataMember(Name = "week_day", EmitDefaultValue = false)]
        public int[] WeekDay { get; set; }

        /// <summary>
        /// Start time range [min, max]
        /// </summary>
        [DataMember(Name = "start_time", EmitDefaultValue = false)]
        public int[] StartTime { get; set; }

        /// <summary>
        /// Total duration range [min, max]
        /// </summary>
        [DataMember(Name = "total_duration", EmitDefaultValue = false)]
        public int[] TotalDuration { get; set; }

        /// <summary>
        /// Total distance range [min, max]
        /// </summary>
        [DataMember(Name = "total_distance", EmitDefaultValue = false)]
        public double[] TotalDistance { get; set; }

        /// <summary>
        /// Total destinations range [min, max]
        /// </summary>
        [DataMember(Name = "total_destinations", EmitDefaultValue = false)]
        public int[] TotalDestinations { get; set; }

        /// <summary>
        /// Average time between stops range [min, max]
        /// </summary>
        [DataMember(Name = "avg_time_between_stops", EmitDefaultValue = false)]
        public int[] AvgTimeBetweenStops { get; set; }

        /// <summary>
        /// Average distance between stops range [min, max]
        /// </summary>
        [DataMember(Name = "avg_distance_between_stops", EmitDefaultValue = false)]
        public double[] AvgDistanceBetweenStops { get; set; }

        /// <summary>
        /// Start date range ["YYYY-MM-DD", "YYYY-MM-DD"]
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        public string[] StartDate { get; set; }
    }
}
