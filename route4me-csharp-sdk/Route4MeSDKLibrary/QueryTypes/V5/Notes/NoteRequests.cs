using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Request to create a note
    /// </summary>
    [DataContract]
    public class NoteStoreRequest : GenericParameters
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
    }

    /// <summary>
    ///     Request to update a note
    /// </summary>
    [DataContract]
    public class NoteUpdateRequest : GenericParameters
    {
        /// <summary>
        ///     Note contents
        /// </summary>
        [DataMember(Name = "strNoteContents", EmitDefaultValue = false)]
        public string StrNoteContents { get; set; }

        /// <summary>
        ///     Update type (enum, nullable)
        /// </summary>
        [DataMember(Name = "strUpdateType", EmitDefaultValue = false)]
        public string StrUpdateType { get; set; }

        /// <summary>
        ///     File attachment (binary)
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
    }

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

    /// <summary>
    ///     Parameters for getting notes by route
    /// </summary>
    public class RouteNotesParameters : GenericParameters
    {
        /// <summary>
        ///     Route ID (32-char hex string)
        /// </summary>
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Page number
        /// </summary>
        [HttpQueryMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        ///     Items per page
        /// </summary>
        [HttpQueryMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }
    }

    /// <summary>
    ///     Parameters for getting notes by destination
    /// </summary>
    public class DestinationNotesParameters : GenericParameters
    {
        /// <summary>
        ///     Route destination ID (integer or 32-char hex string)
        /// </summary>
        [HttpQueryMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public string RouteDestinationId { get; set; }

        /// <summary>
        ///     Page number
        /// </summary>
        [HttpQueryMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        ///     Items per page
        /// </summary>
        [HttpQueryMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }
    }
}
