using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from the custom note type adding process.
    /// </summary>
    [DataContract]
    internal sealed class AddCustomNoteTypeResponse
    {
        /// <value>Added custom note</value>
        [DataMember(Name = "result")]
        public string Result { get; set; }

        /// <value>How many destination were affected by adding process</value>
        [DataMember(Name = "affected")]
        public int Affected { get; set; }
    }
}