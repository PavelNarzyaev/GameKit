using System;
using System.Collections.Generic;
using Data.Constants;
using Mediators;
using MonoBehaviours.Screens;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace MonoBehaviours
{
    public class Layers : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        private readonly Dictionary<Type, ScreenAbstract> m_screenByType = new();
        private readonly Dictionary<Layer, RectTransform> m_transformByLayerName = new();
        [Inject] private DiContainer m_diContainer;
        [Inject] private LayersMediator m_layersMediator;

        private void Awake()
        {
            var layers = Enum.GetValues(typeof(Layer));

            foreach (var layer in layers)
            {
                var layerName = layer.ToString();
                var layerContainer = new GameObject(layerName);

                layerContainer.transform.SetParent(rectTransform, false);

                var layerRectTransform = layerContainer.AddComponent<RectTransform>();
                layerRectTransform.anchorMin = Vector2.zero;
                layerRectTransform.anchorMax = Vector2.one;
                layerRectTransform.offsetMin = Vector2.zero;
                layerRectTransform.offsetMax = Vector2.zero;

                m_transformByLayerName.Add((Layer)layer, layerRectTransform);
            }
        }

        private void OnEnable()
        {
            m_layersMediator.ShowScreenEvent += ShowScreenHandler;
            m_layersMediator.HideScreenIfExistsEvent += HideScreenIfExistsHandler;
            m_layersMediator.DestroyAllScreensEvent += DestroyAllScreensHandler;
            m_layersMediator.SetScreenIndexEvent += SetScreenIndexHandler;
        }

        private void OnDisable()
        {
            m_layersMediator.ShowScreenEvent -= ShowScreenHandler;
            m_layersMediator.HideScreenIfExistsEvent -= HideScreenIfExistsHandler;
            m_layersMediator.DestroyAllScreensEvent -= DestroyAllScreensHandler;
            m_layersMediator.SetScreenIndexEvent -= SetScreenIndexHandler;
        }

        private void ShowScreenHandler(Type screenType, Layer layer)
        {
            if (m_screenByType.TryGetValue(screenType, out var screen))
            {
                screen.gameObject.SetActive(true);
            }
            else
            {
                if (!m_transformByLayerName.TryGetValue(layer, out var layerTransform))
                {
                    throw new Exception($"Rect transform for screen «{screenType}» is not found");
                }

                var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(screenType.Name);
                var screenPrefab = asyncOperationHandle.WaitForCompletion();
                if (!screenPrefab)
                {
                    Addressables.Release(asyncOperationHandle);
                    throw new Exception($"Prefab for screen «{screenType}» is not found");
                }

                var screenGameObject = m_diContainer.InstantiatePrefab(screenPrefab, layerTransform);
                Addressables.Release(asyncOperationHandle);

                var screenComponent = screenGameObject.GetComponent<ScreenAbstract>();
                if (!screenComponent)
                {
                    throw new Exception($"Screen prefab must have a «{nameof(ScreenAbstract)}» component.");
                }

                m_screenByType.Add(screenType, screenComponent);
            }
        }

        private void HideScreenIfExistsHandler(Type screenType)
        {
            if (!m_screenByType.TryGetValue(screenType, out var screen))
            {
                return;
            }

            if (screen.IsCached())
            {
                screen.gameObject.SetActive(false);
            }
            else
            {
                DestroyScreenGameObject(screen);
                m_screenByType.Remove(screenType);
            }
        }

        private void DestroyAllScreensHandler()
        {
            foreach (var screen in m_screenByType.Values)
            {
                DestroyScreenGameObject(screen);
            }

            m_screenByType.Clear();
        }

        private void SetScreenIndexHandler(Type screenType, int index)
        {
            m_screenByType[screenType].transform.SetSiblingIndex(index);
        }

        private static void DestroyScreenGameObject(ScreenAbstract screen)
        {
            var screenGameObject = screen.gameObject;
            screenGameObject.SetActive(false);
            Destroy(screenGameObject);
            Addressables.ReleaseInstance(screenGameObject);
        }
    }
}
