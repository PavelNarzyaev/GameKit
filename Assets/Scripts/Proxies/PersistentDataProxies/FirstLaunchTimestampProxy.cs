using System.Globalization;
using UnityEngine;

public class FirstLaunchTimestampProxy
{
	public void Set(long value)
	{
		PlayerPrefs.SetString(PlayerPrefsKeys.firstLaunchTimestamp, value.ToString(CultureInfo.InvariantCulture));
	}

	public long Get()
	{
		return long.Parse(PlayerPrefs.GetString(PlayerPrefsKeys.firstLaunchTimestamp), CultureInfo.InvariantCulture);
	}
}
