using System.Runtime.Serialization;

namespace IqOptionApi.Models {
    public enum WinType {
        [EnumMember(Value = "equal")] Equal,
        [EnumMember(Value = "win")] Win,
        [EnumMember(Value = "loose")] Loose
    }
}