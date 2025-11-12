using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Individual note item for bulk creation
    /// </summary>
    [DataContract]
    public class NoteStoreBulkItem
    {
        /// <summary>
        ///     Route ID (required, 32-char hex string)
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Note contents (required)
        /// </summary>
        [DataMember(Name = "strNoteContents", EmitDefaultValue = false)]
        public string StrNoteContents { get; set; }

        /// <summary>
        ///     Address ID (nullable, integer)
        /// </summary>
        [DataMember(Name = "address_id", EmitDefaultValue = false)]
        public long? AddressId { get; set; }

        /// <summary>
        ///     Destination UUID (nullable)
        /// </summary>
        [DataMember(Name = "destination_uuid", EmitDefaultValue = false)]
        public string DestinationUuid { get; set; }

        /// <summary>
        ///     Update type (enum, nullable)
        /// </summary>
        [DataMember(Name = "strUpdateType", EmitDefaultValue = false)]
        public string StrUpdateType { get; set; }

        /// <summary>
        ///     Device latitude (nullable)
        /// </summary>
        [DataMember(Name = "dev_lat", EmitDefaultValue = false)]
        public double? DevLat { get; set; }

        /// <summary>
        ///     Device longitude (nullable)
        /// </summary>
        [DataMember(Name = "dev_lng", EmitDefaultValue = false)]
        public double? DevLng { get; set; }

        /// <summary>
        ///     Remote speed (nullable)
        /// </summary>
        [DataMember(Name = "remote_speed", EmitDefaultValue = false)]
        public double? RemoteSpeed { get; set; }

        /// <summary>
        ///     Remote altitude (nullable)
        /// </summary>
        [DataMember(Name = "remote_altitude", EmitDefaultValue = false)]
        public double? RemoteAltitude { get; set; }

        /// <summary>
        ///     File attachment (binary, nullable)
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

        /// <summary>
        ///     UTC time (unix timestamp in seconds, nullable)
        /// </summary>
        [DataMember(Name = "utc_time", EmitDefaultValue = false)]
        public int? UtcTime { get; set; }

        /// <summary>
        ///     Upload ID (32-char hex string, nullable)
        /// </summary>
        [DataMember(Name = "upload_id", EmitDefaultValue = false)]
        public string UploadId { get; set; }
    }
}