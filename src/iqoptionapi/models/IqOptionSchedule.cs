using System;
using Newtonsoft.Json;

namespace iqoptionapi.models {
    public partial class IqOptionSchedule
    {
        [JsonProperty("open")]
        public long Open { get; set; }

        [JsonProperty("close")]
        public long Close { get; set; }

        public DateTime OpenDateTime => Open.FromUnixToDateTime();
        public DateTime CloseDateTime => Close.FromUnixToDateTime();
    }
}