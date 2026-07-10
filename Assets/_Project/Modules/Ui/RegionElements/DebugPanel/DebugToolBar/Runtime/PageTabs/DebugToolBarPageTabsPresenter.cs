using System;
using GameKit.UiDebugPanel.Contracts;
using JetBrains.Annotations;

namespace GameKit.DebugToolBar
{
    [UsedImplicitly]
    public class DebugToolBarPageTabsPresenter : IDisposable
    {
        private readonly IDebugPanelPageNavigator m_debugPanelPageNavigator;

        public event Action PageChanged;

        public string CurrentPageAddressableId => m_debugPanelPageNavigator.CurrentPageAddressableId;

        public DebugToolBarPageTabsPresenter(IDebugPanelPageNavigator debugPanelPageNavigator)
        {
            m_debugPanelPageNavigator = debugPanelPageNavigator;
            m_debugPanelPageNavigator.PageChanged += HandlePageChanged;
        }

        public void Dispose()
        {
            m_debugPanelPageNavigator.PageChanged -= HandlePageChanged;
        }

        public void ShowPage(string addressableId)
        {
            m_debugPanelPageNavigator.Show(addressableId);
        }

        private void HandlePageChanged()
        {
            PageChanged?.Invoke();
        }
    }
}
