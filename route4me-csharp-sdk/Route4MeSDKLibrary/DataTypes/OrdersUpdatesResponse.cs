using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    /// Orders updates response
    /// </summary>
    [DataContract]
    public class OrdersUpdatesResponse
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public OrderUpdateData[] Data { get; set; }

    }

    /// <summary>
    /// Order update data
    /// </summary>
    [DataContract]
    public class OrderUpdateData
    {
        /// <summary>
        ///     Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public long? OrderId { get; set; }

        /// <summary>
        ///     Updated time stamp (unix timestamp, seconds)
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public string UpdatedTimestamp { get; set; }

        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "EXT_FIELD_custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> Data { get; set; }
    }
}