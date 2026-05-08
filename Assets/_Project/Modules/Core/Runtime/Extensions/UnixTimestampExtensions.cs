using System;

namespace GameKit.Core
{
    public static class UnixTimestampExtensions
    {
        public static string ToLocalDatetimeString(this long timestamp, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime.ToString(format);
        }
    }
}
