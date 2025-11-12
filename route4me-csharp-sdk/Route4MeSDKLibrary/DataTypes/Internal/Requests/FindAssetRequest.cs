using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the find asset process.
    /// </summary>
    [DataContract]
    internal sealed class FindAssetRequest : GenericParameters
    {
        /// <value>The tracking code</value>
        [HttpQueryMemberAttribute(Name = "tracking", EmitDefaultValue = false)]
        public string Tracking { get; set; }
    }
}