using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Customer store request
    /// </summary>
    [DataContract]
    public class StoreRequest : GenericParameters
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// External ID
        /// </summary>
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Contacts
        /// </summary>
        [DataMember(Name = "contacts", EmitDefaultValue = false)]
        public Contact[] Contacts { get; set; }

        /// <summary>
        /// Accountable person ID
        /// </summary>
        [DataMember(Name = "accountable_person_id", EmitDefaultValue = false)]
        public long? AccountablePersonId { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public CustomerAddress[] Addresses { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Tax ID
        /// </summary>
        [DataMember(Name = "tax_id", EmitDefaultValue = false)]
        public string TaxId { get; set; }

        /// <summary>
        /// Status (1)
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status { get; set; }

        /// <summary>
        /// Contracts
        /// </summary>
        [DataMember(Name = "contracts", EmitDefaultValue = false)]
        public Contract[] Contracts { get; set; }

        /// <summary>
        /// Facility IDs
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }
    }
}
