using System.Globalization;
using GameKit.Commands.Contracts;
using GameKit.Core;
using GameKit.PlayerState.Contracts;
using GameKit.StateClipboardProxy.Contracts;
using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.StateDebugPage
{
    [UsedImplicitly]
    public class StateDebugPagePresenter
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private IResetStateCommand m_resetStateCommand;
        [Inject] private IStateClipboardProxy m_stateClipboardProxy;
        [Inject] private IDebugPanelMessageNavigator m_debugPanelMessageNavigator;

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
            m_resetStateCommand.Execute();
        }

        public string GetUserIdText()
        {
            return State.UserId;
        }

        public string GetFirstLaunchTimeText()
        {
            return State.FirstLaunchTimestamp.ToLocalDatetimeString();
        }

        public string GetLaunchCountText()
        {
            return State.LaunchesCounter.ToString(CultureInfo.InvariantCulture);
        }

        private PlayerStateDto State => m_playerStateProvider.Data;
    }
}
