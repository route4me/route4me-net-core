using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from an address marking process as departed.
    /// </summary>
    [DataContract]
    internal sealed class MarkAddressDepartedResponse
    {
        /// <value>If true marking process finished successfully</value>
        [System.Runtime.Serialization.DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <value>The error string</value>
        [System.Runtime.Serialization.DataMember(Name = "error")]
        public string Error { get; set; }
    }
}