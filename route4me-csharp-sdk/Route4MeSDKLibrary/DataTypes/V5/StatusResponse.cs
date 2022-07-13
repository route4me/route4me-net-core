using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the requests returning only boolean parameter 'status'
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class StatusResponse : GenericParameters
    {
        /// <summary>
        ///     Status of the request process.
        /// </summary>
        /// <value>
        ///     <c>true</c> if request finished successfully; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool status { get; set; }

        /// <summary>
        ///     Status code of the request process.
        /// </summary>
        [DataMember(Name = "status_code", EmitDefaultValue = false)]
        public int? StatusCode { get; set; }

        /// <summary>
        ///     Is success status code
        /// </summary>
        [DataMember(Name = "is_success_status_code", EmitDefaultValue = false)]
        public bool IsSuccessStatusCode { get; set; }
    }
}