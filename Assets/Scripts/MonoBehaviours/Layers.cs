using System;
using System.Collections.Generic;
using Data.Constants;
using Mediators;
using MonoBehaviours.Screens;
using UnityEngine;
using Zenject;

namespace MonoBehaviours
{
	public class Layers : MonoBehaviour
	{
		[SerializeField] private RectTransform _rectTransform;

		[Inject] private LayersMediator _layersMediator;
		[Inject] private DiContainer _diContainer;

		private Dictionary<string, RectTransform> _transformByLayerName = new();
		private Dictionary<Type, ScreenAbstract> _screenPrefabByType = new();

		private void Awake()
		{
			var layers = Enum.GetValues(typeof(Layer));

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
			_layersMediator.showScreenEvent += ShowScreenHandler;
			_layersMediator.hideScreenIfExistsEvent += HideScreenIfExistsHandler;
			_layersMediator.destroyAllScreensEvent += DestroyAllScreensHandler;
		}

		private void OnDisable()
		{
			_layersMediator.showScreenEvent -= ShowScreenHandler;
			_layersMediator.hideScreenIfExistsEvent -= HideScreenIfExistsHandler;
			_layersMediator.destroyAllScreensEvent -= DestroyAllScreensHandler;
		}

		private void ShowScreenHandler(Type screenType, Layer layerName)
		{
			if (_screenPrefabByType.TryGetValue(screenType, out var screen))
				screen.gameObject.SetActive(true);
			else
			{
				if (!_transformByLayerName.TryGetValue(layerName.ToString(), out var layerTransform))
					throw new Exception($"Rect transform for screen «{screenType}» is not found");
				var screenPrefab = _diContainer.InstantiatePrefabResource(screenType.Name, layerTransform);
				if (screenPrefab == null)
					throw new Exception($"Prefab for screen «{screenType}» is not found");
				var screenComponent = screenPrefab.GetComponent<ScreenAbstract>();
				if (screenComponent == null)
					throw new Exception($"Screen prefab must have a «{nameof(ScreenAbstract)}» component.");
				_screenPrefabByType.Add(screenType, screenComponent);
			}
		}

		private void HideScreenIfExistsHandler(Type screenType)
		{
			if (!_screenPrefabByType.TryGetValue(screenType, out var screen))
				return;
			if (screen.IsCached())
				screen.gameObject.SetActive(false);
			else
			{
				DestroyPrefab(screen.gameObject);
				_screenPrefabByType.Remove(screenType);
			}
		}

		private void DestroyAllScreensHandler()
		{
			foreach (var screen in _screenPrefabByType.Values)
				DestroyPrefab(screen.gameObject);
			_screenPrefabByType.Clear();
		}

		private void DestroyPrefab(GameObject prefab)
		{
			prefab.SetActive(false);
			Destroy(prefab);
		}
	}
}
