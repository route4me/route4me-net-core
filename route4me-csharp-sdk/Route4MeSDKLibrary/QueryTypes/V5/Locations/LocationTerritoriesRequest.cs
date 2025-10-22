using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for location territories
    /// Retrieves territories with location counts and supports pagination
    /// </summary>
    [DataContract]
    public class LocationTerritoriesRequest : LocationCombinedRequest
    {
        /// <summary>
        /// Page number for territories pagination (1-based)
        /// </summary>
        [DataMember(Name = "territories_page", EmitDefaultValue = false)]
        public int? TerritoriesPage { get; set; }

        /// <summary>
        /// Number of territories per page
        /// </summary>
        [DataMember(Name = "territories_per_page", EmitDefaultValue = false)]
        public int? TerritoriesPerPage { get; set; }

        /// <summary>
        /// Search query to filter territories by name
        /// </summary>
        [DataMember(Name = "territories_search_query", EmitDefaultValue = false)]
        public string TerritoriesSearchQuery { get; set; }

        /// <summary>
        /// Sort order for territories as array of [field, direction] pairs
        /// </summary>
        [DataMember(Name = "territories_order_by", EmitDefaultValue = false)]
        public string[][] TerritoriesOrderBy { get; set; }
    }
}
