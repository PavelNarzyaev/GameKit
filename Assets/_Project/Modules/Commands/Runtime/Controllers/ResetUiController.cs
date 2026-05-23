using System;
using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetUiController : IDisposable
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly IDestroyUiCommand m_destroyUiCommand;
        private readonly IShowInitialUiCommand m_showInitialUiCommand;

        public ResetUiController(
            IPlayerStateProvider playerStateProvider,
            IDestroyUiCommand destroyUiCommand,
            IShowInitialUiCommand showInitialUiCommand)
        {
            m_playerStateProvider = playerStateProvider;
            m_destroyUiCommand = destroyUiCommand;
            m_showInitialUiCommand = showInitialUiCommand;
            m_playerStateProvider.Replaced += HandlePlayerStateReplaced;
        }

        public void Dispose()
        {
            m_playerStateProvider.Replaced -= HandlePlayerStateReplaced;
        }

        private void HandlePlayerStateReplaced()
        {
            m_destroyUiCommand.Execute();
            m_showInitialUiCommand.Execute();
        }
    }
}
