using System;
using System.Collections.Generic;
using GameKit.UiRegionsControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameKit.UiRegions
{
    public class UiRegionHostView : MonoBehaviour
    {
        [SerializeField] private RectTransform topPanelRegion;
        [SerializeField] private RectTransform pageRegion;
        [SerializeField] private RectTransform popupsRegion;
        [SerializeField] private RectTransform debugPanelPageRegion;
        [SerializeField] private RectTransform debugPanelTabBarRegion;

        private readonly Dictionary<string, UiRegionElement> m_elementByAddressableId = new();
        private readonly Dictionary<UiRegionId, RectTransform> m_transformByRegionId = new();
        [Inject] private DiContainer m_diContainer;
        [Inject] private UiRegionHostPresenter m_presenter;

        private void Awake()
        {
            m_transformByRegionId.Add(UiRegionId.DebugPanelPage, debugPanelPageRegion);
            m_transformByRegionId.Add(UiRegionId.DebugPanelTabBar, debugPanelTabBarRegion);
            m_transformByRegionId.Add(UiRegionId.TopPanel, topPanelRegion);
            m_transformByRegionId.Add(UiRegionId.Page, pageRegion);
            m_transformByRegionId.Add(UiRegionId.Popups, popupsRegion);
        }

        private void OnEnable()
        {
            m_presenter.RegionElementShowing += HandleRegionElementShowing;
            m_presenter.RegionElementHidingIfExists += HandleRegionElementHidingIfExists;
            m_presenter.AllRegionElementsDestroying += HandleAllRegionElementsDestroying;
            m_presenter.RegionElementIndexSetting += HandleRegionElementIndexSetting;
            m_presenter.RegionActivating += HandleRegionActivating;
        }

        private void OnDisable()
        {
            m_presenter.RegionElementShowing -= HandleRegionElementShowing;
            m_presenter.RegionElementHidingIfExists -= HandleRegionElementHidingIfExists;
            m_presenter.AllRegionElementsDestroying -= HandleAllRegionElementsDestroying;
            m_presenter.RegionElementIndexSetting -= HandleRegionElementIndexSetting;
            m_presenter.RegionActivating -= HandleRegionActivating;
        }

        private void HandleRegionElementShowing(string addressableId, UiRegionId regionId)
        {
            if (m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                element.gameObject.SetActive(true);
            }
            else
            {
                var regionTransform = GetRegionTransformById(regionId);

                var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(addressableId);
                var elementPrefab = asyncOperationHandle.WaitForCompletion();
                if (!elementPrefab)
                {
                    Addressables.Release(asyncOperationHandle);
                    throw new Exception($"Prefab for UI region element \"{addressableId}\" is not found");
                }

                var elementGameObject = m_diContainer.InstantiatePrefab(elementPrefab, regionTransform);
                Addressables.Release(asyncOperationHandle);

                var elementComponent = elementGameObject.GetComponent<UiRegionElement>();
                if (!elementComponent)
                {
                    throw new Exception($"UI region element prefab must have a \"{nameof(UiRegionElement)}\" component.");
                }

                elementComponent.AddressableId = addressableId;
                m_elementByAddressableId.Add(addressableId, elementComponent);
            }
        }

        private void HandleRegionElementHidingIfExists(string addressableId)
        {
            if (!m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                return;
            }

            if (element.IsCached)
            {
                element.gameObject.SetActive(false);
            }
            else
            {
                DestroyRegionElementGameObject(element);
                m_elementByAddressableId.Remove(addressableId);
            }
        }

        private void HandleAllRegionElementsDestroying()
        {
            foreach (var element in m_elementByAddressableId.Values)
            {
                DestroyRegionElementGameObject(element);
            }

            m_elementByAddressableId.Clear();
        }

        private void HandleRegionElementIndexSetting(string addressableId, int index)
        {
            if (!m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                Debug.LogWarning($"Can't set sibling index for UI region element \"{addressableId}\" because it is not loaded.");
                return;
            }

            element.transform.SetSiblingIndex(index);
        }

        private void HandleRegionActivating(UiRegionId regionId, bool isActive)
        {
            GetRegionTransformById(regionId).gameObject.SetActive(isActive);
        }

        private Transform GetRegionTransformById(UiRegionId regionId)
        {
            if (!m_transformByRegionId.TryGetValue(regionId, out var regionTransform))
            {
                throw new Exception($"Rect transform region \"{regionId}\" is not found");
            }

            return regionTransform;
        }

        private static void DestroyRegionElementGameObject(UiRegionElement element)
        {
            var elementGameObject = element.gameObject;
            elementGameObject.SetActive(false);
            Destroy(elementGameObject);
        }
    }
}
