using GameKit.UiDebugPanel.Contracts;
using JetBrains.Annotations;

namespace GameKit.DebugPanelMessage
{
    [UsedImplicitly]
    public class DebugPanelMessagePresenter
    {
        private readonly IDebugPanelMessageNavigator m_debugPanelMessageNavigator;

        public DebugPanelMessagePresenter(IDebugPanelMessageNavigator debugPanelMessageNavigator)
        {
            m_debugPanelMessageNavigator = debugPanelMessageNavigator;
        }

        public void Hide(string addressableId)
        {
            m_debugPanelMessageNavigator.HideMessage(addressableId);
        }
    }
}
