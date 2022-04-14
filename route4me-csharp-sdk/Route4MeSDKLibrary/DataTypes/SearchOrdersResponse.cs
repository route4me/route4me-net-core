using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response from the orders searching process (contains specified fields).
    /// </summary>
    [DataContract]
    public sealed class SearchOrdersResponse
    {
        /// <value>
        ///     An arrary of the objects
        ///     Available types of the array item: Order (default),
        ///     object[] (search by fields)
        /// </value>
        [DataMember(Name = "results")]
        public IList<object[]> Results { get; set; }

        /// <value>Number of the returned orders</value>
        [DataMember(Name = "total")]
        public uint Total { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}
