using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents a resource to assign a type to a facility
    /// </summary>
    [DataContract]
    public class FacilityTypeAssignmentResource
    {
        /// <summary>
        /// Gets or sets the facility type id
        /// </summary>
        [DataMember(Name = "facility_type_id", EmitDefaultValue = false)]
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Gets or sets is type marked as default for facility
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }
    }
}
