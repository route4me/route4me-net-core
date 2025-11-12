using System.Runtime.Serialization;

using Route4MeSDKLibrary.DataTypes.V5.AddressBookContact;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     The parameters for an export by areas request of the specified address book contacts.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressExportByAreasParameters : GenericParameters
    {
        /// <summary>
        /// Filter
        /// </summary>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public AddressExportByAreasFilter Filter { get; set; }
    }

    /// <summary>
    ///     The parameters for an export request of the specified address book contacts.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class AddressExportByAreasFilter : GenericParameters
    {
        /// <summary>
        ///     Query string.
        /// </summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        ///     Bounding box
        /// </summary>
        [DataMember(Name = "bounding_box", EmitDefaultValue = false)]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        ///     Selected Areas
        /// </summary>
        [DataMember(Name = "selected_areas", EmitDefaultValue = false)]
        public SelectedArea[] SelectedAreas { get; set; }

        /// <summary>
        /// A file name
        /// </summary>
        [DataMember(Name = "filename", EmitDefaultValue = false)]
        public string Filename { get; set; }
    }
}