using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Batch update orders request
    /// </summary>
    [DataContract]
    public class BatchUpdateFilterOrderRequest : GenericParameters
    {
        /// <summary>
        /// Organization Api Key
        /// </summary>
        [HttpQueryMemberAttribute(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Order Data { get; set; }

        /// <summary>
        ///     Search
        /// </summary>
        [DataMember(Name = "search", EmitDefaultValue = false)]
        public SearchParamRequestBody Search { get; set; }

        /// <summary>
        ///     Filters
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public FiltersParamRequestBody Filters { get; set; }
    }
}