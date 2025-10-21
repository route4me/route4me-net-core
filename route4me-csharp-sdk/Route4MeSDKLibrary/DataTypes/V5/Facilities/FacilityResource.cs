using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Facility resource data structure
    /// </summary>
    [DataContract]
    public class FacilityResource
    {
        /// <summary>
        /// Unique facility identifier (hex32 string)
        /// </summary>
        [DataMember(Name = "facility_id", EmitDefaultValue = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        /// Facility alias/name
        /// </summary>
        [DataMember(Name = "facility_alias", EmitDefaultValue = false)]
        public string FacilityAlias { get; set; }

        /// <summary>
        /// Facility address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Facility coordinates
        /// </summary>
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public FacilityCoordinates Coordinates { get; set; }

        /// <summary>
        /// Facility status: 1=ACTIVE, 2=INACTIVE, 3=IN_REVIEW
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Facility types array
        /// </summary>
        [DataMember(Name = "facility_types", EmitDefaultValue = false)]
        public FacilityTypeRelation[] FacilityTypes { get; set; }

        /// <summary>
        /// Timestamp when facility was created
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public long? CreatedAt { get; set; }

        /// <summary>
        /// Timestamp when facility was last updated
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public long? UpdatedAt { get; set; }
    }
}

