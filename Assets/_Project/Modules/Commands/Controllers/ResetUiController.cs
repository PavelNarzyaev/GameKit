using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetUiController
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private IDestroyUiCommand m_destroyUiCommand;
        [Inject] private IShowInitialUiCommand m_showInitialUiCommand;

        [Inject]
        private void Inject()
        {
            m_playerStateProvider.RefreshedFromJson += HandleRefreshedFromJson;
        }

        private void HandleRefreshedFromJson()
        {
            m_destroyUiCommand.Execute();
            m_showInitialUiCommand.Execute();
        }
    }
}
