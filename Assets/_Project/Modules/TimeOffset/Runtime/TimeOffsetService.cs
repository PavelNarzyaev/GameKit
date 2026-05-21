using System;
using GameKit.TimeOffset.Contracts;
using JetBrains.Annotations;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetService : ITimeOffsetService
    {
        private readonly PlayerStateTimeOffsetGateway m_gateway;

        public TimeOffsetService(PlayerStateTimeOffsetGateway gateway)
        {
            m_gateway = gateway;
        }

        public event Action Changed
        {
            add => m_gateway.Changed += value;
            remove => m_gateway.Changed -= value;
        }

        public int OffsetSeconds => m_gateway.OffsetSeconds;

        public void AddSeconds(int deltaSeconds)
        {
            var nextOffset = (long)OffsetSeconds + deltaSeconds;
            if (nextOffset < int.MinValue || nextOffset > int.MaxValue)
            {
                return;
            }

            m_gateway.SetOffsetSeconds((int)nextOffset);
        }
    }
}
