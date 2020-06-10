using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class OrderChanged
    {
        [JsonProperty("id")] public string Id { get; set; }
        
        [JsonProperty("version")] public long Version { get; set; }
        
        [JsonProperty("intrument_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentType InstrumentType { get; set; }
        
        [JsonProperty("position_id")] public string PositionId { get; set; }
        
        [JsonProperty("user_balance_id")] public long UserBalanceId { get; set; }
        
        [JsonProperty("user_id")] public long UserId { get; set; }
        
        [JsonProperty("source")] public string SourceName { get; set; }

        [JsonProperty("raw_event")] public OrderChangedEventInfo OrderChangedEventInfo { get; set; }
    }
    
    public class OrderChangedEventInfo
    {
        [JsonProperty("id")] public string Id { get; set; }
        
        [JsonProperty("avg_price")] public decimal AveragePrice { get; set; }
        
        [JsonProperty("instrument_id")] public string InstrumentId { get; set; }
        
        [JsonProperty("instrument_underlying")] public string InstrumentUnderlying { get; set; }
        
        [JsonProperty("margin")] public double Margin { get; set; }
        
        [JsonProperty("instrument_type")] 
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentType InstrumentType { get; set; }
        
        [JsonProperty("side")] public string Side { get; set; }
    }
}