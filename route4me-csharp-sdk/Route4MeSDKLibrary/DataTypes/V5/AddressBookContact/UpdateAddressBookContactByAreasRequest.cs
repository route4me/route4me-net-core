using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Multiple address book contracts for update
    /// </summary>
    [DataContract]
    public sealed class UpdateAddressBookContactByAreasRequest : GenericParameters
    {
        /// <summary>
        ///     Filter
        /// </summary>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressBookContactsFilter Filter { get; set; }

        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public AddressBookContact Data { get; set; }
    }
}