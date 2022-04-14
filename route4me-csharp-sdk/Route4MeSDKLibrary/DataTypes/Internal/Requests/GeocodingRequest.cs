using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the geocoding process.
    /// </summary>
    [DataContract]
    internal sealed class GeocodingRequest : GenericParameters
    {
        /// <value>The list of the addresses delimited by the symbol '|'</value>
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public string Addresses { get; set; }

        /// <value>The response format (son, xml)</value>
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }
    }
}
