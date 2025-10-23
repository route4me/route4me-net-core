using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Custom note type value in note resource
    /// </summary>
    [DataContract]
    public class CustomNoteTypeValue
    {
        /// <summary>
        ///     Custom entry unique ID
        /// </summary>
        [DataMember(Name = "note_custom_entry_id", EmitDefaultValue = false)]
        public string NoteCustomEntryId { get; set; }

        /// <summary>
        ///     Related note ID
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public int? NoteId { get; set; }

        /// <summary>
        ///     UUID of the note
        /// </summary>
        [DataMember(Name = "note_uuid", EmitDefaultValue = false)]
        public string NoteUuid { get; set; }

        /// <summary>
        ///     Custom type ID
        /// </summary>
        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public int? NoteCustomTypeId { get; set; }

        /// <summary>
        ///     Value of the custom field
        /// </summary>
        [DataMember(Name = "note_custom_value", EmitDefaultValue = false)]
        public string NoteCustomValue { get; set; }

        /// <summary>
        ///     Label or name of the custom type
        /// </summary>
        [DataMember(Name = "note_custom_type", EmitDefaultValue = false)]
        public string NoteCustomType { get; set; }
    }
}
