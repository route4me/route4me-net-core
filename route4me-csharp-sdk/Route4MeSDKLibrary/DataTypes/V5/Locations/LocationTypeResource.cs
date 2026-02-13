using System.Runtime.Serialization;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Location type resource
    /// Defines a categorization type for locations
    /// </summary>
    [DataContract]
    public class LocationTypeResource
    {
        /// <summary>
        /// Unique identifier for the location type
        /// </summary>
        [DataMember(Name = "location_type_id")]
        public string LocationTypeId { get; set; }

        /// <summary>
        /// Name of the location type
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the location type
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates if this type can have child locations
        /// </summary>
        [DataMember(Name = "is_parent_type")]
        public bool? IsParentType { get; set; }

        /// <summary>
        /// Indicates if this type can be a child of another location
        /// </summary>
        [DataMember(Name = "is_child_type")]
        public bool? IsChildType { get; set; }

        /// <summary>
        /// Root member ID that owns this location type
        /// </summary>
        [DataMember(Name = "root_member_id")]
        public int? RootMemberId { get; set; }
    }

    [DataContract]
    public class LocationTypeResponse
    {
        /// <summary>
        /// Location type resource
        /// </summary>
        [DataMember(Name = "data")]
        public LocationTypeResource Data { get; set; }

    }

    /// <summary>
    /// Collection of location types with pagination
    /// </summary>
    [DataContract]
    public class LocationTypeCollection
    {
        /// <summary>
        /// Array of location type resources
        /// </summary>
        [DataMember(Name = "data")]
        public LocationTypeResource[] Data { get; set; }

        /// <summary>
        /// Pagination metadata (current page, total pages, etc.)
        /// </summary>
        [DataMember(Name = "meta")]
        public PageMeta Meta { get; set; }

        /// <summary>
        /// Pagination links (first, last, next, prev)
        /// </summary>
        [DataMember(Name = "links")]
        public PageLinks Links { get; set; }
    }
}