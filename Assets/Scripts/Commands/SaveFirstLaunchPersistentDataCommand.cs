using System;
using Zenject;

public class SaveFirstLaunchPersistentDataCommand
{
	[Inject] private CurrentTimeProxy _currentTimeProxy;
	[Inject] private PersistentDataProxy _persistentDataProxy;

	public void Execute()
	{
		_persistentDataProxy.data = new PersistentData
		{
			userId = Guid.NewGuid().ToString(),
			firstLaunchTimestamp = _currentTimeProxy.GetTimestamp()
		};
		_persistentDataProxy.MarkAsDirty();
	}
}
