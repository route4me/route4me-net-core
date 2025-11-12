using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Request to create a custom note type
    /// </summary>
    [DataContract]
    public class NoteCustomTypeStoreRequest : GenericParameters
    {
        /// <summary>
        ///     Note custom type name (required)
        /// </summary>
        [DataMember(Name = "note_custom_type", EmitDefaultValue = false)]
        public string NoteCustomType { get; set; }

        /// <summary>
        ///     Note custom type values (required)
        /// </summary>
        [DataMember(Name = "note_custom_type_values", EmitDefaultValue = false)]
        public string[] NoteCustomTypeValues { get; set; }

        /// <summary>
        ///     Note custom field type (enum: 1,2,3,4)
        /// </summary>
        [DataMember(Name = "note_custom_field_type", EmitDefaultValue = false)]
        public int? NoteCustomFieldType { get; set; }
    }
}