using System;
using Zenject;

public class SaveFirstLaunchPersistentDataCommand
{
	[Inject] private UserIdProxy _userIdProxy;
	[Inject] private FirstLaunchTimestampProxy _firstLaunchTimestampProxy;
	[Inject] private CurrentTimeProxy _currentTimeProxy;

	public void Execute()
	{
		_userIdProxy.Set(Guid.NewGuid().ToString());
		_firstLaunchTimestampProxy.Set(_currentTimeProxy.GetTimestamp());
	}
}
