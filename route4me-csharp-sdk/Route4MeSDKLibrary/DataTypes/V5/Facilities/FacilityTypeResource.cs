using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Facility type resource
    /// </summary>
    [DataContract]
    public class FacilityTypeResource
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
    }
}

