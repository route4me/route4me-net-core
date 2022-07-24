using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Request body for address book contracts request
    /// </summary>
    [DataContract]
    public class AddressBookContactsBodyPaginatedRequest : GenericParameters
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
        ///     Filter vendors by page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "page")]
        public uint? Page { get; set; }

        /// <summary>
        ///     The number of records (vendors) to display per page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "per_page")]
        public uint? PerPage { get; set; }
    }
}