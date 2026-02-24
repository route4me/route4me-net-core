using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Metadata for a single filterable/sortable field on a route destination.
    /// Returned by GET /route-destinations/list/fields.
    /// </summary>
    [DataContract]
    public class RouteDestinationFieldItem
    {
        /// <summary>Human-readable label for the field (e.g., "destination uuid").</summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>Machine-readable field name used in filter/sort requests (e.g., "destination_uuid").</summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>Field kind: "static" for built-in fields, "custom" for user-defined fields.</summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>Data type of the field (e.g., "string", "integer", "date").</summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Response wrapper for GET /route-destinations/list/fields.
    /// </summary>
    [DataContract]
    public class RouteDestinationFieldsResponse
    {
        /// <summary>Array of field definitions available for filtering and sorting.</summary>
        [DataMember(Name = "fields")]
        public RouteDestinationFieldItem[] Fields { get; set; }
    }
}