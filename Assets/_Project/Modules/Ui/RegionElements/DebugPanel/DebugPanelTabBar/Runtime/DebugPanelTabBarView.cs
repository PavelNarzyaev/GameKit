using System.Collections.Generic;
using GameKit.UiDebugShared;
using GameKit.UiRegions;
using GameKit.UiRegionsControl.Contracts;
using UnityEngine;
using Zenject;

namespace GameKit.DebugPanelTabBar
{
    public class DebugPanelTabBarView : UiRegionElement
    {
        [SerializeField] private DebugPanelTabBarLogsIndicator logsIndicator;
        [SerializeField] private DebugButton stateDebugPageTab;
        [SerializeField] private DebugButton timeDebugPageTab;
        [SerializeField] private DebugButton currenciesDebugPageTab;
        [SerializeField] private DebugButton energyDebugPageTab;
        [SerializeField] private DebugButton logsDebugPageTab;
        [SerializeField] private DebugButton closeButton;

        private readonly Dictionary<string, DebugButton> m_tabByAddressableId = new();

        [Inject] private DebugPanelTabBarPresenter m_tabBarPresenter;
        [Inject] private DebugPanelLogsIndicatorPresenter m_logsIndicatorPresenter;
        [Inject] private DebugPanelCloseButtonPresenter m_closeButtonPresenter;
        private DebugButton m_selectedTab;

        private void Awake()
        {
            logsIndicator.AddClickListener(HandleLogsIndicatorClicked);
            SetUpTab(stateDebugPageTab, UiRegionElementAddressableIds.k_StateDebugPage);
            SetUpTab(timeDebugPageTab, UiRegionElementAddressableIds.k_TimeDebugPage);
            SetUpTab(currenciesDebugPageTab, UiRegionElementAddressableIds.k_CurrenciesDebugPage);
            SetUpTab(energyDebugPageTab, UiRegionElementAddressableIds.k_EnergyDebugPage);
            SetUpTab(logsDebugPageTab, UiRegionElementAddressableIds.k_LogsDebugPage);
            closeButton.AddClickListener(m_closeButtonPresenter.Close);
        }

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            m_tabBarPresenter.PageChanged += HandlePageChanged;
            m_logsIndicatorPresenter.StateChanged += HandleLogsIndicatorStateChanged;
            m_closeButtonPresenter.StateChanged += HandleCloseButtonStateChanged;
        }

        private void OnDisable()
        {
            m_tabBarPresenter.PageChanged -= HandlePageChanged;
            m_logsIndicatorPresenter.StateChanged -= HandleLogsIndicatorStateChanged;
            m_closeButtonPresenter.StateChanged -= HandleCloseButtonStateChanged;
        }

        private void HandleLogsIndicatorClicked()
        {
            m_tabBarPresenter.ShowPage(UiRegionElementAddressableIds.k_LogsDebugPage);
        }

        private void SetUpTab(DebugButton tab, string addressableId)
        {
            tab.SetEnabled(m_tabBarPresenter.CurrentPageAddressableId != addressableId);
            m_tabByAddressableId.Add(addressableId, tab);
            tab.AddClickListener(() => m_tabBarPresenter.ShowPage(addressableId));
        }

        private void HandlePageChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            RefreshLogsIndicator();
            RefreshSelectedPage();
            RefreshCloseButton();
        }

        private void HandleLogsIndicatorStateChanged()
        {
            RefreshLogsIndicator();
        }

        private void HandleCloseButtonStateChanged()
        {
            RefreshCloseButton();
        }

        private void RefreshLogsIndicator()
        {
            logsIndicator.SetState(m_logsIndicatorPresenter.State);
        }

        private void RefreshSelectedPage()
        {
            if (m_selectedTab)
            {
                m_selectedTab.SetEnabled(true);
            }

            m_selectedTab = null;

            if (string.IsNullOrEmpty(m_tabBarPresenter.CurrentPageAddressableId))
            {
                return;
            }

            if (!m_tabByAddressableId.TryGetValue(m_tabBarPresenter.CurrentPageAddressableId, out var tab))
            {
                return;
            }

            m_selectedTab = tab;
            m_selectedTab.SetEnabled(false);
        }

        private void RefreshCloseButton()
        {
            closeButton.SetEnabled(m_closeButtonPresenter.IsInteractable);
        }
    }
}
