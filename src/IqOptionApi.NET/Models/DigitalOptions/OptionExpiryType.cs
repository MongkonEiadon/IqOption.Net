using System.Runtime.Serialization;

namespace IqOptionApi.Models.DigitalOptions
{
    /// <summary>
    /// The expiration periods to place the position of Options
    /// </summary>
    public enum DigitalOptionsExpiryType
    {
        [EnumMember(Value = "1M")]
        PT1M = 1,
        
        [EnumMember(Value = "5M")]
        PT5M = 5,
        
        [EnumMember(Value = "15M")]
        PT15M = 15
    }
}