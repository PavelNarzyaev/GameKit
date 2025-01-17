using System;
using Zenject;

public class InitializeStateCommand
{
	[Inject] private CurrentTimeProxy _currentTimeProxy;
	[Inject] private StateProxy _stateProxy;

	public void Execute()
	{
		_stateProxy.data = new State
		{
			userId = Guid.NewGuid().ToString(),
			firstLaunchTimestamp = _currentTimeProxy.GetTimestamp()
		};
		_stateProxy.MarkAsDirty();
	}
}
