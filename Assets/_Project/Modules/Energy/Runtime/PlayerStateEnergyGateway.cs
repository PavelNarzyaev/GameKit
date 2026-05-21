using System;
using GameKit.Energy.Contracts;
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

        public int Energy => EnergyData.Energy;
        public long NextRestoreTimestamp => EnergyData.NextRestoreTimestamp;

        public void SetState(int energy, long nextRestoreTimestamp)
        {
            if (EnergyData.Energy == energy && EnergyData.NextRestoreTimestamp == nextRestoreTimestamp)
            {
                return;
            }

            m_playerStateProvider.Edit(state =>
            {
                state.EnergyData.Energy = energy;
                state.EnergyData.NextRestoreTimestamp = nextRestoreTimestamp;
            });

            Changed?.Invoke();
        }

        private PlayerEnergyDataDto EnergyData => m_playerStateProvider.Data.EnergyData;
    }
}
