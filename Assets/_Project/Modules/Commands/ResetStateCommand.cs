using JetBrains.Annotations;
using GameKit.PlayerState;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetStateCommand
    {
        [Inject] private PlayerStateProvider m_playerStateProvider;
        [Inject] private LaunchCommand m_launchCommand;
        [Inject] private DestroyUiCommand m_destroyUiCommand;

        public void Execute()
        {
            m_destroyUiCommand.Execute();
            m_playerStateProvider.Delete();
            m_launchCommand.Execute();
        }
    }
}
