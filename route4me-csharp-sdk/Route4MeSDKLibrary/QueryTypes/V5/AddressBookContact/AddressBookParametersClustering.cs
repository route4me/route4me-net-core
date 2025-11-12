using System.Runtime.Serialization;

using Route4MeSDKLibrary.DataTypes;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// Parameters for the address book contact(s) clustering request
    /// </summary>
    [DataContract]
    public sealed class AddressBookParametersClustering : GenericParameters
    {
        /// <summary>
        /// Specify which Addresses to show in the corresponding query results. (<seealso cref="DisplayValues"/>)
        /// </summary>
        [HttpQueryMemberAttribute(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }

        /// <summary>
        /// Search in the Addresses by the corresponding query phrase.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }
    }
}