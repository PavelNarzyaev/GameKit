using System;
using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetUiController : IDisposable
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly IUiResetEventPublisher m_uiResetEventPublisher;
        private readonly IShowInitialUiCommand m_showInitialUiCommand;

        public ResetUiController(
            IPlayerStateProvider playerStateProvider,
            IUiResetEventPublisher uiResetEventPublisher,
            IShowInitialUiCommand showInitialUiCommand)
        {
            m_playerStateProvider = playerStateProvider;
            m_uiResetEventPublisher = uiResetEventPublisher;
            m_showInitialUiCommand = showInitialUiCommand;
            m_playerStateProvider.Replaced += HandlePlayerStateReplaced;
        }

        public void Dispose()
        {
            m_playerStateProvider.Replaced -= HandlePlayerStateReplaced;
        }

        private void HandlePlayerStateReplaced()
        {
            m_uiResetEventPublisher.PublishReset();
            m_showInitialUiCommand.Execute();
        }
    }
}
