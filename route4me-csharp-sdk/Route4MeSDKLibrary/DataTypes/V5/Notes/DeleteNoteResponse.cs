using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Response for note deletion
    /// </summary>
    [DataContract]
    public class DeleteNoteResponse
    {
        /// <summary>
        ///     Status of deletion
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public bool Status { get; set; }

        /// <summary>
        ///     Deleted note ID
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public int? NoteId { get; set; }
    }
}