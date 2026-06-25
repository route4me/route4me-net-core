using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The 202 envelope returned by the asynchronous dynamic insert dispatch endpoint
    ///     (POST /routes/lookup-for-new-destination/async).
    /// </summary>
    [DataContract]
    public sealed class DynamicInsertJobResponse
    {
        /// <summary>
        ///     Canonical async job id (UUID). The single source of truth for correlation and polling.
        /// </summary>
        [DataMember(Name = "job_id")]
        public string JobId { get; set; }

        /// <summary>
        ///     URL to poll for the job status (points at the module-level job-tracker route).
        /// </summary>
        [DataMember(Name = "status_url")]
        public string StatusUrl { get; set; }

        /// <summary>
        ///     URL to fetch the final result once the job is terminal
        ///     (points at the module-level job-tracker route).
        /// </summary>
        [DataMember(Name = "result_url")]
        public string ResultUrl { get; set; }
    }
}
