using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Customer list resource
    /// </summary>
    [DataContract]
    public class CustomerListResource
    {
        /// <summary>
        /// Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// External ID
        /// </summary>
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Accountable person first name
        /// </summary>
        [DataMember(Name = "accountable_person_first_name", EmitDefaultValue = false)]
        public string AccountablePersonFirstName { get; set; }

        /// <summary>
        /// Accountable person last name
        /// </summary>
        [DataMember(Name = "accountable_person_last_name", EmitDefaultValue = false)]
        public string AccountablePersonLastName { get; set; }

        /// <summary>
        /// Number of customer locations
        /// </summary>
        [DataMember(Name = "customer_location_count", EmitDefaultValue = false)]
        public int? CustomerLocationCount { get; set; }

        /// <summary>
        /// Status (1)
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Creation date (UTC string)
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
        public int? CreatedByMemberId { get; set; }

        /// <summary>
        /// Updated timestamp
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public long? UpdatedTimestamp { get; set; }

        /// <summary>
        /// Facility IDs
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public List<string> FacilityIds { get; set; }

        /// <summary>
        /// Facilities
        /// </summary>
        [DataMember(Name = "facilities", EmitDefaultValue = false)]
        public string Facilities { get; set; }

        /// <summary>
        /// Contacts
        /// </summary>
        [DataMember(Name = "contacts", EmitDefaultValue = false)]
        public List<CustomerContactResource> Contacts { get; set; }
    }
}
