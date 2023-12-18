using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.PodWorkflows
{
    /// <summary>
    ///     Workflows list parameters.
    /// </summary>
    [DataContract]
    public sealed class PodWorkflowListParameters : GenericParameters
    {
        /// <summary>
        /// Per page
        /// </summary>
        [HttpQueryMember(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; }

        /// <summary>
        /// Cursor
        /// </summary>
        [HttpQueryMember(Name = "cursor", EmitDefaultValue = false)]
        public string Cursor { get; set; }

        /// <summary>
        /// Search query
        /// </summary>
        [HttpQueryMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        [HttpQueryMember(Name = "order_by", EmitDefaultValue = false)]
        public List<string[]> OrderBy { get; set; }
    }
}