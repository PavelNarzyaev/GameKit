using System;
using GameKit.CurrentTime;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class EnergyService : IEnergyService
    {
        [Inject] private PlayerStateEnergyGateway m_gateway;
        [Inject] private ICurrentTimeProvider m_currentTimeProvider;
        [Inject] private IEnergyConfig m_energyConfig;
        public event Action Changed;

        private int RestorationStepSeconds => Math.Max(1, m_energyConfig.OneEnergyRestorationSeconds);

        [Inject]
        private void Inject()
        {
            m_gateway.Changed += HandleGatewayChanged;
        }

        public int Energy => m_gateway.Energy;
        public bool IsRestorationInProgress => Energy < m_energyConfig.EnergyRestorationLimit;

        public bool TryAdd(int amount)
        {
            if (amount < 1)
            {
                return false;
            }

            var nextEnergy = (long)Energy + amount;
            if (nextEnergy > int.MaxValue)
            {
                return false;
            }

            SetState((int)nextEnergy, GetNextRestoreTimestampAfterExternalChange((int)nextEnergy));
            return true;
        }

        public bool TrySpend(int amount)
        {
            if (amount < 1)
            {
                return false;
            }

            if (Energy < amount)
            {
                return false;
            }

            var nextEnergy = Energy - amount;
            SetState(nextEnergy, GetNextRestoreTimestampAfterExternalChange(nextEnergy));
            return true;
        }

        public TimeSpan GetRestorationTimer()
        {
            if (!IsRestorationInProgress)
            {
                return TimeSpan.Zero;
            }

            var nextRestoreTimestamp = m_gateway.NextRestoreTimestamp;
            if (nextRestoreTimestamp <= 0)
            {
                return TimeSpan.FromSeconds(RestorationStepSeconds);
            }

            var remainingSeconds = nextRestoreTimestamp - m_currentTimeProvider.GetTimestamp();
            if (remainingSeconds <= 0)
            {
                return TimeSpan.Zero;
            }

            return TimeSpan.FromSeconds(remainingSeconds);
        }

        public void ProcessPendingRestoration()
        {
            if (!IsRestorationInProgress)
            {
                if (m_gateway.NextRestoreTimestamp != 0)
                {
                    SetState(Energy, 0);
                }

                return;
            }

            var nextRestoreTimestamp = m_gateway.NextRestoreTimestamp;
            if (nextRestoreTimestamp <= 0)
            {
                SetState(Energy, GetTimestampAfterSingleStep());
                return;
            }

            var currentTimestamp = m_currentTimeProvider.GetTimestamp();
            if (currentTimestamp < nextRestoreTimestamp)
            {
                return;
            }

            var elapsedSinceNextRestore = currentTimestamp - nextRestoreTimestamp;
            var restoredByTime = 1 + (int)(elapsedSinceNextRestore / RestorationStepSeconds);
            var availableCapacity = m_energyConfig.EnergyRestorationLimit - Energy;
            var restoredEnergy = Math.Min(restoredByTime, availableCapacity);
            var nextEnergy = Energy + restoredEnergy;
            var updatedNextRestoreTimestamp = nextEnergy >= m_energyConfig.EnergyRestorationLimit
                ? 0
                : nextRestoreTimestamp + (long)restoredEnergy * RestorationStepSeconds;

            SetState(nextEnergy, updatedNextRestoreTimestamp);
        }

        private long GetNextRestoreTimestampAfterExternalChange(int nextEnergy)
        {
            if (nextEnergy >= m_energyConfig.EnergyRestorationLimit)
            {
                return 0;
            }

            if (m_gateway.NextRestoreTimestamp > 0)
            {
                return m_gateway.NextRestoreTimestamp;
            }

            return GetTimestampAfterSingleStep();
        }

        private long GetTimestampAfterSingleStep()
        {
            return m_currentTimeProvider.GetTimestamp() + RestorationStepSeconds;
        }

        private void SetState(int energy, long nextRestoreTimestamp)
        {
            m_gateway.SetState(energy, nextRestoreTimestamp);
        }

        private void HandleGatewayChanged()
        {
            Changed?.Invoke();
        }
    }
}
