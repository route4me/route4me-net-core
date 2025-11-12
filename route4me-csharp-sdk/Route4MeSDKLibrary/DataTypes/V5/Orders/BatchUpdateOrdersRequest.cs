using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Batch update orders request
    /// </summary>
    [DataContract]
    public class BatchUpdateOrdersRequest : GenericParameters
    {
        /// <summary>
        ///     Order IDs
        /// </summary>
        [DataMember(Name = "order_ids", EmitDefaultValue = false)]
        public string[] OrderIds { get; set; }

        /// <summary>
        /// Organization Api Key
        /// </summary>
        [DataMember(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Order Data { get; set; }
    }
}