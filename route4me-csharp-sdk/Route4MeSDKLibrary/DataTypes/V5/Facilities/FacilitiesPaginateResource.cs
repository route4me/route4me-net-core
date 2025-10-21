using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Paginated facilities response
    /// </summary>
    [DataContract]
    public class FacilitiesPaginateResource
    {
        /// <summary>
        /// Array of facilities
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public FacilityResource[] Data { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        /// Total number of facilities
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }

        /// <summary>
        /// Number of facilities per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        /// Last page number
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        /// URL to the next page
        /// </summary>
        [DataMember(Name = "next_page_url", EmitDefaultValue = false)]
        public string NextPageUrl { get; set; }

        /// <summary>
        /// URL to the previous page
        /// </summary>
        [DataMember(Name = "prev_page_url", EmitDefaultValue = false)]
        public string PrevPageUrl { get; set; }

        /// <summary>
        /// Starting item number on current page
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int? From { get; set; }

        /// <summary>
        /// Ending item number on current page
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int? To { get; set; }
    }
}

