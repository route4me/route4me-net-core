﻿using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Customers
{
    /// <summary>
    /// Parameters for customer ID
    /// </summary>
    [DataContract]
    public class CustomerIdParameters : GenericParameters
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        [HttpQueryMember(Name = "customer_id", EmitDefaultValue = false)]
        public string CustomerId { get; set; }
    }
}
