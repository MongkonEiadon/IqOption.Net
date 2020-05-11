using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class Instrument
    {
        [JsonProperty("ticker")] public string Ticker { get; set; }

        [JsonProperty("is_visible")] public bool IsVisible { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("active_id")] public long ActiveId { get; set; }

        [JsonProperty("active_group_id")] public long ActiveGroupId { get; set; }

        [JsonProperty("underlying")] public string Underlying { get; set; }

        [JsonProperty("schedule")] public IqOptionSchedule[] Schedule { get; set; }

        [JsonProperty("is_enabled")] public bool IsEnabled { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("localization_key")] public string LocalizationKey { get; set; }

        [JsonProperty("image")] public string Image { get; set; }

        [JsonProperty("precision")] public long Precision { get; set; }

        [JsonProperty("start_time")] public long StartTime { get; set; }

        [JsonProperty("last_ask")] public object LastAsk { get; set; }

        [JsonProperty("last_bid")] public object LastBid { get; set; }

        [JsonProperty("currency_left_side")] public string CurrencyLeftSide { get; set; }

        [JsonProperty("currency_right_side")] public string CurrencyRightSide { get; set; }
    }
}