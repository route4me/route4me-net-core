using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5
{
    /// <summary>
    ///     The request parameter for the address book contacts removing process.
    /// </summary>
    [DataContract]
    public sealed class AddressBookContactsRequest : GenericParameters
    {
        /// The array of the address IDs
        [DataMember(Name = "address_ids", EmitDefaultValue = false)]
        public long[] AddressIds { get; set; }
    }
}
