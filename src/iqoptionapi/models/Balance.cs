﻿using Newtonsoft.Json;

namespace IqOptionApi.Models {
    public class Balance {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public BalanceType Type { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("new_amount")]
        public long NewAmount { get; set; }

        [JsonProperty("bonus_amount")]
        public long BonusAmount { get; set; }

        [JsonProperty("bonus_total_amount")]
        public long BonusTotalAmount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }
    }
}