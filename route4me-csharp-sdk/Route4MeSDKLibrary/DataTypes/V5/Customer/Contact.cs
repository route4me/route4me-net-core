using System.Runtime.Serialization;

using Newtonsoft.Json.Linq;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Contact
    /// </summary>
    [DataContract]
    public class Contact
    {
        /// <summary>
        /// Type (primary, billing, sales, shipping, technical, returns, customer_service, maintenance, operations, other)
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public JObject CustomData { get; set; }
    }
}