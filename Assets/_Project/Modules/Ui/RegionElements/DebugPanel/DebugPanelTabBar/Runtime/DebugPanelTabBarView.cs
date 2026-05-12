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

        [Inject] private DebugPanelTabBarPresenter m_presenter;
        private DebugButton m_selectedTab;

        private void Awake()
        {
            logsIndicator.AddClickListener(HandleLogsIndicatorClicked);
            SetUpTab(stateDebugPageTab, UiRegionElementAddressableIds.k_StateDebugPage);
            SetUpTab(timeDebugPageTab, UiRegionElementAddressableIds.k_TimeDebugPage);
            SetUpTab(currenciesDebugPageTab, UiRegionElementAddressableIds.k_CurrenciesDebugPage);
            SetUpTab(energyDebugPageTab, UiRegionElementAddressableIds.k_EnergyDebugPage);
            SetUpTab(logsDebugPageTab, UiRegionElementAddressableIds.k_LogsDebugPage);
            closeButton.AddClickListener(m_presenter.Close);
        }

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            m_presenter.PageChanged += HandlePageChanged;
            m_presenter.LogsIndicatorStateChanged += HandleLogsIndicatorStateChanged;
        }

        private void OnDisable()
        {
            m_presenter.PageChanged -= HandlePageChanged;
            m_presenter.LogsIndicatorStateChanged -= HandleLogsIndicatorStateChanged;
        }

        private void HandleLogsIndicatorClicked()
        {
            m_presenter.ShowPage(UiRegionElementAddressableIds.k_LogsDebugPage);
        }

        private void SetUpTab(DebugButton tab, string addressableId)
        {
            tab.SetEnabled(m_presenter.CurrentPageAddressableId != addressableId);
            m_tabByAddressableId.Add(addressableId, tab);
            tab.AddClickListener(() => m_presenter.ShowPage(addressableId));
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

        private void RefreshLogsIndicator()
        {
            logsIndicator.SetState(m_presenter.LogsIndicatorState);
        }

        private void RefreshSelectedPage()
        {
            if (m_selectedTab)
            {
                m_selectedTab.SetEnabled(true);
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
            m_selectedTab.SetEnabled(false);
        }

        private void RefreshCloseButton()
        {
            var isCloseButtonInteractable = !string.IsNullOrEmpty(m_presenter.CurrentPageAddressableId);
            closeButton.SetEnabled(isCloseButtonInteractable);
        }
    }
}
