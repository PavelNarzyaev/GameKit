using System.Collections.Generic;
using GameKit.UiDebugShared;
using GameKit.UiRegionsControl.Contracts;
using UnityEngine;
using Zenject;

namespace GameKit.DebugToolBar
{
    public class DebugToolBarPageTabsView : MonoBehaviour
    {
        [SerializeField] private DebugButton stateDebugPageTab;
        [SerializeField] private DebugButton timeDebugPageTab;
        [SerializeField] private DebugButton currenciesDebugPageTab;
        [SerializeField] private DebugButton energyDebugPageTab;
        [SerializeField] private DebugButton logsDebugPageTab;

        private readonly Dictionary<string, DebugButton> m_tabByAddressableId = new();

        [Inject] private DebugToolBarPageTabsPresenter m_presenter;
        private DebugButton m_selectedTab;

        private void Awake()
        {
            SetUpTab(stateDebugPageTab, UiRegionElementAddressableIds.k_StateDebugPage);
            SetUpTab(timeDebugPageTab, UiRegionElementAddressableIds.k_TimeDebugPage);
            SetUpTab(currenciesDebugPageTab, UiRegionElementAddressableIds.k_CurrenciesDebugPage);
            SetUpTab(energyDebugPageTab, UiRegionElementAddressableIds.k_EnergyDebugPage);
            SetUpTab(logsDebugPageTab, UiRegionElementAddressableIds.k_LogsDebugPage);
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
    }
}
