using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Response for bulk create notes operation
    /// </summary>
    [DataContract]
    public class BulkNotesResponse
    {
        /// <summary>
        ///     Status of the operation
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        /// <summary>
        ///     Indicates if operation is asynchronous
        /// </summary>
        [DataMember(Name = "async", EmitDefaultValue = false)]
        public bool Async { get; set; }
    }
}