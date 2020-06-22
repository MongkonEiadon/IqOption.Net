using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    
    /// <summary>
    /// Indicate the type of Active Pair
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstrumentType
    {
        #region Options

        [EnumMember(Value = "digital-option")]
        DigitalOption,
        
        /// <summary>
        /// The binary's option with more than 5M duration
        /// </summary>
        [EnumMember(Value = "binary-option")]
        BinaryOption,
        
        /// <summary>
        /// The Forex's option
        /// </summary>
        [EnumMember(Value = "fx-option")]
        FxOption,
        
        /// <summary>
        /// The option with short duration (1M-5M)
        /// </summary>
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