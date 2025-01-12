using System;
using UnityEngine;
using Zenject;

public class CurrentTimeProxy
{
	private static long _startupTimestamp;

	[Inject]
	private void Inject()
	{
		_startupTimestamp = DateTime.UtcNow.ToTimestamp() - (int)Time.realtimeSinceStartup;
	}

	public long GetTimestamp()
	{
		return _startupTimestamp + (int)Time.realtimeSinceStartup;
	}
}
