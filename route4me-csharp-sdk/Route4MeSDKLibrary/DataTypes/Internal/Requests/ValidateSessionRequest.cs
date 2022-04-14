using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the session validation process.
    /// </summary>
    [DataContract]
    internal sealed class ValidateSessionRequest : GenericParameters
    {
        /// <value>The session ID</value>
        [HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
        public string SessionGuid { get; set; }

        /// <value>The member ID</value>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <value>The response format (json, xml)</value>
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }
    }
}
