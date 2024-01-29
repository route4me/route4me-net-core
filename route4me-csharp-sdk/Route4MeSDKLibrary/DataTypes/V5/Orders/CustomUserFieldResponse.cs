using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Custom user fields response
    /// </summary>
    [DataContract]
    public class CustomUserFieldResponse
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "Data", EmitDefaultValue = false, IsRequired = false)]
        public OrderCustomField Data { get; set; }
    }
}