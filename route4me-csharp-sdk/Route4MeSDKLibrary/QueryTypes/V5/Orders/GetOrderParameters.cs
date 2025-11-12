using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Orders
{
    [DataContract]
    public class GetOrderParameters : GenericParameters
    {
        /// <summary>
        /// Order UUID
        /// </summary>
        public string OrderUuid { get; set; }

        /// <summary>
        /// Organization Api Key
        /// </summary>
        [HttpQueryMemberAttribute(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }
    }
}