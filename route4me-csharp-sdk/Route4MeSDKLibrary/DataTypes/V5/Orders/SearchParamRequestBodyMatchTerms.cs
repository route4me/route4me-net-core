using System.Runtime.Serialization;

using Newtonsoft.Json.Linq;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Search Param Request Body Matches
    /// </summary>
    [DataContract]
    public class SearchParamRequestBodyMatchTerms
    {
        /// <summary>
        ///     Custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public JObject CustomData { get; set; }

        /// <summary>
        ///     First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        ///     Phone
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        ///     Address 1
        /// </summary>
        [DataMember(Name = "address_1", EmitDefaultValue = false)]
        public string Address1 { get; set; }

        /// <summary>
        ///     Address zip
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string AddressZip { get; set; }

        /// <summary>
        ///     Address alias
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string AddressAlias { get; set; }

    }
}