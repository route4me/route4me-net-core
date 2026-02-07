using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Response from file upload operation
    /// </summary>
    [DataContract]
    public sealed class UploadFileResponse
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
        /// Array of available encodings to decode uploaded file
        /// </summary>
        [DataMember(Name = "encodings", EmitDefaultValue = false)]
        public string[] Encodings { get; set; }

        /// <summary>
        /// Default columns mapping
        /// </summary>
        [DataMember(Name = "default_map", EmitDefaultValue = false)]
        public object DefaultMap { get; set; }
    }
}
