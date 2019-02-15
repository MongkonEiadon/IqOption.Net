using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IqOptionApi.Models
{
    public enum EnumBuyType
    {
        [EnumMember(Value = "turbo")]
        Turbo,
        [EnumMember(Value = "binary")]
        Binary
    }
}
