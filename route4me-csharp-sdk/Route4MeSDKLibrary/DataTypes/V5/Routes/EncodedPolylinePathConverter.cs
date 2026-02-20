using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Deserializes the <c>path</c> field from the Route4Me V5 API.
    /// The API returns <c>path</c> as a JSON array of Google Encoded Polyline strings,
    /// each representing one route segment. Each segment is decoded independently
    /// and all resulting points are concatenated into a flat <see cref="DirectionPathPoint"/> array.
    /// </summary>
    /// <remarks>
    /// Encoding format: Google Encoded Polyline Algorithm
    /// (https://developers.google.com/maps/documentation/utilities/polylinealgorithm).
    /// Coordinates are stored as scaled integers (×1e5), delta-encoded within each segment.
    /// </remarks>
    internal sealed class EncodedPolylinePathConverter : JsonConverter<DirectionPathPoint[]>
    {
        public override DirectionPathPoint[] ReadJson(
            JsonReader reader,
            Type objectType,
            DirectionPathPoint[] existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType != JsonToken.StartArray)
                throw new JsonSerializationException(
                    $"Expected a JSON array for 'path' but got '{reader.TokenType}'. JSON path: {reader.Path}");

            var points = new List<DirectionPathPoint>();

            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.String)
                    DecodeSegment((string)reader.Value, points);
            }

            return points.ToArray();
        }

        private static void DecodeSegment(string encoded, List<DirectionPathPoint> points)
        {
            int index = 0, lat = 0, lng = 0;

            while (index < encoded.Length)
            {
                lat += DecodeNext(encoded, ref index);
                lng += DecodeNext(encoded, ref index);
                points.Add(new DirectionPathPoint { Lat = lat / 1e5, Lng = lng / 1e5 });
            }
        }

        private static int DecodeNext(string encoded, ref int index)
        {
            int result = 0, shift = 0, b;

            do
            {
                b = encoded[index++] - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            }
            while (b >= 0x20);

            return (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
        }

        /// <summary>
        /// Serialization is not supported: <c>path</c> is a read-only API field.
        /// </summary>
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, DirectionPathPoint[] value, JsonSerializer serializer)
            => throw new NotSupportedException($"{nameof(EncodedPolylinePathConverter)} does not support writing.");
    }
}
