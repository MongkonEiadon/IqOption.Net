using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public partial class Balance
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("enrolled_amount")]
        public decimal EnrolledAmount { get; set; }
        [JsonProperty("enrolled_sum_amount")]
        public decimal EnrolledSumAmount { get; set; }
        [JsonProperty("hold_amount")]
        public decimal HoldAmount { get; set; }
        [JsonProperty("orders_amount")]
        public decimal OrdersAmount { get; set; }
        [JsonProperty("auth_amount")]
        public decimal AuthAmount { get; set; }
        [JsonProperty("equivalent")]
        public long Equivalent { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("tournament_id")]
        public string TournamentId { get; set; }
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }
        [JsonProperty("is_fiat")]
        public bool IsFiat { get; set; }
        [JsonProperty("is_marginal")]
        public bool IsMarginal { get; set; }
        [JsonProperty("has_deposits")]
        public bool HasDeposits { get; set; }

    }
}