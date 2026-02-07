using System.Runtime.Serialization;

using Route4MeSDKLibrary.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Base class for list requests with pagination
    /// </summary>
    [DataContract]
    public class BaseListRequest : GenericParameters
    {
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

        /// <summary>
        /// Timezone for date/time formatting
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        /// Sorting configuration [["field", "asc|desc"], ...]
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public string[][] OrderBy { get; set; }
    }
}
