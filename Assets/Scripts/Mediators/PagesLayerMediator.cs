using System;
using UnityEngine;
using Zenject;

public class PagesLayerMediator
{
	[Inject] private LayersMediator _layersMediator;

	private Type _currentPage;

	public void ShowPage<T>() where T : MonoBehaviour
	{
		if (_currentPage != null)
			_layersMediator.DestroyScreenIfExists(_currentPage);
		_layersMediator.ShowScreen<T>(LayerNames.Layer.Page);
		_currentPage = typeof(T);
	}
}
