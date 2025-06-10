using System;

namespace Utilities
{
    public static class TimestampUtility
    {
        public static long ConvertDatetimeToTimestamp(DateTime dateTime) => new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
        private static DateTime ConvertTimestampToLocalDateTime(long timestamp) => DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
        public static string ConvertTimestampToReadableString(long timestamp) => ConvertTimestampToLocalDateTime(timestamp).ToString("yyyy-MM-dd HH:mm:ss");
    }
}
