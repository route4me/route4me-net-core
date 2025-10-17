using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Customer show resource
    /// </summary>
    [DataContract]
    public class CustomerShowResource : CustomerResource
    {
        /// <summary>
        /// Accountable Person FirstName
        /// </summary>
        [DataMember(Name = "accountable_person_first_name")]
        public string AccountablePersonFirstName { get; set; }

        /// <summary>
        /// Accountable Person LastName
        /// </summary>
        [DataMember(Name = "accountable_person_last_name")]
        public string AccountablePersonLastName { get; set; }

        /// <summary>
        /// Customer Location Count
        /// </summary>
        [DataMember(Name = "customer_location_count")]
        public int CustomerLocationCount { get; set; }
    }
}
