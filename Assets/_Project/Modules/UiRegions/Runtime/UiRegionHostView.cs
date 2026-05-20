using System;
using System.Collections.Generic;
using GameKit.UiRegions.Contracts;
using UnityEngine;
using Zenject;

namespace GameKit.UiRegions
{
    public class UiRegionHostView : MonoBehaviour
    {
        [SerializeField] private RectTransform backgroundRegion;
        [SerializeField] private RectTransform topPanelRegion;
        [SerializeField] private RectTransform pageRegion;
        [SerializeField] private RectTransform popupsRegion;
        [SerializeField] private RectTransform debugPanelPageRegion;
        [SerializeField] private RectTransform debugPanelMessageRegion;
        [SerializeField] private RectTransform debugPanelTabBarRegion;

        private readonly Dictionary<UiRegionId, RectTransform> m_transformByRegionId = new();
        [Inject] private IUiRegionHostPresenter m_presenter;
        [Inject] private UiRegionElementSpawner m_regionElementSpawner;

        private void Awake()
        {
            m_transformByRegionId.Add(UiRegionId.Background, backgroundRegion);
            m_transformByRegionId.Add(UiRegionId.TopPanel, topPanelRegion);
            m_transformByRegionId.Add(UiRegionId.Page, pageRegion);
            m_transformByRegionId.Add(UiRegionId.Popups, popupsRegion);
            m_transformByRegionId.Add(UiRegionId.DebugPanelPage, debugPanelPageRegion);
            m_transformByRegionId.Add(UiRegionId.DebugPanelMessage, debugPanelMessageRegion);
            m_transformByRegionId.Add(UiRegionId.DebugPanelTabBar, debugPanelTabBarRegion);
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
            m_regionElementSpawner.Show(addressableId, GetRegionTransformById(regionId));
        }

        private void HandleRegionElementHidingIfExists(string addressableId)
        {
            m_regionElementSpawner.HideIfExists(addressableId);
        }

        private void HandleAllRegionElementsDestroying()
        {
            m_regionElementSpawner.DestroyAll();
        }

        private void HandleRegionElementIndexSetting(string addressableId, int index)
        {
            m_regionElementSpawner.SetSiblingIndex(addressableId, index);
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
    }
}
