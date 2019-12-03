using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    [Owned]
    public class AddressNote : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// An unique ID of a note in the database
        /// </summary>
        [Key, Column("note_db_id")]
        public int NoteDbId { get; set; }

        /// <summary>
        /// An unique ID of a note in the Route4me account
        /// Note: in the DB this value could be null
        /// </summary>
        [Column("note_id")]
        public int? NoteId { get; set; }

        //[Column("route_db_id")]
        //public string RouteDbId { get; set; }

        /// <summary>
        /// The route ID
        /// </summary>
        [Column("route_id")]
        public string RouteId { get; set; }

        /// <summary>
        /// The route destination ID
        /// </summary>
        [Column("route_destination_id")]
        public int RouteDestinationId { get; set; }

        /// <summary>
        /// An unique ID of an uploaded file
        /// </summary>
        [Column("upload_id")]
        public string UploadId { get; set; }

        /// <summary>
        /// When the note was added
        /// </summary>
        [Column("ts_added")]
        public long? TimestampAdded { get; set; }

        /// <summary>
        /// The position latitude where the address note was added
        /// </summary>
        [Range(-90, 90)]
        [Column("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// The position longitude where the address note was added
        /// </summary>
        [Range(-180, 180)]
        [Column("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// The activity type
        /// See available activity types here: <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Activity.dtd#L23"/>
        /// The parameter in the note response activity_type is same as StatusUpdateType.
        /// </summary>
        [Column("activity_type")]
        public Enum.StatusUpdateType StatusUpdateType { get; set; }

        /// <summary>
        /// The note text contents
        /// </summary>
        [Column("contents")]
        public string Contents { get; set; }

        /// <summary>
        /// An upload type of the note
        /// </summary>
        [Column("upload_type")]
        public Enum.UploadType UploadType { get; set; }

        /// <summary>
        /// An upload url - where a file-note was uploaded.
        /// </summary>
        [Column("upload_url")]
        public string UploadUrl { get; set; }

        /// <summary>
        /// An extension of the uploaded file.
        /// </summary>
        [Column("upload_extension")]
        public string UploadExtension { get; set; }

        /// <summary>
        /// The device a note was uploaded from
        /// </summary>
        [Column("device_type")]
        public Enum.DeviceType DeviceType { get; set; }

        [Column("custom_types")]
        public string CustomTypes { get; set; }

        [NotMapped]
        public AddressCustomNote[] CustomTypesObj
        {
            get { return CustomTypes == null ? null : JsonConvert.DeserializeObject<AddressCustomNote[]>(CustomTypes); }
            set { CustomTypes = JsonConvert.SerializeObject(value); }
        }

        public int RouteDestinationDbId { get; set; }
        [ForeignKey("FK_AddressNotes_Addresses_RouteDestinationDbId")]
        public Address address { get; set; }

        public string RouteDbId { get; set; }
        [ForeignKey("FK_AddressNotes_Routes_RouteDbId")]
        public Route Route { get; set; }
        //public Route route { get; set; }
    }
}
