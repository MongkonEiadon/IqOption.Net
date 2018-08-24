using System;
using Newtonsoft.Json;

namespace IqOptionApi.Models {
    public partial class IqOptionSchedule {
        [JsonProperty("open")]
        public long Open { get; set; }

        [JsonProperty("close")]
        public long Close { get; set; }

        public DateTime OpenDateTime => DateTimeOffset.FromUnixTimeSeconds(Open).DateTime.ToLocalTime();
        public DateTime CloseDateTime => DateTimeOffset.FromUnixTimeSeconds(Close).DateTime.ToLocalTime();
    }
}