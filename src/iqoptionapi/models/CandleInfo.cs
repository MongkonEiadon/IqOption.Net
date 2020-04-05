using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class CandleInfo
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("open")] public double Open { get; set; }

        [JsonProperty("close")] public double Close { get; set; }

        [JsonProperty("min")] public double Min { get; set; }

        [JsonProperty("max")] public double Max { get; set; }

        [JsonProperty("volumn")] public long Volumn { get; set; }


        [JsonProperty("from")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset From { get; set; }


        [JsonProperty("to")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset To { get; set; }
    }
}