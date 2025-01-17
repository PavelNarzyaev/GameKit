using Zenject;

public class LaunchCommand
{
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private ResetUiCommand _resetUiCommand;

	public void Execute()
	{
		var isFirstLaunch = !_persistentDataProxy.Exists();
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
		else
			_persistentDataProxy.RefreshFromFile();
		_persistentDataProxy.data.launchesCounter++;
		_persistentDataProxy.MarkAsDirty();

		_resetUiCommand.Execute();
	}
}
