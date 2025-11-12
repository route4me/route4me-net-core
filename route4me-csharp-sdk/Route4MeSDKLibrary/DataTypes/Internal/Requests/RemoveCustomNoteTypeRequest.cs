using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameter for the customer removing process.
    /// </summary>
    [DataContract]
    internal sealed class RemoveCustomNoteTypeRequest : GenericParameters
    {
        /// <value>A custom note type ID></value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }
    }
}