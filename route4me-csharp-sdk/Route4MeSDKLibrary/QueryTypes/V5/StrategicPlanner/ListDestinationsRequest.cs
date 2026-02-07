using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing draft destinations
    /// </summary>
    [DataContract]
    public sealed class ListDestinationsRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for destinations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public DestinationFilters Filters { get; set; }
    }

    /// <summary>
    /// Filters for draft destinations
    /// </summary>
    [DataContract]
    public sealed class DestinationFilters
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
    }
}
