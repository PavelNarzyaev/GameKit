using System.Globalization;
using GameKit.Commands;
using GameKit.Core;
using GameKit.PlayerState;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.StateDebugPage
{
    [UsedImplicitly]
    public class StateDebugPagePresenter
    {
        [Inject] private PlayerStateProvider m_playerStateProvider;
        [Inject] private ResetStateCommand m_resetStateCommand;
        [Inject] private StateClipboardProxy.StateClipboardProxy m_stateClipboardProxy;

        public void CopyStateToClipboard()
        {
            m_stateClipboardProxy.CopyStateToClipboard();
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
