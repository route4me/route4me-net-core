using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Note custom type resource
    /// </summary>
    [DataContract]
    public class NoteCustomTypeResource
    {
        /// <summary>
        ///     Note custom type ID
        /// </summary>
        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public int? NoteCustomTypeId { get; set; }

        /// <summary>
        ///     Note custom type name
        /// </summary>
        [DataMember(Name = "note_custom_type", EmitDefaultValue = false)]
        public string NoteCustomType { get; set; }

        /// <summary>
        ///     Root owner member ID
        /// </summary>
        [DataMember(Name = "root_owner_member_id", EmitDefaultValue = false)]
        public int? RootOwnerMemberId { get; set; }

        /// <summary>
        ///     Note custom type values
        /// </summary>
        [DataMember(Name = "note_custom_type_values", EmitDefaultValue = false)]
        public string[] NoteCustomTypeValues { get; set; }

        /// <summary>
        ///     Note custom field type (1,2,3,4)
        /// </summary>
        [DataMember(Name = "note_custom_field_type", EmitDefaultValue = false)]
        public int? NoteCustomFieldType { get; set; }
    }

    /// <summary>
    ///     Note custom type collection response
    /// </summary>
    [DataContract]
    public class NoteCustomTypeCollection
    {
        /// <summary>
        ///     Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        /// <summary>
        ///     Response code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int? Code { get; set; }

        /// <summary>
        ///     Array of custom type resources
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public NoteCustomTypeResource[] Data { get; set; }
    }
}
