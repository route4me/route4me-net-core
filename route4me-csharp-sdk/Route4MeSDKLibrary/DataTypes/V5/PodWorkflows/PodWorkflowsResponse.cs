using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.PodWorkflows
{
    /// <summary>
    /// POD Workflows Response
    /// </summary>
    [DataContract]
    public class PodWorkflowsResponse
    {
        /// <summary>
        /// POD Workflows
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public PodWorkflow[] Data { get; set; }

        /// <summary>
        /// Next page cursor
        /// </summary>
        [DataMember(Name = "next_page_cursor", EmitDefaultValue = false)]
        public string NextPageCursor { get; set; }

        /// <summary>
        /// Total Items Count
        /// </summary>
        [DataMember(Name = "total_items_count", EmitDefaultValue = false)]
        public int TotalItemsCount { get; set; }
    }
}