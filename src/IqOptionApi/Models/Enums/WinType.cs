using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IqOptionApi.Models
{
    public enum WinType
    {
        [EnumMember(Value = "equal")]
        Equal,
        [EnumMember(Value = "win")]
        Win,
        [EnumMember(Value = "loose")]
        Loose
    }
}
