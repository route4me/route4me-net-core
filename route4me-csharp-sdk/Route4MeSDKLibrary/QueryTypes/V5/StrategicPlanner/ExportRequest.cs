using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Base request for export operations
    /// </summary>
    [DataContract]
    public class ExportRequest : BulkOperationRequest
    {
        /// <summary>
        /// Columns to include in the export
        /// </summary>
        [DataMember(Name = "columns", EmitDefaultValue = false)]
        public string[] Columns { get; set; }

        /// <summary>
        /// Export format (e.g., "csv", "xlsx")
        /// </summary>
        [DataMember(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        /// <summary>
        /// Include related data in export
        /// </summary>
        [DataMember(Name = "with_related_data", EmitDefaultValue = false)]
        public bool? WithRelatedData { get; set; }
    }

    /// <summary>
    /// Request for exporting optimizations
    /// </summary>
    [DataContract]
    public sealed class ExportOptimizationsRequest : ExportRequest
    {
        /// <summary>
        /// Filters for optimizations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public StrategicOptimizationFilters Filters { get; set; }
    }

    /// <summary>
    /// Request for exporting locations
    /// </summary>
    [DataContract]
    public sealed class ExportLocationsRequest : ExportRequest
    {
        /// <summary>
        /// Filters for locations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public LocationFilters Filters { get; set; }
    }

    /// <summary>
    /// Request for exporting scenarios
    /// </summary>
    [DataContract]
    public sealed class ExportScenariosRequest : ExportRequest
    {
        /// <summary>
        /// Filters for scenarios
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public ScenarioFilters Filters { get; set; }
    }

    /// <summary>
    /// Request for exporting routes
    /// </summary>
    [DataContract]
    public sealed class ExportRoutesRequest : ExportRequest
    {
        /// <summary>
        /// Filters for routes
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilters Filters { get; set; }
    }

    /// <summary>
    /// Request for exporting visits
    /// </summary>
    [DataContract]
    public sealed class ExportVisitsRequest : ExportRequest
    {
        /// <summary>
        /// Filters for visits
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public VisitFilters Filters { get; set; }
    }

    /// <summary>
    /// Request for nested export of optimizations with separate filters for each entity type
    /// </summary>
    [DataContract]
    public sealed class NestedOptimizationsExportRequest : BaseListRequest
    {
        /// <summary>
        /// Export parameters for optimizations
        /// </summary>
        [DataMember(Name = "optimizations", EmitDefaultValue = false)]
        public ExportOptimizationsRequest Optimizations { get; set; }

        /// <summary>
        /// Export parameters for locations
        /// </summary>
        [DataMember(Name = "locations", EmitDefaultValue = false)]
        public ExportLocationsRequest Locations { get; set; }

        /// <summary>
        /// Export parameters for scenarios
        /// </summary>
        [DataMember(Name = "scenarios", EmitDefaultValue = false)]
        public ExportScenariosRequest Scenarios { get; set; }

        /// <summary>
        /// Export parameters for routes
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public ExportRoutesRequest Routes { get; set; }

        /// <summary>
        /// Export parameters for visits
        /// </summary>
        [DataMember(Name = "visits", EmitDefaultValue = false)]
        public ExportVisitsRequest Visits { get; set; }
    }

    /// <summary>
    /// Request for nested export of scenarios with separate filters for each entity type
    /// </summary>
    [DataContract]
    public sealed class NestedScenariosExportRequest : BaseListRequest
    {
        /// <summary>
        /// Export parameters for scenarios
        /// </summary>
        [DataMember(Name = "scenarios", EmitDefaultValue = false)]
        public ExportScenariosRequest Scenarios { get; set; }

        /// <summary>
        /// Export parameters for routes
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public ExportRoutesRequest Routes { get; set; }

        /// <summary>
        /// Export parameters for visits
        /// </summary>
        [DataMember(Name = "visits", EmitDefaultValue = false)]
        public ExportVisitsRequest Visits { get; set; }
    }
}
