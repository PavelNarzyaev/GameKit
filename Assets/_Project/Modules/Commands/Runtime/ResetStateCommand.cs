using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetStateCommand : IResetStateCommand
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly ILaunchCommand m_launchCommand;
        private readonly IDestroyUiCommand m_destroyUiCommand;

        public ResetStateCommand(
            IPlayerStateProvider playerStateProvider,
            ILaunchCommand launchCommand,
            IDestroyUiCommand destroyUiCommand)
        {
            m_playerStateProvider = playerStateProvider;
            m_launchCommand = launchCommand;
            m_destroyUiCommand = destroyUiCommand;
        }

        public void Execute()
        {
            m_destroyUiCommand.Execute();
            m_playerStateProvider.Delete();
            m_launchCommand.Execute();
        }
    }
}
