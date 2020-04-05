using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class Money
    {
        [JsonProperty("deposit")] public Deposit Deposit { get; set; }

        [JsonProperty("withdraw")] public Deposit Withdraw { get; set; }
    }
}