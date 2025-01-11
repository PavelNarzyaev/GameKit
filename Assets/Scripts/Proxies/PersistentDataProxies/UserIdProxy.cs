using UnityEngine;

public class UserIdProxy
{
	private const string _playerPrefsKey = "user_id";

	public void Set(string id)
	{
		PlayerPrefs.SetString(_playerPrefsKey, id);
	}

	public string Get()
	{
		return PlayerPrefs.GetString(_playerPrefsKey);
	}
}
