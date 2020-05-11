using System.Runtime.Serialization;

namespace IqOptionApi.Models
{
    public enum OrderResult
    {
        [EnumMember(Value = "loose")] Loose,

        [EnumMember(Value = "win")] Win,

        [EnumMember(Value = "equal")] Equal,

        [EnumMember(Value = "sold")] Sold
    }

    public enum OptionType
    {
        Turbo = 3,

        Binary = 1
    }
}