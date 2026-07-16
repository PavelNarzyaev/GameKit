using System;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class PlayerStateEnergyGateway
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        public PlayerStateEnergyGateway(IPlayerStateProvider playerStateProvider)
        {
            m_playerStateProvider = playerStateProvider;
        }

        public int Energy => m_playerStateProvider.Energy.CurrentValue;
        public long NextRestoreTimestamp => m_playerStateProvider.EnergyNextRestoreTimestamp.CurrentValue;

        public void SetState(int energy, long nextRestoreTimestamp)
        {
            if (Energy == energy && NextRestoreTimestamp == nextRestoreTimestamp)
            {
                return;
            }

            m_playerStateProvider.SetEnergyState(energy, nextRestoreTimestamp);
            Changed?.Invoke();
        }
    }
}
