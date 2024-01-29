using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5
{
    /// <summary>
    /// Batch update orders response
    /// </summary>
    [DataContract]
    public class AsyncStatusResponse
    {
        /// <summary>
        ///     Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        /// <summary>
        ///     Async
        /// </summary>
        [DataMember(Name = "async", EmitDefaultValue = false)]
        public bool Async { get; set; }
    }
}