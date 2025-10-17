using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents a facility type resource
    /// </summary>
    [DataContract]
    public class FacilityTypeResource : GenericParameters
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
    }
}
