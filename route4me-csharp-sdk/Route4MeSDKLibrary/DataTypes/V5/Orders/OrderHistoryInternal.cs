using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    ///     The order history
    /// </summary>
    [DataContract]
    internal class OrderHistoryInternal
    {
        /// <summary>
        ///     Timestamp of creation
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long CreatedTimestamp { get; set; }

        /// <summary>
        ///     Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public long OrderId { get; set; }

        /// <summary>
        ///     Event type
        /// </summary>
        [DataMember(Name = "event_type", EmitDefaultValue = false)]
        public int EventType { get; set; }

        /// <summary>
        ///     The id of the member inside the route4me system
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     Route member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        ///     Order's diff
        /// </summary>
        [DataMember(Name = "order_diffs", EmitDefaultValue = false)]
        public string Diffs { get; set; }

        /// <summary>
        ///     Order model
        /// </summary>
        [DataMember(Name = "order_model", EmitDefaultValue = false)]
        public string OrderModel { get; set; }
    }
}