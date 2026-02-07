using System.Runtime.Serialization;
using Route4MeSDKLibrary.QueryTypes;
using Route4MeSDK.DataTypes.V5.StrategicPlanner;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for file upload preview
    /// </summary>
    [DataContract]
    public sealed class UploadPreviewRequest : GenericParameters
    {
        /// <summary>
        /// Uploaded file ID received from uploadFile
        /// </summary>
        [DataMember(Name = "strUploadID", EmitDefaultValue = false)]
        public string StrUploadID { get; set; }

        /// <summary>
        /// Return errors as warnings (with 200 status) instead of errors (with 406 status)
        /// </summary>
        [DataMember(Name = "skip_validation", EmitDefaultValue = false)]
        public bool? SkipValidation { get; set; }

        /// <summary>
        /// Number of returned rows to show in the table (max 500)
        /// </summary>
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int? Limit { get; set; }

        /// <summary>
        /// Number of Excel file sheet
        /// </summary>
        [DataMember(Name = "sheet", EmitDefaultValue = false)]
        public int? Sheet { get; set; }

        /// <summary>
        /// Number of encoding
        /// </summary>
        [DataMember(Name = "intFromEncodingIndex", EmitDefaultValue = false)]
        public int? IntFromEncodingIndex { get; set; }

        /// <summary>
        /// Array of columns mapping to overwrite default
        /// </summary>
        [DataMember(Name = "arrValidCSVColumns", EmitDefaultValue = false)]
        public string[] ArrValidCSVColumns { get; set; }

        /// <summary>
        /// Array of columns to ignore in mapping
        /// </summary>
        [DataMember(Name = "arrIgnoreCSVColumns", EmitDefaultValue = false)]
        public string[] ArrIgnoreCSVColumns { get; set; }
    }

    /// <summary>
    /// Request for creating optimization from uploaded file or customer locations
    /// </summary>
    [DataContract]
    public sealed class CreateOptimizationRequest : GenericParameters
    {
        /// <summary>
        /// Uploaded file ID (optional if using filters for customer locations)
        /// </summary>
        [DataMember(Name = "strUploadID", EmitDefaultValue = false)]
        public string StrUploadID { get; set; }

        /// <summary>
        /// Name of the uploaded file
        /// </summary>
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Array of JSON objects containing parameters for processing
        /// </summary>
        [DataMember(Name = "paramsJson", EmitDefaultValue = false)]
        public OptimizationParameters[] ParamsJson { get; set; }

        /// <summary>
        /// Array of columns mapping to overwrite default
        /// </summary>
        [DataMember(Name = "arrValidCSVColumns", EmitDefaultValue = false)]
        public string[] ArrValidCSVColumns { get; set; }

        /// <summary>
        /// Array of columns to ignore in mapping
        /// </summary>
        [DataMember(Name = "arrIgnoreCSVColumns", EmitDefaultValue = false)]
        public string[] ArrIgnoreCSVColumns { get; set; }

        /// <summary>
        /// Indicates if the request originates from the UI
        /// </summary>
        [DataMember(Name = "isUi", EmitDefaultValue = false)]
        public bool? IsUi { get; set; }

        /// <summary>
        /// Depot addresses
        /// </summary>
        [DataMember(Name = "depot_addresses", EmitDefaultValue = false)]
        public DepotAddress[] DepotAddresses { get; set; }

        /// <summary>
        /// Name of the strategic optimization
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Filters for customer locations (used when not uploading a file)
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public object Filters { get; set; }

        /// <summary>
        /// Draft optimization ID
        /// </summary>
        [DataMember(Name = "draft_optimization_id", EmitDefaultValue = false)]
        public string DraftOptimizationId { get; set; }
    }

    /// <summary>
    /// Depot address
    /// </summary>
    [DataContract]
    public sealed class DepotAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }
    }
}
