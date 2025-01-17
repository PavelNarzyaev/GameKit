using System;
using Zenject;

public class PagesLayerMediator
{
	[Inject] private LayersMediator _layersMediator;

	private Type _currentPageType;

	public void ShowPage(Type screenType)
	{
		if (_currentPageType != null)
			_layersMediator.DestroyScreenIfExists(_currentPageType);
		_layersMediator.ShowScreen(screenType, LayerNames.Layer.Page);
		_currentPageType = screenType;
	}
}
