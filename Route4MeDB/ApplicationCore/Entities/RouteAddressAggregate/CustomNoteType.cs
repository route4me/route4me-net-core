using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    public class CustomNoteType
    {
        /// <summary>
        /// The custom note type ID
        /// </summary>
        [Column("note_custom_type_id")]
        public int NoteCustomTypeID { get; set; }

        /// <summary>
        /// The custom type
        /// </summary>
        [Column("note_custom_type")]
        public string NoteCustomType { get; set; }

        /// <summary>
        /// The root owner member ID
        /// </summary>
        [Column("root_owner_member_id")]
        public int RootOwnerMemberID { get; set; }

        /// <summary>
        /// An array of the custom type note values
        /// </summary>
        [Column("note_custom_type_values")]
        public string NoteCustomTypeValues { get; set; }

        [NotMapped]
        public string[] NoteCustomTypeValuesArray
        {
            get { return NoteCustomTypeValues == null ? null : JsonConvert.DeserializeObject<string[]>(NoteCustomTypeValues); }
            set { NoteCustomTypeValues = JsonConvert.SerializeObject(value); }
        }
    }
}
