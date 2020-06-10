using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstrumentType
    {
        #region Options

        [EnumMember(Value = "digital-option")]
        DigitalOption,
        
        [EnumMember(Value = "binary-option")]
        BinaryOption,
        
        [EnumMember(Value = "turbo")]
        TurboOption,

        #endregion
        [EnumMember(Value = "forex")]
        Forex,
        
        [EnumMember(Value = "cfd")]
        CFD,
        
        [EnumMember(Value = "crypto")]
        Crypto,
        
        Unknown
    }
}