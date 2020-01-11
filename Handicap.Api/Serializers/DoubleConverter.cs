using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Handicap.Api.Serializers
{
    public class DoubleConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

                if (Utf8Parser.TryParse(span, out long number, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return (int)number;
                }

                if (Int64.TryParse(reader.GetString(), out number))
                {
                    return (int)number;
                }
            }

            // Default behavior; will throw if TokenType != Number
            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            // Write as number or string? Could also use [JsonConverter] to specify a different converter for certain properties.
            writer.WriteStringValue(value.ToString());
        }
    }
}
