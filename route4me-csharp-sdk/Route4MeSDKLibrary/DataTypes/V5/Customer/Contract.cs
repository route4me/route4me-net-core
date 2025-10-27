using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Contract
    /// </summary>
    [DataContract]
    public class Contract
    {
        /// <summary>
        /// Start date (m-d-Y)
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }

        /// <summary>
        /// Expiration (m-d-Y)
        /// </summary>
        [DataMember(Name = "expiration", EmitDefaultValue = false)]
        public string Expiration { get; set; }
    }
}
