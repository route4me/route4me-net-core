using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Facilities
{
    /// <summary>
    /// Represents a request to create a new facility resource
    /// </summary>
    [DataContract]
    public class FacilityCreateRequest : GenericParameters
    {
        /// <summary>
        /// Gets or sets the facility alias
        /// </summary>
        [DataMember(Name = "facility_alias", EmitDefaultValue = false)]
        public string FacilityAlias { get; set; }

        /// <summary>
        /// Gets or sets the facility address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the facility coordinates
        /// </summary>
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public FacilityCoordinates Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the facility status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public FacilityStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the assigned facility types
        /// </summary>
        [DataMember(Name = "facility_types", EmitDefaultValue = false)]
        public FacilityTypeAssignmentResource[] FacilityTypes { get; set; }
    }
}
