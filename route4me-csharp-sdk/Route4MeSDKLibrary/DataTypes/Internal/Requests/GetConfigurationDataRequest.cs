using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Requests
{
    /// <summary>
    ///     The request parameters for the configuration data request process.
    /// </summary>
    [DataContract]
    internal sealed class GetConfigurationDataRequest : GenericParameters
    {
        /// <value>A user's configuration key</value>
        [HttpQueryMemberAttribute(Name = "config_key", EmitDefaultValue = false)]
        public string ConfigKey { get; set; }
    }
}
