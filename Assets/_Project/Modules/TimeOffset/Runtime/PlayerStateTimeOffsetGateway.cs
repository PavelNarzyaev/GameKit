using System;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class PlayerStateTimeOffsetGateway
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        public int OffsetSeconds => m_playerStateProvider.Data?.TimeOffsetSeconds ?? 0;

        public void SetOffsetSeconds(int offsetSeconds)
        {
            if (m_playerStateProvider.Data.TimeOffsetSeconds == offsetSeconds)
            {
                return;
            }

            m_playerStateProvider.Data.TimeOffsetSeconds = offsetSeconds;
            m_playerStateProvider.MarkAsDirty();
            Changed?.Invoke();
        }
    }
}
