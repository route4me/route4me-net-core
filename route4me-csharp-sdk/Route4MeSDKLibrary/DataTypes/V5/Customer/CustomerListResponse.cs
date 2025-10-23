using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Response model for customer list
    /// </summary>
    [DataContract]
    public class CustomerListResponse
    {
        /// <summary>
        /// List of customers
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public List<CustomerListResource> Items { get; set; }
    }
}
