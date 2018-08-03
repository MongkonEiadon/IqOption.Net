using System;
using Newtonsoft.Json;

namespace iqoptionapi.models {
    public partial class Money {
        [JsonProperty("deposit")]
        public Deposit Deposit { get; set; }

        [JsonProperty("withdraw")]
        public Deposit Withdraw { get; set; }
    }

    public class ServerTime {
        public DateTime ServerTimeStamp { get; set; }
    }

    public class Candles {
        public object Data { get; set; }
    }
}