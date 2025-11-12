using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Request to update a note
    /// </summary>
    [DataContract]
    public class NoteUpdateRequest : GenericParameters
    {
        /// <summary>
        ///     Note contents
        /// </summary>
        [DataMember(Name = "strNoteContents", EmitDefaultValue = false)]
        public string StrNoteContents { get; set; }

        /// <summary>
        ///     Update type (enum, nullable)
        /// </summary>
        [DataMember(Name = "strUpdateType", EmitDefaultValue = false)]
        public string StrUpdateType { get; set; }

        /// <summary>
        ///     File attachment (binary)
        /// </summary>
        [DataMember(Name = "file", EmitDefaultValue = false)]
        public string File { get; set; }

        /// <summary>
        ///     Device type (enum)
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        /// <summary>
        ///     Custom note types (array, nullable)
        /// </summary>
        [DataMember(Name = "custom_note_type", EmitDefaultValue = false)]
        public int[] CustomNoteType { get; set; }
    }
}