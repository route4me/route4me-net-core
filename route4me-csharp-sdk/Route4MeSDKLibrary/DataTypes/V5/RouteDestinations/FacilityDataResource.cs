using System.Collections.Generic;
using System.Runtime.Serialization;

using Route4MeSDKLibrary.DataTypes.V5.Facilities;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// A facility type entry associated with a <see cref="FacilityDataResource"/>.
    /// </summary>
    [DataContract]
    public class FacilityType
    {
        /// <summary>Unique identifier for the facility type.</summary>
        [DataMember(Name = "facility_type_id")]
        public int? FacilityTypeId { get; set; }

        /// <summary>Human-readable alias for the facility type (e.g., "Cold Storage").</summary>
        [DataMember(Name = "facility_type_alias")]
        public string FacilityTypeAlias { get; set; }

        /// <summary>Whether this is the default facility type.</summary>
        [DataMember(Name = "is_default")]
        public bool? IsDefault { get; set; }
    }

    /// <summary>
    /// A facility associated with a route destination.
    /// Returned as part of the <c>facilities</c> array on <see cref="RouteDestinationResource"/>.
    /// </summary>
    [DataContract]
    public class FacilityDataResource
    {
        /// <summary>Facility identifier (32-character hexadecimal UUID).</summary>
        [DataMember(Name = "facility_id")]
        public string FacilityId { get; set; }

        /// <summary>Human-readable alias for the facility (e.g., "Main Warehouse").</summary>
        [DataMember(Name = "facility_alias")]
        public string FacilityAlias { get; set; }

        /// <summary>Status label for the facility (e.g., "Active").</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>Numeric status code (e.g., 1 for Active).</summary>
        [DataMember(Name = "status_code")]
        public int? StatusCode { get; set; }

        /// <summary>Facility coordinates as a nested object with <c>lat</c> and <c>lng</c>.</summary>
        [DataMember(Name = "coordinates")]
        public FacilityCoordinates Coordinates { get; set; }

        /// <summary>Street address of the facility.</summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>List of facility types associated with this facility.</summary>
        [DataMember(Name = "facility_types")]
        public List<FacilityType> FacilityTypes { get; set; }
    }
}