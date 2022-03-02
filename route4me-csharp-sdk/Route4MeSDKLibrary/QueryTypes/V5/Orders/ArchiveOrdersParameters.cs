using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Parameters for Archive Orders request (reading)
    /// </summary>
    [DataContract]
    public class ArchiveOrdersParameters : GenericParameters
    {
        /// <summary>
        ///     The cursor
        /// </summary>
        [DataMember(Name = "cursor", EmitDefaultValue = false)]
        public string Cursor { get; set; }

        /// <summary>
        ///     Amount of items per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; }

        /// <summary>
        ///     Filters
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public ArchiveOrdersFilters Filters { get; set; }
    }
}