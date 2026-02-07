using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Column configuration for combined views
    /// </summary>
    [DataContract]
    public sealed class CombinedConfigCollection
    {
        /// <summary>
        /// Column definitions
        /// </summary>
        [DataMember(Name = "columns", EmitDefaultValue = false)]
        public ColumnConfig[] Columns { get; set; }

        /// <summary>
        /// Key for the columns configurator
        /// </summary>
        [DataMember(Name = "columns_configurator_key", EmitDefaultValue = false)]
        public string ColumnsConfiguratorKey { get; set; }

        /// <summary>
        /// Route names for the columns configurator
        /// </summary>
        [DataMember(Name = "columns_configurator_route_names", EmitDefaultValue = false)]
        public ColumnsConfiguratorRouteNames ColumnsConfiguratorRouteNames { get; set; }
    }

    /// <summary>
    /// Column configuration details
    /// </summary>
    [DataContract]
    public sealed class ColumnConfig
    {
        /// <summary>
        /// Column identifier
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Display label for the column
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// Cell configuration type
        /// </summary>
        [DataMember(Name = "cell_config", EmitDefaultValue = false)]
        public string CellConfig { get; set; }

        /// <summary>
        /// Whether the column is sortable
        /// </summary>
        [DataMember(Name = "sortable", EmitDefaultValue = false)]
        public bool? Sortable { get; set; }

        /// <summary>
        /// Whether the column takes full width
        /// </summary>
        [DataMember(Name = "full_width", EmitDefaultValue = false)]
        public bool? FullWidth { get; set; }
    }

    /// <summary>
    /// Route names for columns configurator
    /// </summary>
    [DataContract]
    public sealed class ColumnsConfiguratorRouteNames
    {
        /// <summary>
        /// Get route name
        /// </summary>
        [DataMember(Name = "get", EmitDefaultValue = false)]
        public string Get { get; set; }

        /// <summary>
        /// Edit route name
        /// </summary>
        [DataMember(Name = "edit", EmitDefaultValue = false)]
        public string Edit { get; set; }
    }
}
