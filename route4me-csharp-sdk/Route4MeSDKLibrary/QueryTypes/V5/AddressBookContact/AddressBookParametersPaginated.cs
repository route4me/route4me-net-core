using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// Parameters for the address book contact(s) paginated request
    /// </summary>
    public sealed class AddressBookParametersPaginated : GenericParameters
    {
        /// <summary>
        /// Comma-delimited list of the address fields to be included into the search results. e.g., "address_id, address_alias, address_1"
        /// </summary>
        [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
        public string Fields { get; set; }

        /// <summary>
        /// Specify which Addresses to show in the corresponding query results. (<seealso cref="DisplayValues"/>)
        /// </summary>
        [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }

        /// <summary>
        /// Search in the Addresses by the corresponding query phrase.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        ///     Filter vendors by page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        ///     The number of records (vendors) to display per page.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

    }
}