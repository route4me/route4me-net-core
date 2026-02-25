using System.Collections.Generic;
using System.Runtime.Serialization;

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
        /// <summary>Unique integer identifier for the facility.</summary>
        [DataMember(Name = "facility_id")]
        public int? FacilityId { get; set; }

        /// <summary>Human-readable alias for the facility (e.g., "Main Warehouse").</summary>
        [DataMember(Name = "facility_alias")]
        public string FacilityAlias { get; set; }

        /// <summary>Status label for the facility (e.g., "Active").</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>Short status code (e.g., "A").</summary>
        [DataMember(Name = "status_code")]
        public string StatusCode { get; set; }

        /// <summary>Coordinates of the facility formatted as "lat,lng" (e.g., "49.8397,24.0297").</summary>
        [DataMember(Name = "coordinates")]
        public string Coordinates { get; set; }

        /// <summary>Street address of the facility.</summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>List of facility types associated with this facility.</summary>
        [DataMember(Name = "facility_types")]
        public List<FacilityType> FacilityTypes { get; set; }
    }
}