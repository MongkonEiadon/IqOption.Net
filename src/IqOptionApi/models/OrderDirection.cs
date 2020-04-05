using System.Runtime.Serialization;

namespace IqOptionApi
{
    public enum OrderDirection
    {
        [EnumMember(Value = "put")] Put = 1,

        [EnumMember(Value = "call")] Call = 2
    }
}