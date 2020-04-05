using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class BalanceChanged
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("current_balance")] public Balance CurrentBalance { get; set; }
    }
}