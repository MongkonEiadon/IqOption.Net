﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Converters.JsonConverters
{
    internal class UnixDateTimeJsonConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTimeOffset) value).ToUniversalTime().ToUnixTimeMilliseconds());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            return DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(reader.Value)).ToLocalTime();
        }
    }
}