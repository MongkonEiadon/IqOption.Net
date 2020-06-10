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

            
            for (var i = 1; i <= 5; i++)
                dic.Add(i, exp.AddMinutes(i - 1));
            
            
            

            /*
            var idx = 50;
            var index = 0;
            exp = dt.AddSeconds(-(dt.Second)).AddMilliseconds(-(dt.Millisecond));
            while (index < idx)
            {
                if (exp.Minute % 15 == 0 && (exp.ToUnixTimeSeconds() - now > 60 * 5))
                {
                    dic.Add(1, exp);
                    index = index + 1;
                }

                exp = exp.AddMinutes(1);
            }
            */

            return dic;
        }
    }
}