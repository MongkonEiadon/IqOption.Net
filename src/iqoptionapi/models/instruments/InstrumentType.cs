using System.Runtime.Serialization;

namespace IqOptionApi.models.instruments {
    public enum InstrumentType {
        Forex,

        [EnumMember(Value = "digital-option")] CFD,

        Crypto,

        Unknown
    }
}