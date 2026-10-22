using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     An accepted asynchronous dynamic insert lookup job.
    /// </summary>
    [DataContract]
    public class DynamicInsertJob
    {
        /// <summary>
        ///     Async job id (UUID).
        /// </summary>
        [DataMember(Name = "job_id")]
        public string JobId { get; set; }

        /// <summary>
        ///     Path to poll the job status.
        /// </summary>
        [DataMember(Name = "status_url")]
        public string StatusUrl { get; set; }

        /// <summary>
        ///     Path to fetch the job result once terminal.
        /// </summary>
        [DataMember(Name = "result_url")]
        public string ResultUrl { get; set; }
    }

    /// <summary>
    ///     Status or result of an asynchronous dynamic insert lookup job.
    /// </summary>
    [DataContract]
    public class DynamicInsertJobResult
    {
        /// <summary>
        ///     Job status: "processing", "processed", "failed" or "canceled".
        ///     The result endpoint returns "true" when the job finished.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        ///     The best insertion options. Set only when the job finished.
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public DynamicInsertMatchedRoute[] Result { get; set; }
    }
}