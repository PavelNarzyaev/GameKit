using System;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class PlayerStateEnergyGateway
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

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
