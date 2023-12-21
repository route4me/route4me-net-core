using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    /// Remove address book group request body
    /// </summary>
    [DataContract]
    internal class RemoveAddressBookGroupRequest : GenericParameters
    {
        /// <summary>
        ///     Group ID
        /// </summary>
        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public string GroupId { get; set; }
    }
}
