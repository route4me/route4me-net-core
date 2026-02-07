using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Describes a single exportable column
    /// </summary>
    [DataContract]
    public sealed class ColumnMetadata
    {
        /// <summary>
        /// Internal column key or identifier
        /// </summary>
        [DataMember(Name = "field_name", EmitDefaultValue = false)]
        public string FieldName { get; set; }

        /// <summary>
        /// Human-readable, translated column title
        /// </summary>
        [DataMember(Name = "field_title", EmitDefaultValue = false)]
        public string FieldTitle { get; set; }

        /// <summary>
        /// Logical group for UI grouping/filtering
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        /// Where the column is coming from (e.g., "addresses", "notes", "actual", "custom", "base")
        /// </summary>
        [DataMember(Name = "scope", EmitDefaultValue = false)]
        public string Scope { get; set; }

        /// <summary>
        /// Whether the user has permission to see the column
        /// </summary>
        [DataMember(Name = "allowed", EmitDefaultValue = false)]
        public bool? Allowed { get; set; }
    }
}
