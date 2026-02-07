using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Export job status response
    /// </summary>
    [DataContract]
    public sealed class ExportStatusResponse
    {
        /// <summary>
        /// Export filename
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = false)]
        public string Filename { get; set; }

        /// <summary>
        /// Export status (e.g., "finished", "failed", "processing")
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// When the export job started
        /// </summary>
        [DataMember(Name = "started", EmitDefaultValue = false)]
        public string Started { get; set; }

        /// <summary>
        /// When the export job finished (if completed)
        /// </summary>
        [DataMember(Name = "finished", EmitDefaultValue = false)]
        public string Finished { get; set; }
    }

    /// <summary>
    /// Export job submission response
    /// </summary>
    [DataContract]
    public sealed class ExportJobResponse
    {
        /// <summary>
        /// Job submission status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Unique job ID assigned to the background task
        /// </summary>
        [DataMember(Name = "job_id", EmitDefaultValue = false)]
        public int? JobId { get; set; }
    }
}
