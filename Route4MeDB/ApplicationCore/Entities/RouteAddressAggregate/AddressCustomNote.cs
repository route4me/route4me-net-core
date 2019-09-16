using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.GeocodingAggregate;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    public class AddressCustomNote : IAggregateRoot
    {
        /// <summary>
        /// A unique ID (40 chars) of a custom note entry.
        /// </summary>
        [Column("note_custom_entry_id")]
        public int NoteCustomEntryID { get; set; }

        /// <summary>
        /// The custom note ID.
        /// </summary>
        [Column("note_id")]
        public string NoteID { get; set; }

        /// <summary>
        /// The custom note type ID.
        /// </summary>
        [Column("note_custom_type_id")]
        public int NoteCustomTypeID { get; set; }

        /// <summary>
        /// The custom note value.
        /// </summary>
        [Column("note_custom_value")]
        public string NoteCustomValue { get; set; }

        /// <summary>
        /// The custom note type.
        /// </summary>
        [Column("note_custom_type")]
        public string NoteCustomType { get; set; }
    }
}
