using System;

public static class TimestampExtensions
{
	public static long ToTimestamp(this DateTime dateTime)
	{
		return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
	}

	public static DateTime ToLocalDateTime(this long timestamp)
	{
		return DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
	}
}
