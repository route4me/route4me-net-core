using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Batch delete orders request
    /// </summary>
    [DataContract]
    public class BatchDeleteOrdersRequest : GenericParameters
    {
        /// <summary>
        ///     Order IDs
        /// </summary>
        [DataMember(Name = "order_ids", EmitDefaultValue = false)]
        public string[] OrderIds { get; set; }
    }
}