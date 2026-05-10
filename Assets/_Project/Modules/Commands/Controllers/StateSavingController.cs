using JetBrains.Annotations;
using GameKit.PlayerState;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class StateSavingController
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private GameKitTickController m_gameKitTickController;

        [Inject]
        private void Inject()
        {
            m_gameKitTickController.Ticked += HandleTicked;
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
