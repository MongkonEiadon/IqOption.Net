﻿using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class CurrentCandle : CandleInfo
    {
        [JsonProperty("active_id")] public ActivePair ActivePair { get; set; }
        [JsonProperty("size")] public TimeFrame TimeFrame { get; set; }
        
        [JsonProperty("at")] public long At { get; set; }
        
        [JsonProperty("ask")] public double Ask { get; set; }
        
        [JsonProperty("bid")] public double Bid { get; set; }
        
        [JsonProperty("phase")] public string Phase { get; set; }
    }
}