using System;
using iqoptionapi.models;
using Newtonsoft.Json;

namespace iqoptionapi.converters.JsonConverters {
    public sealed class InstrumentTypeJsonConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer) {
            var value = (string) reader.Value;

            switch (value) {
                case "forex":
                    return InstrumentType.Forex;
                case "cfd":
                    return InstrumentType.CFD;
                case "crypto":
                    return InstrumentType.Crypto;
                default:
                    return InstrumentType.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer) {
            var _value = (InstrumentType) (value);

            switch (value) {
                case InstrumentType.Forex:
                    writer.WriteValue("forex");
                    break;
                case InstrumentType.CFD:
                    writer.WriteValue("cfd");
                    break;
                case InstrumentType.Crypto:
                    writer.WriteValue("crypto");
                    break;
                default:
                    writer.WriteValue("");
                    break;
            }
        }
    }
}