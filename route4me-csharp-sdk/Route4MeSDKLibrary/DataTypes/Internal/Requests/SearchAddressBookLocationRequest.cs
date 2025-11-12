using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the address book locations searching process.
    /// </summary>
    [DataContract]
    internal sealed class SearchAddressBookLocationRequest : GenericParameters
    {
        /// <value>Comma-delimited list of the contact IDs</value>
        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public string AddressId { get; set; }

        /// <value>The query text</value>
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <value>The comma-delimited list of the fields</value>
        [HttpQueryMemberAttribute(Name = "fields", EmitDefaultValue = false)]
        public string Fields { get; set; }

        /// <value>Search starting position</value>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public int? Offset { get; set; }

        /// <value>The number of records to return</value>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public int? Limit { get; set; }
    }
}