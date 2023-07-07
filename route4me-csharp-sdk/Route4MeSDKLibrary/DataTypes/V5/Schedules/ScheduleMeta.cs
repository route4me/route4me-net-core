using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Schedule meta
    /// </summary>
    [DataContract]
    public class ScheduleMeta
    {
        /// <summary>
        ///     Current page
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///    From
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int? From { get; set; }

        /// <summary>
        ///     Last page
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        ///     Path
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        ///     Per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        ///     To
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int? To { get; set; }

        /// <summary>
        ///     Current page
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}