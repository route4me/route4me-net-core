using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameter containing the array of the order IDs.
    /// </summary>
    [DataContract]
    internal sealed class RemoveOrdersRequest : GenericParameters
    {
        /// <value>The array of the order IDs</value>
        [DataMember(Name = "order_ids", EmitDefaultValue = false)]
        public string[] OrderIds { get; set; }
    }
}