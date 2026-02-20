using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Response containing route custom data for multiple routes.
    /// Returned by the bulk route custom data endpoint (POST /route-custom-data/bulk).
    /// </summary>
    [DataContract]
    public class RouteCustomDataCollection
    {
        /// <summary>
        /// Dictionary mapping route IDs to their custom data key-value pairs.
        /// Each key is a route ID and the value is a dictionary of custom data for that route.
        /// </summary>
        /// <remarks>
        /// The API may return this field as either a JSON object or a JSON array.
        /// The <see cref="RouteCustomDataDictionaryConverter"/> handles both formats transparently.
        /// </remarks>
        [JsonProperty("data")]
        [JsonConverter(typeof(RouteCustomDataDictionaryConverter))]
        public Dictionary<string, Dictionary<string, string>> Data { get; set; }
    }
}