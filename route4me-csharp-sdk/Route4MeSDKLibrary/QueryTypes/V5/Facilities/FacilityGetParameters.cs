using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Facilities
{
    /// <summary>
    /// Query parameters for retrieving facilities
    /// </summary>
    [DataContract]
    public class FacilityGetParameters : GenericParameters
    {
        /// <summary>
        /// Page number for pagination
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        /// Number of facilities per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        /// Filter by facility status
        /// 1 = ACTIVE, 2 = INACTIVE, 3 = IN_REVIEW
        /// </summary>
        [HttpQueryMemberAttribute(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Filter by facility type ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "facility_type_id", EmitDefaultValue = false)]
        public int? FacilityTypeId { get; set; }

        /// <summary>
        /// Search query string
        /// </summary>
        [HttpQueryMemberAttribute(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }
    }
}