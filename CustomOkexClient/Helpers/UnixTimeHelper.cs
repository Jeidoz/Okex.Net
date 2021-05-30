using System;

namespace CustomOkexClient.Helpers
{
    public static class UnixTimeHelper
    {
        private static readonly DateTime UnixEpoch = 
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeMilliSeconds(this DateTime dateTime)
        {
            return Convert.ToInt64((dateTime - UnixEpoch).TotalSeconds) * 1_000 + dateTime.Millisecond;
        }
    }
}