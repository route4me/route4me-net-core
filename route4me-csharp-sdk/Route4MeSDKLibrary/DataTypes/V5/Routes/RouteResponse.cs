using System.Runtime.Serialization;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSDKLibrary.DataTypes.V5.Routes
{
    /// <summary>
    ///     V5 API response wrapper for a single route.
    ///     The API returns the route inside a "data" envelope.
    /// </summary>
    [DataContract]
    public class RouteResponse
    {
        /// <summary>
        ///     The route payload.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataObjectRoute Data { get; set; }
    }
}
