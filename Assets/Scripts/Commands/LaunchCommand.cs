using Zenject;

public class LaunchCommand
{
	[Inject] private UserIdProxy _userIdProxy;
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;
	[Inject] private LaunchesCounterProxy _launchesCounterProxy;

	public void Execute()
	{
		var isFirstLaunch = string.IsNullOrEmpty(_userIdProxy.Get());
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
		_launchesCounterProxy.Increment();
	}
}
