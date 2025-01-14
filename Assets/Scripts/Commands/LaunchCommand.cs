using Zenject;

public class LaunchCommand
{
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;
	[Inject] private PersistentDataProxy _persistentDataProxy;

	public void Execute()
	{
		var isFirstLaunch = !_persistentDataProxy.Exists();
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
		else
			_persistentDataProxy.Load();
		_persistentDataProxy.data.launchesCounter++;
		_persistentDataProxy.Save();
	}
}
