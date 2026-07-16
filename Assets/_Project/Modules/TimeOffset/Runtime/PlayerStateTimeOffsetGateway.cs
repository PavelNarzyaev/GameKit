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

        public int OffsetSeconds => m_playerStateProvider.TimeOffsetSeconds.CurrentValue;

        public void SetOffsetSeconds(int offsetSeconds)
        {
            if (OffsetSeconds == offsetSeconds)
            {
                return;
            }

            m_playerStateProvider.SetTimeOffsetSeconds(offsetSeconds);
            Changed?.Invoke();
        }
    }
}
