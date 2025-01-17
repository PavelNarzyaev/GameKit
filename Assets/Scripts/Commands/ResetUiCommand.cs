using Zenject;

public class ResetUiCommand
{
	[Inject] private LayersMediator _layersMediator;
	[Inject] private PagesLayerMediator _pagesLayerMediator;

	public void Execute()
	{
		_layersMediator.DestroyAllScreens();

		_layersMediator.ShowScreen(typeof(NavigationPanelScreen), Layer.NavigationScreen);
		_pagesLayerMediator.ShowPage(typeof(MainPageScreen));
	}
}
