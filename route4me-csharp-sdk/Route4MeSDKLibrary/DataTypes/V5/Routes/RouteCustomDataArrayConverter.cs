using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// JSON converter for route custom_data that accepts either a single object or an array.
    /// GET /api/v5.0/routes/{route_id} may return custom_data as a single object, e.g.
    /// "custom_data": {"priority":"high","region":"northeast"}. List/filter may return an array.
    /// This converter normalizes both to a single Dictionary&lt;string, string&gt; (array → first element).
    /// </summary>
    internal sealed class RouteCustomDataArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => objectType == typeof(Dictionary<string, string>);

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Null || token.Type == JTokenType.Undefined)
                return null;

            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.ToObject<Dictionary<string, string>>();

                // case JTokenType.Array:
                //     var arr = token.ToObject<Dictionary<string, string>[]>();
                //     if (arr == null || arr.Length == 0)
                //         return null;
                //     return arr[0];

                default:
                    return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => serializer.Serialize(writer, value);
    }
}
