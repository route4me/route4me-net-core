using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents a facility resource
    /// </summary>
    [DataContract]
    public class FacilityResource
    {
        /// <summary>
        /// Gets or sets the facility id
        /// </summary>
        [DataMember(Name = "facility_id", EmitDefaultValue = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Gets or sets the facility coordinates
        /// </summary>
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public FacilityCoordinates Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the facility address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the facility alias
        /// </summary>
        [DataMember(Name = "facility_alias", EmitDefaultValue = false)]
        public string FacilityAlias { get; set; }

        /// <summary>
        /// Gets or sets the facility status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public FacilityStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the assigned facility types
        /// </summary>
        [DataMember(Name = "facility_types", EmitDefaultValue = false)]
        public FacilityTypeRelationResource[] FacilityTypes { get; set; }
    }
}
