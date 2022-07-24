using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     The parameters for an export by area IDs request of the specified address book contacts.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressExportByAreaIdsParameters : GenericParameters
    {
        /// <summary>
        /// Territory IDs
        /// </summary>
        [DataMember(Name = "territory_ids", EmitDefaultValue = false)]
        public string[] TerritoryIds { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = false)]
        public string Filename { get; set; }
    }
}