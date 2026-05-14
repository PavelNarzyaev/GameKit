using GameKit.Core.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateSavingController
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private IGameTickSource m_gameTickSource;

        [Inject]
        private void Inject()
        {
            m_gameTickSource.Ticked += HandleTicked;
        }

        private void HandleTicked()
        {
            if (m_playerStateProvider.IsDirty)
            {
                m_playerStateProvider.Save();
            }
        }
    }
}
