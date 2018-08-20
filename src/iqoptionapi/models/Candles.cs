using System;
using iqoptionapi.ws;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoptionapi.models {




    public class Candles {

        [JsonProperty("candles")]
        public CandleItem[] Items { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonIgnore]
        public int Count => Items?.Length ?? 0;
    }


    public class CandleItem {

        [JsonProperty("id")]
        public int Id { get; set; }

        private DateTimeOffset _from;


        [JsonProperty("from")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset From {
            get => _from;
            set => _from = value.ToLocalTime();
        }


        private DateTimeOffset _to;

        [JsonProperty("to")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset To {
            get => _to;
            set => _to = value.ToLocalTime();
        }

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
    }
}