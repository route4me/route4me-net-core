using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Request body for address book contracts request
    /// </summary>
    [DataContract]
    public class AddressBookParametersClusteringBodyRequest : GenericParameters
    {
        /// <summary>
        ///     Filter
        /// </summary>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressBookContactsFilter Filter { get; set; }


        /// <summary>
        ///     Filter
        /// </summary>
        [DataMember(Name = "clustering", EmitDefaultValue = false)]
        public Clustering Clustering { get; set; }
    }
}