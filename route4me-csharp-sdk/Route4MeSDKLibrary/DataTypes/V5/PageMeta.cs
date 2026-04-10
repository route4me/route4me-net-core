using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Meta info of the paged response.
    /// </summary>
    [DataContract]
    public sealed class PageMeta
    {
        /// <summary>
        ///     Current page number
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     From page
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int? From { get; set; }

        /// <summary>
        ///     Last page
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        ///     URL to the last page
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        ///     Per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        ///     To page
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int? To { get; set; }

        /// <summary>
        ///     Total retrieved records.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}