using Zenject;

public class LaunchCommand
{
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private LayersMediator _layersMediator;

	public void Execute()
	{
		var isFirstLaunch = !_persistentDataProxy.Exists();
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
		else
			_persistentDataProxy.RefreshFromFile();
		_persistentDataProxy.data.launchesCounter++;
		_persistentDataProxy.MarkAsDirty();

		_layersMediator.ShowScreen<MainPageScreen>(LayerNames.Layer.Page);
		// _layersMediator.Show<AnalyticsPageScreen>(LayerNames.Layer.Page);
	}
}
