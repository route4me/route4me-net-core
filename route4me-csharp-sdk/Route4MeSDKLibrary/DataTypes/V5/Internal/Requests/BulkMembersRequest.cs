using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDKLibrary.DataTypes.V5.Internal.Requests
{
    /// <summary>
    ///     The request class to bulk create the team members.
    /// </summary>
    [DataContract]
    internal sealed class BulkMembersRequest : GenericParameters
    {
        // Array of the team member requests
        [DataMember(Name = "users")] public TeamRequest[] Users { get; set; }
    }
}
