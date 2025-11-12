using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the custom note type adding process.
    /// </summary>
    [DataContract]
    internal sealed class AddCustomNoteTypeRequest : GenericParameters
    {
        /// <value>The custom note type</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <value>An array of the custom note values</value>
        [DataMember(Name = "values", EmitDefaultValue = false)]
        public string[] Values { get; set; }
    }
}