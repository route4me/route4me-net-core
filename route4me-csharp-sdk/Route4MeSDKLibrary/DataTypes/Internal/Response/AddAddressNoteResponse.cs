using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from the address note adding process.
    /// </summary>
    [DataContract]
    internal sealed class AddAddressNoteResponse
    {
        /// <value>If true an address note added successfuly</value>
        [System.Runtime.Serialization.DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <value>The address note ID</value>
        [System.Runtime.Serialization.DataMember(Name = "note_id")]
        public string NoteID { get; set; }

        /// <value>The upload ID</value>
        [System.Runtime.Serialization.DataMember(Name = "upload_id")]
        public string UploadID { get; set; }

        /// <value>The AddressNote type object</value>
        [System.Runtime.Serialization.DataMember(Name = "note")]
        public AddressNote Note { get; set; }
    }
}