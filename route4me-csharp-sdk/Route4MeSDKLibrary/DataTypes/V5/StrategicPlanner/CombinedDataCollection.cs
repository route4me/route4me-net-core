using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Generic paginated collection wrapper for combined data responses
    /// </summary>
    [DataContract]
    public sealed class CombinedDataCollection
    {
        /// <summary>
        /// Current page index (1-based)
        /// </summary>
        [DataMember(Name = "current_page_index", EmitDefaultValue = false)]
        public int? CurrentPageIndex { get; set; }

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        [DataMember(Name = "total_items_count", EmitDefaultValue = false)]
        public int? TotalItemsCount { get; set; }

        /// <summary>
        /// Subtotals grouped by the group_by field
        /// </summary>
        [DataMember(Name = "groups_subtotals", EmitDefaultValue = false)]
        public object[] GroupsSubtotals { get; set; }

        /// <summary>
        /// Field used for grouping results
        /// </summary>
        [DataMember(Name = "group_by", EmitDefaultValue = false)]
        public string GroupBy { get; set; }

        /// <summary>
        /// Current sorting configuration
        /// </summary>
        [DataMember(Name = "current_sorting", EmitDefaultValue = false)]
        public string[][] CurrentSorting { get; set; }

        /// <summary>
        /// Current filter data applied to the request
        /// </summary>
        [DataMember(Name = "current_filter_data", EmitDefaultValue = false)]
        public object CurrentFilterData { get; set; }

        /// <summary>
        /// Aggregate totals for the entire dataset
        /// </summary>
        [DataMember(Name = "totals", EmitDefaultValue = false)]
        public object Totals { get; set; }
    }
}
