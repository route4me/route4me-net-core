using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    ///     Order history response
    /// </summary>
    [DataContract]
    public class OrderHistoryResponse
    {
        /// <summary>
        ///     Next page cursor
        /// </summary>
        [DataMember(Name = "next_page_cursor", EmitDefaultValue = false)]
        public string NextPageCursor { get; set; }

        /// <summary>
        ///     The list of archived orders
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public OrderHistory[] Results { get; set; }
    }
}