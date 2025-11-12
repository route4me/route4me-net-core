using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Search orders request
    /// </summary>
    [DataContract]
    public class SearchOrdersRequest : GenericParameters
    {
        /// <summary>
        ///     Order IDs
        /// </summary>
        [DataMember(Name = "order_ids", EmitDefaultValue = false)]
        public string[] OrderIds { get; set; }

        /// <summary>
        ///     Return provided fields as map
        /// </summary>
        [DataMember(Name = "return_provided_fields_as_map", EmitDefaultValue = false)]
        public string ReturnProvidedFieldsAsMap { get; set; }

        /// <summary>
        ///     Array of fields name to be ordered by (ASC by default, of DESC the field name should have "-" prefix).
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public string[] OrderBy { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int Limit { get; set; }

        /// <summary>
        /// Offset (page number)
        /// </summary>
        [DataMember(Name = "offset", EmitDefaultValue = false)]
        public int Offset { get; set; }

        /// <summary>
        ///     Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        /// <summary>
        ///     Additional Fields
        /// </summary>
        [DataMember(Name = "addition", EmitDefaultValue = false)]
        public string[] AdditionalFields { get; set; }

        /// <summary>
        ///     Search
        /// </summary>
        [DataMember(Name = "search", EmitDefaultValue = false)]
        public SearchParamRequestBody Search { get; set; }

        /// <summary>
        ///     Search
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public FiltersParamRequestBody Filters { get; set; }

        /// <summary>
        /// Organization Api Key
        /// </summary>
        [HttpQueryMember(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }
    }
}