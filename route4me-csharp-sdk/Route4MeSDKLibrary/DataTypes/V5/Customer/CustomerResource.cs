using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Customer data
    /// </summary>
    [DataContract]
    public class CustomerResource
    {
        /// <summary>
        /// Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public string CustomerId { get; set; }

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
        /// Creation date (m-d-Y)
        /// </summary>
        [DataMember(Name = "creation_date", EmitDefaultValue = false)]
        public string CreationDate { get; set; }

        /// <summary>
        /// Created timestamp
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long? CreatedTimestamp { get; set; }

        /// <summary>
        /// Created day ID
        /// </summary>
        [DataMember(Name = "created_day_id", EmitDefaultValue = false)]
        public int? CreatedDayId { get; set; }

        /// <summary>
        /// Created by member ID
        /// </summary>
        [DataMember(Name = "created_by_member_id", EmitDefaultValue = false)]
        public long? CreatedByMemberId { get; set; }

        /// <summary>
        /// Updated timestamp
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public long? UpdatedTimestamp { get; set; }

        /// <summary>
        /// Facility IDs
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }

        /// <summary>
        /// Facilities
        /// </summary>
        [DataMember(Name = "facilities", EmitDefaultValue = false)]
        public Facility[] Facilities { get; set; }
    }
}
