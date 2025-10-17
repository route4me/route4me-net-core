using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents an assigned facility type
    /// </summary>
    [DataContract]
    public class FacilityTypeRelationResource
    {
        /// <summary>
        /// Gets or sets the facility type id
        /// </summary>
        [DataMember(Name = "facility_type_id", EmitDefaultValue = false)]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the facility type alias
        /// </summary>
        [DataMember(Name = "facility_type_alias", EmitDefaultValue = false)]
        public string FacilityTypeAlias { get; set; }

        /// <summary>
        /// Gets or sets is type marked as default for facility
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }
    }
}
