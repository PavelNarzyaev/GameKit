using System;
using GameKit.UiDebugPanel.Contracts;
using JetBrains.Annotations;

namespace GameKit.DebugPanelTabBar
{
    [UsedImplicitly]
    public class DebugPanelCloseButtonPresenter : IDisposable
    {
        private readonly IDebugPanelPageNavigator m_debugPanelPageNavigator;

        public event Action StateChanged;

        public bool IsInteractable => m_debugPanelPageNavigator.CurrentPageAddressableId != null;

        public DebugPanelCloseButtonPresenter(IDebugPanelPageNavigator debugPanelPageNavigator)
        {
            m_debugPanelPageNavigator = debugPanelPageNavigator;
            m_debugPanelPageNavigator.PageChanged += HandlePageChanged;
        }

        public void Dispose()
        {
            m_debugPanelPageNavigator.PageChanged -= HandlePageChanged;
        }

        public void Close()
        {
            m_debugPanelPageNavigator.Close();
        }

        private void HandlePageChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
