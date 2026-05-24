using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.UiReset.Contracts;
using JetBrains.Annotations;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetStateCommand : IResetStateCommand
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly ILaunchCommand m_launchCommand;
        private readonly IUiResetEventPublisher m_uiResetEventPublisher;

        public ResetStateCommand(
            IPlayerStateProvider playerStateProvider,
            ILaunchCommand launchCommand,
            IUiResetEventPublisher uiResetEventPublisher)
        {
            m_playerStateProvider = playerStateProvider;
            m_launchCommand = launchCommand;
            m_uiResetEventPublisher = uiResetEventPublisher;
        }

        public void Execute()
        {
            m_uiResetEventPublisher.PublishReset();
            m_playerStateProvider.Delete();
            m_launchCommand.Execute();
        }
    }
}
