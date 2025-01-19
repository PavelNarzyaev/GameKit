using System;
using Zenject;

public class PagesLayerMediator
{
	[Inject] private LayersMediator _layersMediator;

	public event Action changePageEvent;
	public Type currentPageType { get; private set; }

	public void ShowPage(Type pageScreenType)
	{
		if (currentPageType != null)
			_layersMediator.DestroyScreenIfExists(currentPageType);
		_layersMediator.ShowScreen(pageScreenType, Layer.Page);
		currentPageType = pageScreenType;
		changePageEvent?.Invoke();
	}
}
