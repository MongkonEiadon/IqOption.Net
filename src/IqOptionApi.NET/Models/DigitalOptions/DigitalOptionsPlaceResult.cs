using System;
using Newtonsoft.Json;

namespace IqOptionApi.Models.DigitalOptions
{
    public class DigitalOptionsPlacedResult
    {
        [JsonProperty("id")]
        public Int64? Id { get; set; }
        
        [JsonProperty("part")]
        public string ErrorPart { get; set; }
        
        [JsonProperty("level")]
        public string ErrorLevel { get; set; }
        
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        public bool IsError() => Id == null;
    }
}