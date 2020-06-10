using System.Runtime.Serialization;

namespace IqOptionApi.Models.DigitalOptions
{
    /// <summary>
    /// The expiration periods to place the position of Options
    /// </summary>
    public enum DigitalOptionsExpiryDuration
    {
        [EnumMember(Value = "1M")]
        M1 = 1,
        
        [EnumMember(Value = "5M")]
        M5 = 5,
        
        [EnumMember(Value = "15M")]
        M15 = 15
    }
}