using System;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class PlayerStateTimeOffsetGateway
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        public PlayerStateTimeOffsetGateway(IPlayerStateProvider playerStateProvider)
        {
            m_playerStateProvider = playerStateProvider;
        }

        public int OffsetSeconds => m_playerStateProvider.Data?.TimeOffsetSeconds ?? 0;

        public void SetOffsetSeconds(int offsetSeconds)
        {
            if (m_playerStateProvider.Data.TimeOffsetSeconds == offsetSeconds)
            {
                return;
            }

            m_playerStateProvider.Edit(state => state.TimeOffsetSeconds = offsetSeconds);
            Changed?.Invoke();
        }
    }
}
