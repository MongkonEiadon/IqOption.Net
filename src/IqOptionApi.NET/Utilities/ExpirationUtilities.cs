using System;
using System.Collections.Generic;
using System.Text;

namespace IqOptionApi.Utilities
{
    public static class ExpirationUtilities
    {
        public static DateTimeOffset GetBinaryExpiration(DateTimeOffset ServerTime, int Duration)
        {
            DateTimeOffset now = ServerTime;
            DateTimeOffset exp = ServerTime;
            if (now.Second > 0)
            {
                DateTimeOffset RawExp = now.AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
                if (now.Second >= 30)
                {
                    exp = RawExp.AddMinutes(Duration + 1);
                }
                else
                {
                    exp = RawExp.AddMinutes(Duration);
                }
            }
            return exp;
        }

        public static DateTimeOffset GetBinaryExpiration(DateTimeOffset ServerTime, DateTimeOffset Duration)
        {
            if (Duration.Second % 60 != 0) Duration = Duration.AddSeconds(60 - Duration.Second);
            return Duration;
        }
    }
}
