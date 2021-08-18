using System;
using System.Collections;
using System.Collections.Generic;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Models.DigitalOptions;

namespace IqOptionApi.Utilities
{
    public static class DateTimeUtilities
    {
        public static DateTimeOffset GetExpirationTime(DateTimeOffset dt, DigitalOptionsExpiryDuration duration)
        {
            return ExpirationTimeTable(dt)[(int) duration];
        }

        public static DateTimeOffset GetExpirationTime(DateTimeOffset dt, BinaryOptionsDuration duration)
        {
            return ExpirationTimeTable(dt)[(int) duration];
        }

        public static IDictionary<int, DateTimeOffset> ExpirationTimeTable(DateTimeOffset dt)
        {
            var dic = new Dictionary<int, DateTimeOffset>();

            var now = dt.ToUnixTimeSeconds();
            var exp = dt.AddSeconds(-(dt.Second)).AddMilliseconds(-(dt.Millisecond));

            if (exp.AddMinutes(1).ToUnixTimeSeconds() - dt.ToUnixTimeSeconds() > 30)
                exp = exp.AddMinutes(1);
            else exp = exp.AddMinutes(2);

            for (var i=1;i<=15;i++)
            {
                if (i == 1)
                {
                    dic.Add(i, exp.AddMinutes(i - 1));
                }
                else
                {
                    DateTimeOffset now_date = dt.AddMinutes(1).AddSeconds(30);
                    while (true)
                    {
                        if (now_date.Minute % i == 0 && (now_date.ToUnixTimeSeconds() - dt.ToUnixTimeSeconds()) > 30)
                        {
                            break;
                        }
                        now_date = now_date.AddMinutes(1);
                    }
                    exp = now_date;
                    dic.Add(i, exp);
                }
            }
            return dic;
        }
    }
}