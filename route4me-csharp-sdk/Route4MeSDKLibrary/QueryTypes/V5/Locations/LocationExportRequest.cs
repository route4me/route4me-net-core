using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for location export
    /// Exports locations to file formats like CSV, XLS, or XLSX
    /// </summary>
    [DataContract]
    public class LocationExportRequest : LocationCombinedRequest
    {
        /// <summary>
        /// Array of column field names to include in export
        /// </summary>
        [DataMember(Name = "columns", EmitDefaultValue = false)]
        public string[] Columns { get; set; }

        /// <summary>
        /// Specific address IDs to export (if specified, filters are ignored)
        /// </summary>
        [DataMember(Name = "address_ids", EmitDefaultValue = false)]
        public int[] AddressIds { get; set; }

        /// <summary>
        /// Address IDs to exclude from export
        /// </summary>
        [DataMember(Name = "excluded_ids", EmitDefaultValue = false)]
        public int[] ExcludedIds { get; set; }

        /// <summary>
        /// Output filename (without extension)
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = false)]
        public string Filename { get; set; }

        /// <summary>
        /// Export format: "csv", "xls", or "xlsx"
        /// </summary>
        [DataMember(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        /// <summary>
        /// Timezone for date/time formatting (e.g., "America/New_York")
        /// </summary>
        [DataMember(Name = "tz", EmitDefaultValue = false)]
        public string Timezone { get; set; }
    }

    /// <summary>
    /// Request for getting available export columns information
    /// Returns metadata about which columns can be exported
    /// </summary>
    [DataContract]
    public class LocationExportColumnsRequest : LocationCombinedRequest
    {
        /// <summary>
        /// Specific address IDs to check for exportable columns
        /// </summary>
        [DataMember(Name = "address_ids", EmitDefaultValue = false)]
        public int[] AddressIds { get; set; }

        /// <summary>
        /// Address IDs to exclude when determining exportable columns
        /// </summary>
        [DataMember(Name = "excluded_ids", EmitDefaultValue = false)]
        public int[] ExcludedIds { get; set; }
    }
}