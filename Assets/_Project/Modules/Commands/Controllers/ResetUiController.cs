using JetBrains.Annotations;
using GameKit.PlayerState;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetUiController
    {
        [Inject] private PlayerStateProvider m_playerStateProvider;
        [Inject] private DestroyUiCommand m_destroyUiCommand;
        [Inject] private ShowInitialUiCommand m_showInitialUiCommand;

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
