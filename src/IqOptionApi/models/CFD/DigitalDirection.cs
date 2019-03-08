using System.Runtime.Serialization;

namespace IqOptionApi.models.CFD {
    public enum DigitalDirection {
        [EnumMember(Value = "long")] Long = 1,
        [EnumMember(Value = "short")] Short = 2
    }
}