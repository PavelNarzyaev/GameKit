using Zenject;

public class LaunchCommand
{
	[Inject] private UserIdProxy _userIdProxy;
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;

	public void Execute()
	{
		var isFirstLaunch = string.IsNullOrEmpty(_userIdProxy.Get());
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
	}
}
