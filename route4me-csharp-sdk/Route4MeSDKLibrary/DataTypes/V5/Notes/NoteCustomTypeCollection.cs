using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.Notes
{
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
