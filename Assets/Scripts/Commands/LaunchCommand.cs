using Zenject;

public class LaunchCommand
{
	[Inject] private SaveFirstLaunchPersistentDataCommand _saveFirstLaunchPersistentDataCommand;
	[Inject] private PersistentDataProxy _persistentDataProxy;
	[Inject] private LayersMediator _layersMediator;
	[Inject] private PagesLayerMediator _pagesLayerMediator;

	public void Execute()
	{
		var isFirstLaunch = !_persistentDataProxy.Exists();
		if (isFirstLaunch)
			_saveFirstLaunchPersistentDataCommand.Execute();
		else
			_persistentDataProxy.RefreshFromFile();
		_persistentDataProxy.data.launchesCounter++;
		_persistentDataProxy.MarkAsDirty();

		_layersMediator.ShowScreen<NavigationPanelScreen>(LayerNames.Layer.NavigationScreen);
		_pagesLayerMediator.ShowPage<MainPageScreen>();
	}
}
