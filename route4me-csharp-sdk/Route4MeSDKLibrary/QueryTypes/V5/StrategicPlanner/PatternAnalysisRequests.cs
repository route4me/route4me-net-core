using System.Runtime.Serialization;
using Route4MeSDKLibrary.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for pattern analysis file upload preview
    /// </summary>
    [DataContract]
    public sealed class PatternAnalysisUploadPreviewRequest : GenericParameters
    {
        /// <summary>
        /// Uploaded file ID
        /// </summary>
        [DataMember(Name = "strUploadID", EmitDefaultValue = false)]
        public string StrUploadID { get; set; }

        /// <summary>
        /// Skip validation
        /// </summary>
        [DataMember(Name = "skip_validation", EmitDefaultValue = false)]
        public bool? SkipValidation { get; set; }

        /// <summary>
        /// Limit number of rows
        /// </summary>
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int? Limit { get; set; }

        /// <summary>
        /// Sheet number
        /// </summary>
        [DataMember(Name = "sheet", EmitDefaultValue = false)]
        public int? Sheet { get; set; }

        /// <summary>
        /// Encoding index
        /// </summary>
        [DataMember(Name = "intFromEncodingIndex", EmitDefaultValue = false)]
        public int? IntFromEncodingIndex { get; set; }

        /// <summary>
        /// Valid CSV columns
        /// </summary>
        [DataMember(Name = "arrValidCSVColumns", EmitDefaultValue = false)]
        public string[] ArrValidCSVColumns { get; set; }

        /// <summary>
        /// Ignore CSV columns
        /// </summary>
        [DataMember(Name = "arrIgnoreCSVColumns", EmitDefaultValue = false)]
        public string[] ArrIgnoreCSVColumns { get; set; }
    }

    /// <summary>
    /// Request for initiating pattern analysis
    /// </summary>
    [DataContract]
    public sealed class InitiatePatternAnalysisRequest : GenericParameters
    {
        /// <summary>
        /// Uploaded file ID
        /// </summary>
        [DataMember(Name = "strUploadID", EmitDefaultValue = false)]
        public string StrUploadID { get; set; }

        /// <summary>
        /// Valid CSV columns
        /// </summary>
        [DataMember(Name = "arrValidCSVColumns", EmitDefaultValue = false)]
        public string[] ArrValidCSVColumns { get; set; }

        /// <summary>
        /// Ignore CSV columns
        /// </summary>
        [DataMember(Name = "arrIgnoreCSVColumns", EmitDefaultValue = false)]
        public string[] ArrIgnoreCSVColumns { get; set; }
    }
}
