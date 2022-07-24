using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Request body for address book contracts request
    /// </summary>
    [DataContract]
    public class AddressBookContactsBodyRequest : GenericParameters
    {
        /// <summary>
        ///     Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        /// <summary>
        ///     Filter
        /// </summary>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressBookContactsFilter[] Filter { get; set; }

        /// <summary>
        /// You can sort the results using the specified fields.
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public List<string[]> OrderBy { get; set; }

        /// <summary>
        ///     Limit the number of records in response.
        /// </summary>
        [DataMember(Name = "limit")]
        public uint? Limit { get; set; }

        /// <summary>
        ///     Only records from that offset will be considered.
        /// </summary>
        [DataMember(Name = "offset")]
        public uint? Offset { get; set; }

    }
}
