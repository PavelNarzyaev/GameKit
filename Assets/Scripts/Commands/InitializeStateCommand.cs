using System;
using Zenject;

public class InitializeStateCommand
{
	[Inject] private CurrentTimeProxy _currentTimeProxy;
	[Inject] private LocalStateProxy _localStateProxy;

	public void Execute()
	{
		_localStateProxy.data = new State
		{
			userId = Guid.NewGuid().ToString(),
			firstLaunchTimestamp = _currentTimeProxy.GetTimestamp()
		};
		_localStateProxy.MarkAsDirty();
	}
}
