using System;
using IqOptionApi.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IqOptionApi.Converters.JsonConverters;
using System.Collections.Generic;

namespace IqOptionApi.Models
{
    public class InstrumentQuotesExpiration
    {
        [JsonProperty("period")]
        public int Period { get; set; }
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }
    }
    public class InstrumentQuotesPrice
    {
        [JsonProperty("ask")]
        public double? Ask { get; set; }
        [JsonProperty("bid")]
        public double? Bid { get; set; }
    }
    public class InstrumentQuotesData
    {
        [JsonProperty("price")]
        public InstrumentQuotesPrice Price { get; set; }
        [JsonProperty("symbols")]
        public List<string> Symbols { get; set; }
    }
    public class InstrumentQuotes
    {
        [JsonProperty("active")]
        public ActivePair Active { get; set; }
        [JsonProperty("expiration")]
        public InstrumentQuotesExpiration Expiration { get; set; }
        [JsonProperty("kind")]
        public InstrumentType Instrument { get; set; }
        [JsonProperty("quotes")]
        public List<InstrumentQuotesData> Quotes { get; set; }
    }
}
