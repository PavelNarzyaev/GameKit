using GameKit.UiDebugPanel.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.DebugPanelMessage
{
    [UsedImplicitly]
    public class DebugPanelMessagePresenter
    {
        [Inject] private IDebugPanelMessageNavigator m_debugPanelMessageNavigator;

        public void Hide(string addressableId)
        {
            m_debugPanelMessageNavigator.HideMessage(addressableId);
        }
    }
}
