using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Facilities;

namespace Route4MeSDKLibrary.QueryTypes.V5.Facilities
{
    /// <summary>
    /// Request parameters for updating a facility
    /// </summary>
    [DataContract]
    public class FacilityUpdateRequest : GenericParameters
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
    }
}