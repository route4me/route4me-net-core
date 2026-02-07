using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing strategic optimizations
    /// </summary>
    [DataContract]
    public sealed class ListStrategicOptimizationsRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for strategic optimizations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public StrategicOptimizationFilters Filters { get; set; }
    }

    /// <summary>
    /// Filters for strategic optimizations
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationFilters
    {
        /// <summary>
        /// Search query
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// Creation timestamp range [min, max]
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long[] CreatedTimestamp { get; set; }

        /// <summary>
        /// Scenarios count range [min, max]
        /// </summary>
        [DataMember(Name = "scenarios_count", EmitDefaultValue = false)]
        public int[] ScenariosCount { get; set; }

        /// <summary>
        /// Locations count range [min, max]
        /// </summary>
        [DataMember(Name = "locations_count", EmitDefaultValue = false)]
        public int[] LocationsCount { get; set; }

        /// <summary>
        /// Average distance range [min, max]
        /// </summary>
        [DataMember(Name = "average_distance", EmitDefaultValue = false)]
        public double[] AverageDistance { get; set; }

        /// <summary>
        /// Average duration range [min, max]
        /// </summary>
        [DataMember(Name = "average_duration", EmitDefaultValue = false)]
        public int[] AverageDuration { get; set; }

        /// <summary>
        /// Created date range ["YYYY-MM-DD", "YYYY-MM-DD"]
        /// </summary>
        [DataMember(Name = "created_date", EmitDefaultValue = false)]
        public string[] CreatedDate { get; set; }

        /// <summary>
        /// Average routes range [min, max]
        /// </summary>
        [DataMember(Name = "average_routes", EmitDefaultValue = false)]
        public double[] AverageRoutes { get; set; }

        /// <summary>
        /// Average destinations range [min, max]
        /// </summary>
        [DataMember(Name = "average_destinations", EmitDefaultValue = false)]
        public double[] AverageDestinations { get; set; }

        /// <summary>
        /// Average route duration range [min, max]
        /// </summary>
        [DataMember(Name = "average_route_duration", EmitDefaultValue = false)]
        public int[] AverageRouteDuration { get; set; }

        /// <summary>
        /// Average route distance range [min, max]
        /// </summary>
        [DataMember(Name = "average_route_distance", EmitDefaultValue = false)]
        public double[] AverageRouteDistance { get; set; }

        /// <summary>
        /// Average travel duration range [min, max]
        /// </summary>
        [DataMember(Name = "average_travel_duration", EmitDefaultValue = false)]
        public int[] AverageTravelDuration { get; set; }

        /// <summary>
        /// Average service duration range [min, max]
        /// </summary>
        [DataMember(Name = "average_service_duration", EmitDefaultValue = false)]
        public int[] AverageServiceDuration { get; set; }

        /// <summary>
        /// Average time between destinations range [min, max]
        /// </summary>
        [DataMember(Name = "average_time_between_destinations", EmitDefaultValue = false)]
        public int[] AverageTimeBetweenDestinations { get; set; }

        /// <summary>
        /// Average distance between destinations range [min, max]
        /// </summary>
        [DataMember(Name = "average_distance_between_destinations", EmitDefaultValue = false)]
        public double[] AverageDistanceBetweenDestinations { get; set; }
    }
}
