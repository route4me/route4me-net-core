using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Address
    /// </summary>
    [DataContract]
    public class CustomerAddress
    {
        /// <summary>
        /// Type (billing, shipping, delivery)
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}