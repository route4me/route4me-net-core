using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Response model for the customer importance (priority) list
    /// </summary>
    [DataContract]
    public class CustomerPriorityListResponse
    {
        /// <summary>
        /// Customer priorities
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public CustomerPriorityResource[] Data { get; set; }
    }
}