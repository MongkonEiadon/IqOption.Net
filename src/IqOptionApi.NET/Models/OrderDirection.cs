using System.Runtime.Serialization;

namespace IqOptionApi.Models
{
    public enum OrderDirection
    {
        [EnumMember(Value = "put")] Put = 1,

        [EnumMember(Value = "call")] Call = 2
    }
}