using Zenject;

public class LaunchCommand
{
	[Inject] private InitializeStateCommand _initializeStateCommand;
	[Inject] private StateProxy _stateProxy;
	[Inject] private ResetUiCommand _resetUiCommand;

	public void Execute()
	{
		var isFirstLaunch = !_stateProxy.Exists();
		if (isFirstLaunch)
			_initializeStateCommand.Execute();
		else
			_stateProxy.RefreshFromFile();
		_stateProxy.data.launchesCounter++;
		_stateProxy.MarkAsDirty();

		_resetUiCommand.Execute();
	}
}
