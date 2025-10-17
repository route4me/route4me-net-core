using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Facilities
{
    /// <summary>
    /// Represents query parameters to get facilities  
    /// </summary>
    [DataContract]
    public class FacilityGetParameters : GenericParameters
    {
        /// <summary>
        /// Gets or sets a text search query
        /// </summary>
        [HttpQueryMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the current page number
        /// </summary>
        [HttpQueryMember(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        /// Gets or sets facility count per page
        /// </summary>
        [HttpQueryMember(Name = "per_page", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }
    }
}
