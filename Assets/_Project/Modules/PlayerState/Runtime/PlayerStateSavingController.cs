using GameKit.Core.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateSavingController
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly IGameTickSource m_gameTickSource;

        public PlayerStateSavingController(IPlayerStateProvider playerStateProvider, IGameTickSource gameTickSource)
        {
            m_playerStateProvider = playerStateProvider;
            m_gameTickSource = gameTickSource;
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
