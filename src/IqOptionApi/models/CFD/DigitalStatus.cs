using System.Runtime.Serialization;

namespace IqOptionApi.models.CFD {
    public enum DigitalStatus
    {
        [EnumMember(Value = "open")] Open,
        [EnumMember(Value = "closed")] Closed,
        [EnumMember(Value = "opened")] Opened

    }
}