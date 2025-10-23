using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Facility type relation for response data
    /// </summary>
    [DataContract]
    public class FacilityTypeRelation
    {
        /// <summary>
        /// Facility type ID
        /// </summary>
        [DataMember(Name = "facility_type_id")]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Facility type alias/name
        /// </summary>
        [DataMember(Name = "facility_type_alias")]
        public string FacilityTypeAlias { get; set; }

        /// <summary>
        /// Is this the default facility type for this facility
        /// </summary>
        [DataMember(Name = "is_default")]
        public bool IsDefault { get; set; }
    }
}

