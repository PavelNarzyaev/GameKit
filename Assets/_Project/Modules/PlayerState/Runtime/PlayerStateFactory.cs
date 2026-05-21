using System;
using GameKit.CurrentTime.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class PlayerStateFactory : IPlayerStateFactory
    {
        private readonly ICurrentTimeProvider m_currentTimeProvider;

        public PlayerStateFactory(ICurrentTimeProvider currentTimeProvider)
        {
            m_currentTimeProvider = currentTimeProvider;
        }

        public PlayerStateDto Create()
        {
            return new PlayerStateDto
            {
                UserId = Guid.NewGuid().ToString(),
                FirstLaunchTimestamp = m_currentTimeProvider.GetTimestamp(),
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
