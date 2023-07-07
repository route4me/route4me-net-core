using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     The parameters for an export request of the specified address book contacts.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressExportParameters : GenericParameters
    {
        /// <summary>
        /// An array of the address IDs.
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public long[] AddressIds { get; set; }

        /// <summary>
        /// A file name
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = false)]
        public string Filename { get; set; }
    }
}