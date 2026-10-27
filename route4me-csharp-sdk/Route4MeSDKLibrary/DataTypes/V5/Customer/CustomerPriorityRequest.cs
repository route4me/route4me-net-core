using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Request body to create or update a customer importance (priority) level
    /// </summary>
    [DataContract]
    public class CustomerPriorityRequest : GenericParameters
    {
        /// <summary>
        /// Name, unique per account (case-insensitive), up to 64 characters. Required on create and update.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Sort order, 0 or more. Lower is more important.
        /// </summary>
        [DataMember(Name = "sort_order", EmitDefaultValue = false)]
        public int? SortOrder { get; set; }
    }
}