using System;
using GameKit.Core.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateFactory : IPlayerStateFactory
    {
        private readonly IRealTimeSource m_realTimeSource;

        public PlayerStateFactory(IRealTimeSource realTimeSource)
        {
            m_realTimeSource = realTimeSource;
        }

        public PlayerStateDto Create()
        {
            return new PlayerStateDto(Guid.NewGuid().ToString(), m_realTimeSource.GetTimestamp())
            {
                Currencies =
                {
                    SoftCurrency = 100,
                    HardCurrency = 50,
                },
                EnergyData =
                {
                    Energy = 100,
                }
            };
        }
    }
}
