using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models {
    public class CandleInfo {
        private DateTimeOffset _from;
        private DateTimeOffset _to;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("max")]
        public double Max { get; set; }

        [JsonProperty("volumn")]
        public long Volumn { get; set; }


        [JsonProperty("from")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset From {
            get => _from;
            set => _from = value.ToLocalTime();
        }


        [JsonProperty("to")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset To {
            get => _to;
            set => _to = value.ToLocalTime();
        }
    }
}