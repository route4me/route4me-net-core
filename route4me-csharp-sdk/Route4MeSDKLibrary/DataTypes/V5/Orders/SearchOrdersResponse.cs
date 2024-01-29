using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Search orders response
    /// </summary>
    [DataContract]
    public class SearchOrdersResponse
    {
        /// <summary>
        ///     Orders
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public Order[] Results { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        [DataMember(Name = "total")]
        public uint Total { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}