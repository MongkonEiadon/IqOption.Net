using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class InstrumentsResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("msg")]
        public InstrumentInfo Message { get; set; }
        
        public class InstrumentInfo
        {
            [JsonProperty("type")]
            public InstrumentType Type { get; set; }
            
            [JsonProperty("instruments")]
            public InstrumentItem[] Instruments { get; set; }
        }

        public class InstrumentItem
        {
            [JsonProperty("ticker")]
            public string Ticker { get; set; }
            
            [JsonProperty("id")]
            public string TickerActiveId { get; set; }
            
            [JsonProperty("active_id")]
            public ActivePair ActivePair { get; set; }
            
            [JsonProperty("underlying")]
            public string UnderlyingInfo { get; set; }
        }
    }
}