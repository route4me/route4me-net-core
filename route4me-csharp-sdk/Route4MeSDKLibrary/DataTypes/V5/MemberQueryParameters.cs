using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5
{
    /// <summary>
    ///     The request parameters for retrieving team members.
    /// </summary>
    [DataContract]
    public sealed class MemberQueryParameters : GenericParameters
    {
        /// <value>Team user ID</value>
        [HttpQueryMember(Name = "user_id", EmitDefaultValue = false)]
        public string UserId { get; set; }
    }
}
