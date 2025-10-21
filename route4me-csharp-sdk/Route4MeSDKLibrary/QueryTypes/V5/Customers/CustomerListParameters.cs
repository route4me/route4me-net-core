using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.QueryTypes.V5.Customers
{
    /// <summary>
    /// Request body for getting list with customers data
    /// </summary>
    [DataContract]
    public class CustomerListParameters : GenericParameters
    {
        /// <summary>
        /// Filters for customers list
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public CustomerListFilters Filters { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public string[] OrderBy { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        /// Items per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        /// Fields to include
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }
}
