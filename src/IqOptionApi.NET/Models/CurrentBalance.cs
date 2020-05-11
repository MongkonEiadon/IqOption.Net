using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class Balance
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("type")] public BalanceType Type { get; set; }

        [JsonProperty("index")] public long Index { get; set; }

        [JsonProperty("amount")] public decimal Amount { get; set; }

        [JsonProperty("balances")] public Balance[] Balances { get; set; }

        [JsonProperty("is_fiat")] public bool IsFiat { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("new_amount")] public decimal NewAmount { get; set; }

        [JsonProperty("is_marginal")] public bool IsMarginal { get; set; }

        [JsonProperty("bonus_amount")] public long BonusAmount { get; set; }

        [JsonProperty("enrolled_amount")] public double EnrolledAmount { get; set; }

        [JsonProperty("bonus_total_amount")] public long BonusTotalAmount { get; set; }

        [JsonProperty("description")] public object Description { get; set; }
    }
}