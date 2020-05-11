﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Converters.JsonConverters
{
    internal class UnixSecondsDateTimeJsonConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTimeOffset) value).ToUnixTimeSeconds());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reader.Value));
        }
    }
}