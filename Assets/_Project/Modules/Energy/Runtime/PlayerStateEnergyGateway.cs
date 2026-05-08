using System;
using GameKit.PlayerState;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class PlayerStateEnergyGateway
    {
        [Inject] private PlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        [Inject]
        private void Inject()
        {
            m_playerStateProvider.RefreshedFromJson += HandlePlayerStateRefreshedFromJson;
        }

        public int Energy => EnergyData.Energy;
        public long NextRestoreTimestamp => EnergyData.NextRestoreTimestamp;

        public void SetState(int energy, long nextRestoreTimestamp)
        {
            if (EnergyData.Energy == energy && EnergyData.NextRestoreTimestamp == nextRestoreTimestamp)
            {
                return;
            }

            EnergyData.Energy = energy;
            EnergyData.NextRestoreTimestamp = nextRestoreTimestamp;
            m_playerStateProvider.MarkAsDirty();
            Changed?.Invoke();
        }

        private PlayerEnergyDataDto EnergyData => m_playerStateProvider.Data.EnergyData ??= new PlayerEnergyDataDto();

        private void HandlePlayerStateRefreshedFromJson()
        {
            Changed?.Invoke();
        }
    }
}
