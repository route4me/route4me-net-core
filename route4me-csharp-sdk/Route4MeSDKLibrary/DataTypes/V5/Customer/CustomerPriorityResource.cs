using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Customer importance (priority) level
    /// </summary>
    [DataContract]
    public class CustomerPriorityResource
    {
        /// <summary>
        /// Customer priority ID
        /// </summary>
        [DataMember(Name = "customer_priority_id", EmitDefaultValue = false)]
        public string CustomerPriorityId { get; set; }

        /// <summary>
        /// Name, unique per account
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Sort order. Lower is more important. Null when not set.
        /// </summary>
        [DataMember(Name = "sort_order", EmitDefaultValue = false)]
        public int? SortOrder { get; set; }
    }
}