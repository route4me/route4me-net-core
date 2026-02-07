using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing draft visits
    /// </summary>
    [DataContract]
    public sealed class ListVisitsRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for visits
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public VisitFilters Filters { get; set; }
    }

    /// <summary>
    /// Filters for draft visits
    /// </summary>
    [DataContract]
    public sealed class VisitFilters
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
        /// Route draft ID
        /// </summary>
        [DataMember(Name = "route_draft_id", EmitDefaultValue = false)]
        public string RouteDraftId { get; set; }

        /// <summary>
        /// Planner location ID
        /// </summary>
        [DataMember(Name = "planner_location_id", EmitDefaultValue = false)]
        public string PlannerLocationId { get; set; }

        /// <summary>
        /// Destinations count range [min, max]
        /// </summary>
        [DataMember(Name = "destinations", EmitDefaultValue = false)]
        public int[] Destinations { get; set; }

        /// <summary>
        /// Average days between destinations range [min, max]
        /// </summary>
        [DataMember(Name = "avg_days_between_destinations", EmitDefaultValue = false)]
        public double[] AvgDaysBetweenDestinations { get; set; }

        /// <summary>
        /// Visit day filter (exact values: 0-6 for Sunday-Saturday)
        /// </summary>
        [DataMember(Name = "visit_day", EmitDefaultValue = false)]
        public int[] VisitDay { get; set; }

        /// <summary>
        /// Cycle frequency filter (exact values)
        /// </summary>
        [DataMember(Name = "cycle_frequency", EmitDefaultValue = false)]
        public int[] CycleFrequency { get; set; }
    }
}
