using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Customers;

namespace Route4MeSDKLibrary.QueryTypes.V5.Customers
{
    /// <summary>
    /// Request body for creating a customer
    /// </summary>
    [DataContract]
    public class UpdateCustomerParameters : StoreRequest
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        [HttpQueryMember(Name = "customer_id", EmitDefaultValue = false)]
        public string CustomerId { get; set; }
    }
}