using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Request for bulk creating notes
    /// </summary>
    [DataContract]
    public class NoteStoreBulkRequest : GenericParameters
    {
        /// <summary>
        ///     Array of notes to create
        /// </summary>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public NoteStoreBulkItem[] Notes { get; set; }

        /// <summary>
        ///     Device type (enum)
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }
    }
}
