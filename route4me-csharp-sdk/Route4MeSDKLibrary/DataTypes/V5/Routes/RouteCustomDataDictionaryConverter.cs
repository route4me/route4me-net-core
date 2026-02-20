using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Custom JSON converter for the bulk route custom data response.
    /// Handles the case where the API returns the <c>data</c> field as a JSON array
    /// (e.g. <c>[{"&lt;routeId&gt;": {"key": "value"}}, ...]</c>) instead of the
    /// JSON object defined in the OpenAPI spec. Both formats produce a
    /// <see cref="Dictionary{TKey,TValue}"/> keyed by route ID.
    /// </summary>
    internal sealed class RouteCustomDataDictionaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => objectType == typeof(Dictionary<string, Dictionary<string, string>>);

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var result = new Dictionary<string, Dictionary<string, string>>();

            switch (token.Type)
            {
                case JTokenType.Object:
                    // Expected spec format: {"<routeId>": {"key": "value"}, ...}
                    foreach (var prop in ((JObject)token).Properties())
                    {
                        result[prop.Name] = prop.Value.Type != JTokenType.Null
                            ? prop.Value.ToObject<Dictionary<string, string>>()
                            : null;
                    }
                    break;

                case JTokenType.Array:
                    // Actual API format: [{"<routeId>": {"key": "value"}}, ...]
                    // Each array element is a single-entry object mapping routeId → customData.
                    foreach (var element in (JArray)token)
                    {
                        if (element.Type != JTokenType.Object)
                            continue;

                        foreach (var prop in ((JObject)element).Properties())
                        {
                            if (prop.Value.Type == JTokenType.Object)
                            {
                                result[prop.Name] = prop.Value.ToObject<Dictionary<string, string>>();
                            }
                            else if (prop.Value.Type == JTokenType.Null)
                            {
                                result[prop.Name] = null;
                            }
                        }
                    }
                    break;
                default:
                    throw new JsonSerializationException(
                        $"Unexpected token type '{token.Type}' when deserializing route custom data. JSON path: {token.Path}");
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => serializer.Serialize(writer, value);
    }
}