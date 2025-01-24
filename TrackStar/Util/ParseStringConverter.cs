using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Util
{
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return t == typeof(long?) ? (long?)null : 0; // Return default value for nullable or non-nullable long
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                // If the value is already a long
                return Convert.ToInt64(reader.Value);
            }

            if (reader.TokenType == JsonToken.String)
            {
                var value = serializer.Deserialize<string>(reader);

                // Check for "N/A" or other invalid values
                if (string.IsNullOrWhiteSpace(value) || value.Equals("N/A", StringComparison.OrdinalIgnoreCase))
                {
                    return t == typeof(long?) ? (long?)null : 0; // Return default value for invalid strings
                }

                // Try to parse the string as a long
                if (long.TryParse(value, out var l))
                {
                    return l;
                }
            }
            // Base case: Handle any other unexpected token types or invalid data
            return t == typeof(long?) ? (long?)null : 0; // Return default value
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
        }
    }

}
