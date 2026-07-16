using System.Globalization;
using GameKit.Core;
using GameKit.PlayerState.Contracts;
using GameKit.StateClipboardProxy.Contracts;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;

namespace GameKit.StateDebugPage
{
    [UsedImplicitly]
    public class StateDebugPagePresenter
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly IStateClipboardProxy m_stateClipboardProxy;
        private readonly IDebugPanelMessageNavigator m_debugPanelMessageNavigator;

        public StateDebugPagePresenter(
            IPlayerStateProvider playerStateProvider,
            IStateClipboardProxy stateClipboardProxy,
            IDebugPanelMessageNavigator debugPanelMessageNavigator)
        {
            m_playerStateProvider = playerStateProvider;
            m_stateClipboardProxy = stateClipboardProxy;
            m_debugPanelMessageNavigator = debugPanelMessageNavigator;
        }

        public void CopyStateToClipboard()
        {
            m_stateClipboardProxy.CopyStateToClipboard();
            m_debugPanelMessageNavigator.ShowMessage(UiRegionElementAddressableIds.k_DebugPanelMessage);
        }

        public void PasteStateFromClipboard()
        {
            m_stateClipboardProxy.PasteStateFromClipboard();
        }

        public void ResetState()
        {
            m_playerStateProvider.Reset();
        }

        public string GetUserIdText()
        {
            return m_playerStateProvider.UserId;
        }

        public string GetFirstLaunchTimeText()
        {
            return m_playerStateProvider.FirstLaunchTimestamp.ToLocalDatetimeString();
        }

        public string GetLaunchCountText()
        {
            return m_playerStateProvider.LaunchesCounter.ToString(CultureInfo.InvariantCulture);
        }
    }
}
