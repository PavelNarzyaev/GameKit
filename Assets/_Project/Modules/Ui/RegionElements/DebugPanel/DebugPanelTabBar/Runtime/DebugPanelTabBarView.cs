using System.Collections.Generic;
using GameKit.UiRegions;
using GameKit.UiRegionsControl.Contracts;
using UnityEngine;
using Zenject;

namespace GameKit.DebugPanelTabBar
{
    public class DebugPanelTabBarView : UiRegionElement
    {
        [SerializeField] private DebugPanelTab stateDebugPageTab;
        [SerializeField] private DebugPanelTab timeDebugPageTab;
        [SerializeField] private DebugPanelTab currenciesDebugPageTab;
        [SerializeField] private DebugPanelTab energyDebugPageTab;
        [SerializeField] private DebugPanelTabBarCloseButton closeButton;

        private readonly Dictionary<string, DebugPanelTab> m_tabByAddressableId = new();

        [Inject] private DebugPanelTabBarPresenter m_presenter;
        private DebugPanelTab m_selectedTab;

        private void Awake()
        {
            SetUpTab(stateDebugPageTab, UiRegionElementAddressableIds.k_StateDebugPage);
            SetUpTab(timeDebugPageTab, UiRegionElementAddressableIds.k_TimeDebugPage);
            SetUpTab(currenciesDebugPageTab, UiRegionElementAddressableIds.k_CurrenciesDebugPage);
            SetUpTab(energyDebugPageTab, UiRegionElementAddressableIds.k_EnergyDebugPage);
            closeButton.AddClickListener(m_presenter.Close);
        }

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            m_presenter.PageChanged += HandlePageChanged;
        }

        private void OnDisable()
        {
            m_presenter.PageChanged -= HandlePageChanged;
        }

        private void SetUpTab(DebugPanelTab tab, string addressableId)
        {
            tab.SetSelected(m_presenter.CurrentPageAddressableId == addressableId);
            m_tabByAddressableId.Add(addressableId, tab);
            tab.AddClickListener(() => m_presenter.ShowPage(addressableId));
        }

        private void HandlePageChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            RefreshSelectedPage();
            RefreshCloseButton();
        }

        private void RefreshSelectedPage()
        {
            if (m_selectedTab)
            {
                m_selectedTab.SetSelected(false);
            }

            m_selectedTab = null;

            if (string.IsNullOrEmpty(m_presenter.CurrentPageAddressableId))
            {
                return;
            }

            if (!m_tabByAddressableId.TryGetValue(m_presenter.CurrentPageAddressableId, out var tab))
            {
                return;
            }

            m_selectedTab = tab;
            m_selectedTab.SetSelected(true);
        }

        private void RefreshCloseButton()
        {
            var isCloseButtonInteractable = !string.IsNullOrEmpty(m_presenter.CurrentPageAddressableId);
            closeButton.SetEnabled(isCloseButtonInteractable);
        }
    }
}
