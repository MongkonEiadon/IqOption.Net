using System;

namespace iqoptionapi {
    internal static class DateTimeExtensions
    {


        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixToDateTime(this object This)
        {
            return _epoch.AddMilliseconds(Convert.ToUInt64(This));
        }
    }
}