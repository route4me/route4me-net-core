using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the get address book contacts request
    /// </summary>
    [DataContract]
    public sealed class AddressBookContactsClusteringResponse
    {
        /// <summary>
        ///     An array of the address clusters.
        /// </summary>
        [DataMember(Name = "clusters", EmitDefaultValue = false)]
        public ClusterItem[] Clusters { get; set; }

        /// <summary>
        ///     Total number of the returned address book contacts
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}