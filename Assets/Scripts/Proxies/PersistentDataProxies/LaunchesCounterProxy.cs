using UnityEngine;

public class LaunchesCounterProxy
{
	public void Increment()
	{
		PlayerPrefs.SetInt(PlayerPrefsKeys.launchesCounter, Get() + 1);
	}

	public int Get()
	{
		return PlayerPrefs.GetInt(PlayerPrefsKeys.launchesCounter);
	}
}
