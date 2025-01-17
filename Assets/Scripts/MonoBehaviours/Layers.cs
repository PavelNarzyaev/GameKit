using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class Layers : MonoBehaviour
{
	[SerializeField] private RectTransform _rectTransform;

	[Inject] private LayersMediator _layersMediator;
	[Inject] private DiContainer _diContainer;

	private Dictionary<string, RectTransform> _transformByLayerName = new();

	private void Awake()
	{
		var layers = Enum.GetValues(typeof(LayerNames.Layer));

		foreach (var layer in layers)
		{
			var layerName = layer.ToString();
			var layerContainer = new GameObject(layerName);

			layerContainer.transform.SetParent(_rectTransform, false);

			var layerRectTransform = layerContainer.AddComponent<RectTransform>();
			layerRectTransform.anchorMin = Vector2.zero;
			layerRectTransform.anchorMax = Vector2.one;
			layerRectTransform.offsetMin = Vector2.zero;
			layerRectTransform.offsetMax = Vector2.zero;

			_transformByLayerName.Add(layerName, layerRectTransform);
		}
	}

	private void OnEnable()
	{
		_layersMediator.showScreenEvent += ShowScreenEventHandler;
	}

	private void OnDisable()
	{
		_layersMediator.showScreenEvent -= ShowScreenEventHandler;
	}

	private void DestroyIfExists([CanBeNull] GameObject screen)
	{
		if (screen == null) return;
		screen.SetActive(false);
		Destroy(screen);
	}

	private void ShowScreenEventHandler(Type screenType, LayerNames.Layer layerName)
	{
		if (!_transformByLayerName.TryGetValue(layerName.ToString(), out var layerTransform))
			throw new Exception($"Rect transform for screen «{screenType}» is not found");
		var prefab = _diContainer.InstantiatePrefabResource(screenType.ToString(), layerTransform);
		if (prefab == null)
			throw new Exception($"Prefab for screen «{screenType}» is not found");
	}
}
