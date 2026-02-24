using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Response for GET /route-destinations/columns.
    /// Describes available columns and their current display order.
    /// </summary>
    [DataContract]
    public class RouteDestinationColumnsResource
    {
        /// <summary>
        /// Grouped column definitions organised by category.
        /// </summary>
        [DataMember(Name = "categories")]
        public RouteDestinationColumnCategory[] Categories { get; set; }

        /// <summary>
        /// Current column order as an array of column ID strings.
        /// </summary>
        [DataMember(Name = "order")]
        public string[] Order { get; set; }

        /// <summary>
        /// Default column order as an array of column ID strings.
        /// </summary>
        [DataMember(Name = "default_order")]
        public string[] DefaultOrder { get; set; }
    }

    /// <summary>
    /// A category grouping related columns in the columns configuration.
    /// </summary>
    [DataContract]
    public class RouteDestinationColumnCategory
    {
        /// <summary>Display name of the category (e.g., "Destination Info").</summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>Columns belonging to this category.</summary>
        [DataMember(Name = "columns")]
        public RouteDestinationColumnItem[] Columns { get; set; }
    }

    /// <summary>
    /// An individual column entry within a category.
    /// </summary>
    [DataContract]
    public class RouteDestinationColumnItem
    {
        /// <summary>Machine-readable column identifier (e.g., "destination_alias").</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>Human-readable column title (e.g., "Destination Alias").</summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    /// <summary>
    /// Request body for PUT /route-destinations/columns.
    /// </summary>
    [DataContract]
    public class EditDestinationColumnsRequest
    {
        /// <summary>
        /// The configuration key that identifies which column set to update.
        /// </summary>
        [DataMember(Name = "columns_configurator_key")]
        public string ColumnsConfiguratorKey { get; set; }

        /// <summary>
        /// Ordered array of column ID strings representing the desired column order.
        /// </summary>
        [DataMember(Name = "order")]
        public string[] Order { get; set; }
    }

    /// <summary>
    /// Response for PUT /route-destinations/columns.
    /// </summary>
    [DataContract]
    public class EditDestinationColumnsResponse
    {
        /// <summary>Updated column order as saved by the API.</summary>
        [DataMember(Name = "order")]
        public string[] Order { get; set; }
    }
}
