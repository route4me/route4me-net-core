using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Locations
{
    /// <summary>
    /// Request for getting location types
    /// Supports filtering and pagination
    /// </summary>
    [DataContract]
    public class GetLocationTypesRequest : GenericParameters
    {
        /// <summary>
        /// Filter criteria for location types
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public LocationTypeFilters Filters { get; set; }

        /// <summary>
        /// Page number (1-based)
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }
    }

    /// <summary>
    /// Filter criteria for location type queries
    /// </summary>
    [DataContract]
    public class LocationTypeFilters
    {
        /// <summary>
        /// Filter by parent type capability
        /// </summary>
        [DataMember(Name = "is_parent_type", EmitDefaultValue = false)]
        public bool? IsParentType { get; set; }

        /// <summary>
        /// Filter by child type capability
        /// </summary>
        [DataMember(Name = "is_child_type", EmitDefaultValue = false)]
        public bool? IsChildType { get; set; }

        /// <summary>
        /// Return only custom (user-created) location types
        /// </summary>
        [DataMember(Name = "only_custom_types", EmitDefaultValue = false)]
        public bool? OnlyCustomTypes { get; set; }

        /// <summary>
        /// Text search query for location type names
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }
    }

    /// <summary>
    /// Request for creating or updating location type
    /// </summary>
    [DataContract]
    public class StoreLocationTypeRequest : GenericParameters
    {
        /// <summary>
        /// Name of the location type (required)
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the location type
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Whether this type can have child locations
        /// </summary>
        [DataMember(Name = "is_parent_type")]
        public bool IsParentType { get; set; }

        /// <summary>
        /// Whether this type can be a child of another location
        /// </summary>
        [DataMember(Name = "is_child_type")]
        public bool IsChildType { get; set; }
    }

    /// <summary>
    /// Response for location type deletion
    /// </summary>
    [DataContract]
    public class DeleteLocationTypeResponse
    {
        /// <summary>
        /// Indicates whether the deletion was successful
        /// </summary>
        [DataMember(Name = "status")]
        public bool Status { get; set; }
    }
}