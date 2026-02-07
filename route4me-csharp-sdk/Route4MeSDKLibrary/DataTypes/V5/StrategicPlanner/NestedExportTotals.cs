using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Totals for nested export operations on optimizations
    /// </summary>
    [DataContract]
    public sealed class NestedExportOptimizationsFilterTotalsResource
    {
        /// <summary>
        /// Optimizations totals
        /// </summary>
        [DataMember(Name = "optimizations", EmitDefaultValue = false)]
        public ExportTotalItem Optimizations { get; set; }

        /// <summary>
        /// Locations totals
        /// </summary>
        [DataMember(Name = "locations", EmitDefaultValue = false)]
        public ExportTotalItem Locations { get; set; }

        /// <summary>
        /// Scenarios totals
        /// </summary>
        [DataMember(Name = "scenarios", EmitDefaultValue = false)]
        public ExportTotalItem Scenarios { get; set; }

        /// <summary>
        /// Visits totals
        /// </summary>
        [DataMember(Name = "visits", EmitDefaultValue = false)]
        public ExportTotalItem Visits { get; set; }

        /// <summary>
        /// Routes totals
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public ExportTotalItem Routes { get; set; }
    }

    /// <summary>
    /// Totals for nested export operations on scenarios
    /// </summary>
    [DataContract]
    public sealed class NestedExportScenariosFilterTotalsResource
    {
        /// <summary>
        /// Scenarios totals
        /// </summary>
        [DataMember(Name = "scenarios", EmitDefaultValue = false)]
        public ExportTotalItem Scenarios { get; set; }

        /// <summary>
        /// Visits totals
        /// </summary>
        [DataMember(Name = "visits", EmitDefaultValue = false)]
        public ExportTotalItem Visits { get; set; }

        /// <summary>
        /// Routes totals
        /// </summary>
        [DataMember(Name = "routes", EmitDefaultValue = false)]
        public ExportTotalItem Routes { get; set; }
    }

    /// <summary>
    /// Export total item with count and relation
    /// </summary>
    [DataContract]
    public sealed class ExportTotalItem
    {
        /// <summary>
        /// Total count of items
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }

        /// <summary>
        /// Relation indicator: "eq" (exact), "gte" (greater than or equal), "err" (error)
        /// </summary>
        [DataMember(Name = "relation", EmitDefaultValue = false)]
        public string Relation { get; set; }
    }

    /// <summary>
    /// Response wrapper for nested export totals
    /// </summary>
    [DataContract]
    public sealed class NestedExportTotalsResponse
    {
        /// <summary>
        /// Totals data
        /// </summary>
        [DataMember(Name = "totals", EmitDefaultValue = false)]
        public object Totals { get; set; }
    }
}
