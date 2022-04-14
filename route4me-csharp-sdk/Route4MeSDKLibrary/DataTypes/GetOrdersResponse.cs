using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response for the orders getting process.
    /// </summary>
    [DataContract]
    public sealed class GetOrdersResponse
    {
        /// <value>
        ///     An arrary of the objects
        ///     Available types of the array item: Order (default),
        ///     object[] (search by fields)
        /// </value>
        [DataMember(Name = "results")]
        public Order[] Results { get; set; }

        /// <value>Number of the returned orders</value>
        [DataMember(Name = "total")]
        public uint Total { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}
