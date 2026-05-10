using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetStateCommand : IResetStateCommand
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private ILaunchCommand m_launchCommand;
        [Inject] private IDestroyUiCommand m_destroyUiCommand;

        public void Execute()
        {
            m_destroyUiCommand.Execute();
            m_playerStateProvider.Delete();
            m_launchCommand.Execute();
        }
    }
}
