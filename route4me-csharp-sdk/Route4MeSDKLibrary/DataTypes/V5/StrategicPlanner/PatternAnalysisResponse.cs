using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Pattern analysis file upload response
    /// </summary>
    [DataContract]
    public sealed class PatternAnalysisUploadFileResponse
    {
        /// <summary>
        /// Successful request status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool? Status { get; set; }

        /// <summary>
        /// ID in storage of uploaded file
        /// </summary>
        [DataMember(Name = "upload_id", EmitDefaultValue = false)]
        public string UploadId { get; set; }

        /// <summary>
        /// Array of available encodings
        /// </summary>
        [DataMember(Name = "encodings", EmitDefaultValue = false)]
        public string[] Encodings { get; set; }

        /// <summary>
        /// Default columns mapping
        /// </summary>
        [DataMember(Name = "default_map", EmitDefaultValue = false)]
        public object DefaultMap { get; set; }
    }

    /// <summary>
    /// Pattern analysis upload preview response
    /// </summary>
    [DataContract]
    public sealed class PatternAnalysisUploadPreviewResponse
    {
        /// <summary>
        /// Array of headers in the file
        /// </summary>
        [DataMember(Name = "csv_header", EmitDefaultValue = false)]
        public string[] CsvHeader { get; set; }

        /// <summary>
        /// Array of parsed data in the file
        /// </summary>
        [DataMember(Name = "sample_parsed_data", EmitDefaultValue = false)]
        public object[] SampleParsedData { get; set; }

        /// <summary>
        /// Array of sheet names (for Excel files)
        /// </summary>
        [DataMember(Name = "sheets", EmitDefaultValue = false)]
        public string[] Sheets { get; set; }

        /// <summary>
        /// Current sheet number
        /// </summary>
        [DataMember(Name = "current_sheet", EmitDefaultValue = false)]
        public int? CurrentSheet { get; set; }

        /// <summary>
        /// Number of found addresses
        /// </summary>
        [DataMember(Name = "address_count", EmitDefaultValue = false)]
        public int? AddressCount { get; set; }

        /// <summary>
        /// Warnings found in the file
        /// </summary>
        [DataMember(Name = "warnings", EmitDefaultValue = false)]
        public object Warnings { get; set; }
    }

    /// <summary>
    /// Pattern analysis initiation response
    /// </summary>
    [DataContract]
    public sealed class InitiatePatternAnalysisResponse
    {
        /// <summary>
        /// Success status
        /// </summary>
        [DataMember(Name = "success", EmitDefaultValue = false)]
        public bool? Success { get; set; }

        /// <summary>
        /// Job ID for tracking the analysis
        /// </summary>
        [DataMember(Name = "job_id", EmitDefaultValue = false)]
        public string JobId { get; set; }
    }
}
