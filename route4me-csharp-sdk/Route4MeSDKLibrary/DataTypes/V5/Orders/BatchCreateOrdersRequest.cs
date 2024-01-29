using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Batch create orders request
    /// </summary>
    [DataContract]
    public class BatchCreateOrdersRequest : GenericParameters
    {
        /// <summary>
        /// Organization Api Key
        /// </summary>
        [HttpQueryMember(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Order[] Data { get; set; }
    }
}