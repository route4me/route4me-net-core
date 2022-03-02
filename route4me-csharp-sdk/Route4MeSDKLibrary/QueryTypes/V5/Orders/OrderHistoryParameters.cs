using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Orders
{
    /// <summary>
    ///     Parameters for Order History API
    /// </summary>
    [DataContract]
    public class OrderHistoryParameters : GenericParameters
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "order_id", EmitDefaultValue = false)]
        public long OrderId { get; set; }

        /// <summary>
        /// Tracking number
        /// </summary>
        [HttpQueryMemberAttribute(Name = "tracking_number ", EmitDefaultValue = false)]
        public string TrackingNumber { get; set; }
    }
}
