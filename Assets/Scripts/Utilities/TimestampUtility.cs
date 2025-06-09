using System;

namespace Utilities
{
    public static class TimestampUtility
    {
        public static long ConvertDatetimeToTimestamp(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
        }

        public static DateTime ConvertTimestampToLocalDateTime(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
        }

        public static string ConvertTimestampToReadableString(long timestamp)
        {
            return ConvertTimestampToLocalDateTime(timestamp).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
