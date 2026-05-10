using System;
using GameKit.TimeOffset.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetService : ITimeOffsetService
    {
        [Inject] private PlayerStateTimeOffsetGateway m_gateway;

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
