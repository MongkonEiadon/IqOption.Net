using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstrumentType
    {
        [EnumMember(Value = "forex")]
        Forex,
        
        [EnumMember(Value = "cfd")]
        CFD,
        
        [EnumMember(Value = "crypto")]
        Crypto,
        Unknown
    }
}