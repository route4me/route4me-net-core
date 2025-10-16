using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;

namespace Route4MeSDKLibrary.QueryTypes.V5.Facilities
{
    /// <summary>
    /// Request parameters for creating a facility
    /// </summary>
    [DataContract]
    public class FacilityCreateRequest : GenericParameters
    {
        /// <summary>
        /// Facility alias/name
        /// </summary>
        [DataMember(Name = "facility_alias")]
        public string FacilityAlias { get; set; }

        /// <summary>
        /// Facility address
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Facility coordinates
        /// </summary>
        [DataMember(Name = "coordinates")]
        public FacilityCoordinates Coordinates { get; set; }

        /// <summary>
        /// Facility types array
        /// </summary>
        [DataMember(Name = "facility_types")]
        public FacilityTypeAssignment[] FacilityTypes { get; set; }

        /// <summary>
        /// Facility status: 1=ACTIVE, 2=INACTIVE, 3=IN_REVIEW
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Contact person first name
        /// </summary>
        [DataMember(Name = "contact_person_first_name", EmitDefaultValue = false)]
        public string ContactPersonFirstName { get; set; }

        /// <summary>
        /// Contact person last name
        /// </summary>
        [DataMember(Name = "contact_person_last_name", EmitDefaultValue = false)]
        public string ContactPersonLastName { get; set; }

        /// <summary>
        /// Contact person email
        /// </summary>
        [DataMember(Name = "contact_person_email", EmitDefaultValue = false)]
        public string ContactPersonEmail { get; set; }

        /// <summary>
        /// Contact person phone
        /// </summary>
        [DataMember(Name = "contact_person_phone", EmitDefaultValue = false)]
        public string ContactPersonPhone { get; set; }
    }

    /// <summary>
    /// Facility type assignment for create/update requests
    /// </summary>
    [DataContract]
    public class FacilityTypeAssignment
    {
        /// <summary>
        /// Facility type ID
        /// </summary>
        [DataMember(Name = "facility_type_id")]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Is this the default facility type for this facility
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool? IsDefault { get; set; }
    }
}

