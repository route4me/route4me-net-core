using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5
{
    /// <summary>
    ///     The request parameter for the multiple address book contacts creating process.
    /// </summary>
    [DataContract]
    public sealed class BatchCreatingAddressBookContactsRequest : GenericParameters
    {
        /// The array of the address IDs
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Route4MeSDK.DataTypes.V5.AddressBookContact[] Data { get; set; }
    }
}
