using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Customers
{
    /// <summary>
    /// Filters for CustomerListParameters
    /// </summary>
    [DataContract]
    public class CustomerListFilters
    {
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        [DataMember(Name = "view_mode", EmitDefaultValue = false)]
        public string ViewMode { get; set; }

        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long[] RootMemberId { get; set; }

        [DataMember(Name = "customer_id", EmitDefaultValue = false)]
        public string[] CustomerId { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string[] Name { get; set; }

        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string[] ExternalId { get; set; }

        [DataMember(Name = "accountable_person_id", EmitDefaultValue = false)]
        public long[] AccountablePersonId { get; set; }

        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string[] Currency { get; set; }

        [DataMember(Name = "tax_id", EmitDefaultValue = false)]
        public string[] TaxId { get; set; }

        [DataMember(Name = "created_by_member_id", EmitDefaultValue = false)]
        public long[] CreatedByMemberId { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int[] Status { get; set; }

        [DataMember(Name = "creation_date", EmitDefaultValue = false)]
        public string[] CreationDate { get; set; }

        [DataMember(Name = "customer_location_count", EmitDefaultValue = false)]
        public int[] CustomerLocationCount { get; set; }

        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }
    }
}