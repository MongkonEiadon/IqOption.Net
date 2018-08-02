using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoptionapi.converters.JsonConverters {
    internal class UnixDateTimeJsonConverter : DateTimeConverterBase {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue(((DateTime) value).ToUnixTimeSecounds()
                .ToString()); //   ((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if (reader.Value == null) {
                return null;
            }

            return reader.Value.FromUnixToDateTime();
        }
    }

    internal class UnixSecondsDateTimeJsonConverter : DateTimeConverterBase {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue(((DateTime) value).ToUnixTimeSecounds()
                .ToString()); //   ((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if (reader.Value == null) {
                return null;
            }

            return reader.Value.FromUnixSecondsToDateTime();
        }
    }
}