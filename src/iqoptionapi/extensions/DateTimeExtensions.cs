using System;

namespace IqOptionApi.Extensions {
    internal static class DateTimeExtensions {
        public static DateTime FromUnixToDateTime(this object This) {
            return DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(This)).DateTime.ToLocalTime();
        }

        public static DateTime FromUnixSecondsToDateTime(this object This) {
            return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(This)).DateTime.ToLocalTime();
        }

        public static Int64 ToUnixTimeSecounds(this DateTime This) {
            return new DateTimeOffset(This).ToUnixTimeSeconds();
        }
    }
}