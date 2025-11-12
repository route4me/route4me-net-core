using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response for the get users process
    /// </summary>
    [DataContract]
    public sealed class GetUsersResponse
    {
        /// <value>The array of the User objects</value>
        [System.Runtime.Serialization.DataMember(Name = "results")]
        public MemberResponseV4[] Results { get; set; }
    }
}