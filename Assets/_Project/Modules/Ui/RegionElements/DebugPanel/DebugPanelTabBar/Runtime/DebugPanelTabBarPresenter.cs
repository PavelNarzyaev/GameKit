using System;
using GameKit.UiDebugPanel.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.DebugPanelTabBar
{
    [UsedImplicitly]
    public class DebugPanelTabBarPresenter
    {
        [Inject] private IDebugPanelNavigator m_debugPanelNavigator;

        public event Action PageChanged
        {
            add => m_debugPanelNavigator.PageChanged += value;
            remove => m_debugPanelNavigator.PageChanged -= value;
        }

        public string CurrentPageAddressableId => m_debugPanelNavigator.CurrentPageAddressableId;

        public void ShowPage(string addressableId)
        {
            m_debugPanelNavigator.ShowPage(addressableId);
        }

        public void Close()
        {
            m_debugPanelNavigator.Close();
        }
    }
}
