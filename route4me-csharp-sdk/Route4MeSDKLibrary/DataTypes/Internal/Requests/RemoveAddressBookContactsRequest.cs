using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameter for the address book contacts removing process.
    /// </summary>
    [DataContract]
    internal sealed class RemoveAddressBookContactsRequest : GenericParameters
    {
        /// <value>The array of the address IDs</value>
        [System.Runtime.Serialization.DataMember(Name = "address_ids", EmitDefaultValue = false)]
        public string[] AddressIds { get; set; }
    }
}