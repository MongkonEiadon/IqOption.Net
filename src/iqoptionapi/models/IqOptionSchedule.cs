using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class IqOptionSchedule
    {
        [JsonProperty("open")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Open { get; set; }

        [JsonProperty("close")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Close { get; set; }
    }
}