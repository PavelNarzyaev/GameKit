using Zenject;

public class PopupsLayerMediator
{
	[Inject] private LayersMediator _layersMediator;

	public void ShowPopup()
	{
		_layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
	}

	public void ClosePopup()
	{
		_layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
	}
}
