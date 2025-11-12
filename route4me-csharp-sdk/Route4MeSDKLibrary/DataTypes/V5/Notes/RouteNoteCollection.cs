using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Route note collection response with pagination
    /// </summary>
    [DataContract]
    public class RouteNoteCollection
    {
        /// <summary>
        ///     Array of note resources
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteNoteResource[] Data { get; set; }

        /// <summary>
        ///     Total count of notes
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }

        /// <summary>
        ///     Current page number
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     Last page number
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        ///     Items per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }
    }
}