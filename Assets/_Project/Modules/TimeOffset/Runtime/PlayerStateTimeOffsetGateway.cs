using System;
using GameKit.PlayerState;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class PlayerStateTimeOffsetGateway
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        [Inject]
        private void Inject()
        {
            m_playerStateProvider.RefreshedFromJson += HandlePlayerStateRefreshedFromJson;
        }

        public int OffsetSeconds => m_playerStateProvider.Data?.TimeOffsetSeconds ?? 0;

        public void SetOffsetSeconds(int offsetSeconds)
        {
            m_playerStateProvider.Data.TimeOffsetSeconds = offsetSeconds;
            m_playerStateProvider.MarkAsDirty();
            Changed?.Invoke();
        }

        private void HandlePlayerStateRefreshedFromJson()
        {
            Changed?.Invoke();
        }
    }
}
