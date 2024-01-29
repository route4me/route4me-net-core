using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Custom user fields response
    /// </summary>
    [DataContract]
    public class CustomUserFieldsResponse
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public OrderCustomField[] Data { get; set; }
    }
}