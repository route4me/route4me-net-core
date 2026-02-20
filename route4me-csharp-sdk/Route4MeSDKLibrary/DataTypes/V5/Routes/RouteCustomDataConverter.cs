using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Deserializes the <c>custom_data</c> field on a route, handling the API inconsistency
    /// where an empty value is returned as a JSON array <c>[]</c> rather than <c>{}</c> or <c>null</c>.
    /// <list type="bullet">
    ///   <item><c>null</c> → <c>null</c></item>
    ///   <item><c>[]</c> (empty array) → <c>null</c></item>
    ///   <item><c>{"key":"value"}</c> (object) → <see cref="Dictionary{TKey,TValue}"/></item>
    /// </list>
    /// </summary>
    internal sealed class RouteCustomDataConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> ReadJson(
            JsonReader reader,
            Type objectType,
            Dictionary<string, string> existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.StartArray:
                    JArray.Load(reader); // consume and discard
                    return null;
                case JsonToken.StartObject:
                    return JObject.Load(reader).ToObject<Dictionary<string, string>>(serializer);
                default:
                    throw new JsonSerializationException(
                        $"Unexpected token '{reader.TokenType}' for custom_data. JSON path: {reader.Path}");
            }
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, string> value, JsonSerializer serializer)
            => serializer.Serialize(writer, value);
    }
}