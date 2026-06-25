using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The result payload returned by the asynchronous dynamic insert job-tracker result endpoint
    ///     (GET /routes/job-tracker/result/{job_id}). The <see cref="Result"/> array matches the shape
    ///     the synchronous lookup-for-new-destination endpoint returns.
    /// </summary>
    [DataContract]
    public sealed class DynamicInsertJobResult
    {
        /// <summary>
        ///     Indicates whether the job completed successfully.
        /// </summary>
        [DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <summary>
        ///     The list of best insertion options for the supplied destination.
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public DynamicInsertMatchedRoute[] Result { get; set; }
    }
}
