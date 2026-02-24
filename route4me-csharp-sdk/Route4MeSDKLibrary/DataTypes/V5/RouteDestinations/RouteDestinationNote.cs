using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// A custom type entry attached to a destination note.
    /// </summary>
    [DataContract]
    public class RouteDestinationNoteCustomType
    {
        /// <summary>
        /// Unique identifier for the custom entry (32-character hex UUID).
        /// </summary>
        [DataMember(Name = "note_custom_entry_id")]
        public string NoteCustomEntryId { get; set; }

        /// <summary>
        /// Identifier for the custom type definition.
        /// </summary>
        [DataMember(Name = "note_custom_type_id")]
        public int? NoteCustomTypeId { get; set; }

        /// <summary>
        /// Value stored for this custom type.
        /// </summary>
        [DataMember(Name = "note_custom_value")]
        public string NoteCustomValue { get; set; }

        /// <summary>
        /// Name/label of the custom type.
        /// </summary>
        [DataMember(Name = "note_custom_type")]
        public string NoteCustomType { get; set; }
    }

    /// <summary>
    /// A note attached to a route destination.
    /// </summary>
    [DataContract]
    public class RouteDestinationNote
    {
        /// <summary>
        /// Unique integer identifier for the note.
        /// </summary>
        [DataMember(Name = "note_id")]
        public int? NoteId { get; set; }

        /// <summary>
        /// UUID for the note (32-character hex string).
        /// </summary>
        [DataMember(Name = "note_uuid")]
        public string NoteUuid { get; set; }

        /// <summary>
        /// UUID of the associated file upload (32-character hex string).
        /// </summary>
        [DataMember(Name = "upload_id")]
        public string UploadId { get; set; }

        /// <summary>
        /// ISO 8601 timestamp when the note was added.
        /// </summary>
        [DataMember(Name = "timestamp_added")]
        public string TimestampAdded { get; set; }

        /// <summary>
        /// Activity type label (e.g., "note", "photo").
        /// </summary>
        [DataMember(Name = "activity_type")]
        public string ActivityType { get; set; }

        /// <summary>
        /// File extension of the uploaded asset (e.g., "jpg", "pdf").
        /// </summary>
        [DataMember(Name = "upload_extension")]
        public string UploadExtension { get; set; }

        /// <summary>
        /// Public URL to access the uploaded file.
        /// </summary>
        [DataMember(Name = "upload_url")]
        public string UploadUrl { get; set; }

        /// <summary>
        /// MIME type category of the upload (e.g., "image", "document").
        /// </summary>
        [DataMember(Name = "upload_type")]
        public string UploadType { get; set; }

        /// <summary>
        /// Text content of the note.
        /// </summary>
        [DataMember(Name = "contents")]
        public string Contents { get; set; }

        /// <summary>
        /// Latitude where the note was recorded.
        /// </summary>
        [DataMember(Name = "lat")]
        public float? Lat { get; set; }

        /// <summary>
        /// Longitude where the note was recorded.
        /// </summary>
        [DataMember(Name = "lng")]
        public float? Lng { get; set; }

        /// <summary>
        /// Type of device used to add the note (e.g., "mobile", "web").
        /// </summary>
        [DataMember(Name = "device_type")]
        public string DeviceType { get; set; }

        /// <summary>
        /// Additional structured custom type entries associated with this note.
        /// </summary>
        [DataMember(Name = "custom_types")]
        public List<RouteDestinationNoteCustomType> CustomTypes { get; set; }
    }
}