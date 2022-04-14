using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response from the getting process of the address book contacts
    /// </summary>
    [DataContract]
    public sealed class GetAddressBookContactsResponse : GenericParameters
    {
        /// <value>Array of the AddressBookContact type objects</value>
        [System.Runtime.Serialization.DataMember(Name = "results", IsRequired = false)]
        public Route4MeSDK.DataTypes.AddressBookContact[] Results { get; set; }

        /// <value>Number of the returned address book contacts</value>
        [System.Runtime.Serialization.DataMember(Name = "total", IsRequired = false)]
        public uint Total { get; set; }
    }
}
