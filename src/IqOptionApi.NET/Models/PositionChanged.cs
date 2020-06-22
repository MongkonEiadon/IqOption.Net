using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class PositionChanged : IEqualityComparer
    {
        [JsonProperty("id")] public string Id { get; set; }
        
        [JsonProperty("instrument_id")] public string InstrumentId { get; set; }
        
        [JsonProperty("instrument_type")] 
        [JsonConverter(typeof(StringEnumConverter))]
        public InstrumentType InstrumentType { get; set; }
        
        [JsonProperty("active_id")]
        public ActivePair ActivePair { get; set; }
        
        [JsonProperty("invest")]
        public double InvestAmount { get; set; }
        
        [JsonProperty("open_quote")]
        public double OpenQuote { get; set; }
        
        [JsonProperty("raw_event")]
        public PortfolioChangedEventInfo PortfolioChangedEventInfo { get; set; }


        public new bool Equals(object x, object y)
        {
            return x == y;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    public class PortfolioChangedEventInfo
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("user_balance_id")]
        public long UserBalanceId { get; set; }
        
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        
        [JsonProperty("version")]
        public long Version { get; set; }
    }
}