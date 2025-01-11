using UnityEngine;

public class UserIdProxy
{
	public void Set(string id)
	{
		PlayerPrefs.SetString(PlayerPrefsKeys.userId, id);
	}

	public string Get()
	{
		return PlayerPrefs.GetString(PlayerPrefsKeys.userId);
	}
}
