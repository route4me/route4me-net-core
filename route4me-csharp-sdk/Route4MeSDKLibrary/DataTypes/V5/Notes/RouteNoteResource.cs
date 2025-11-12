using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
    /// <summary>
    ///     Route note resource response item
    /// </summary>
    [DataContract]
    public class RouteNoteResource
    {
        /// <summary>
        ///     Note ID
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public int? NoteId { get; set; }

        /// <summary>
        ///     Note UUID (32-char hex string)
        /// </summary>
        [DataMember(Name = "note_uuid", EmitDefaultValue = false)]
        public string NoteUuid { get; set; }

        /// <summary>
        ///     Upload ID (32-char hex string)
        /// </summary>
        [DataMember(Name = "upload_id", EmitDefaultValue = false)]
        public string UploadId { get; set; }

        /// <summary>
        ///     Route ID (32-char hex string)
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int? RouteDestinationId { get; set; }

        /// <summary>
        ///     Destination UUID (32-char hex string)
        /// </summary>
        [DataMember(Name = "destination_uuid", EmitDefaultValue = false)]
        public string DestinationUuid { get; set; }

        /// <summary>
        ///     Timestamp when note was added (formatted string)
        /// </summary>
        [DataMember(Name = "ts_added", EmitDefaultValue = false)]
        public string TsAdded { get; set; }

        /// <summary>
        ///     Activity type
        /// </summary>
        [DataMember(Name = "activity_type", EmitDefaultValue = false)]
        public string ActivityType { get; set; }

        /// <summary>
        ///     Upload extension
        /// </summary>
        [DataMember(Name = "upload_extension", EmitDefaultValue = false)]
        public string UploadExtension { get; set; }

        /// <summary>
        ///     Upload URL
        /// </summary>
        [DataMember(Name = "upload_url", EmitDefaultValue = false)]
        public string UploadUrl { get; set; }

        /// <summary>
        ///     Upload type
        /// </summary>
        [DataMember(Name = "upload_type", EmitDefaultValue = false)]
        public string UploadType { get; set; }

        /// <summary>
        ///     Note contents/text
        /// </summary>
        [DataMember(Name = "contents", EmitDefaultValue = false)]
        public string Contents { get; set; }

        /// <summary>
        ///     Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        ///     Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }

        /// <summary>
        ///     Device type
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        /// <summary>
        ///     Custom note types
        /// </summary>
        [DataMember(Name = "custom_types", EmitDefaultValue = false)]
        public CustomNoteTypeValue[] CustomTypes { get; set; }
    }
}