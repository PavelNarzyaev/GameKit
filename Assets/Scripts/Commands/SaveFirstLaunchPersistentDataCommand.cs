using System;
using Zenject;

public class SaveFirstLaunchPersistentDataCommand
{
	[Inject] private UserIdProxy _userIdProxy;

	public void Execute()
	{
		_userIdProxy.Set(Guid.NewGuid().ToString());
	}
}
