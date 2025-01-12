using UnityEngine;

public class UserIdProxy
{
	public void Set(string value)
	{
		PlayerPrefs.SetString(PlayerPrefsKeys.userId, value);
	}

	public string Get()
	{
		return PlayerPrefs.GetString(PlayerPrefsKeys.userId);
	}
}
