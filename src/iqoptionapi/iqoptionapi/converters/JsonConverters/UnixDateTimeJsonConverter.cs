using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoptionapi.iqoptionapi.converters.JsonConverters {
    public class UnixDateTimeJsonConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalSeconds + "");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return reader.Value.FromUnixToDateTime();
        }



    }
}