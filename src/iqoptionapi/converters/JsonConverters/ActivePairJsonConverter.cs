using System;
using iqoptionapi.models;
using Newtonsoft.Json;

namespace iqoptionapi.converters.JsonConverters {
    public class ActivePairJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value.GetType() == typeof(ActivePair)) {
                writer.WriteValue((int) (ActivePair) value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            var result = Enum.Parse<ActivePair>(reader.Value.ToString());
            return result;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(string) || objectType == typeof(int) || objectType == typeof(long);
        }
    }
}