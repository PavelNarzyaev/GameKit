using System;
using Data;
using JetBrains.Annotations;
using ScriptableObjects.Configs;
using Zenject;

namespace Proxies
{
    [UsedImplicitly]
    public class EnergyProxy
    {
        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private CurrentTimeProxy m_currentTimeProxy;
        [Inject] private MainConfig m_mainConfig;
        public event Action ChangedEvent;

        private EnergyData EnergyData => m_localStateProxy.Data.energyData;

        public int Energy
        {
            get => EnergyData.energy;
            private set
            {
                EnergyData.energy = value;
                ChangedEvent?.Invoke();
                RefreshRestorationDataIfNeeded();
                m_localStateProxy.MarkAsDirty();
            }
        }

        public bool IsRestorationInProgress
        {
            get => EnergyData.isRestorationInProgress;
            private set
            {
                EnergyData.isRestorationInProgress = value;
                m_localStateProxy.MarkAsDirty();
            }
        }

        private long RestorationStartTimestamp
        {
            get => EnergyData.restorationStartTimestamp;
            set
            {
                EnergyData.restorationStartTimestamp = value;
                m_localStateProxy.MarkAsDirty();
            }
        }

        public int Restored
        {
            get => EnergyData.restored;
            private set
            {
                EnergyData.restored = value;
                m_localStateProxy.MarkAsDirty();
            }
        }

        public void Restore(int number)
        {
            Restored += number;
            Add(number);
        }

        public void Add(int number)
        {
            Energy += number;
        }

        public bool TryToSpend(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (Energy < number)
            {
                return false;
            }

            Energy -= number;
            return true;
        }

        public void RefreshRestorationDataIfNeeded()
        {
            var isRestorationNeeded = Energy < m_mainConfig.energyRestorationLimit;
            if (isRestorationNeeded == IsRestorationInProgress)
            {
                return;
            }

            IsRestorationInProgress = isRestorationNeeded;
            if (!IsRestorationInProgress)
            {
                return;
            }

            Restored = 0;
            RestorationStartTimestamp = m_currentTimeProxy.GetTimestamp();
        }

        public TimeSpan GetRestorationTimer()
        {
            if (!IsRestorationInProgress)
            {
                return TimeSpan.Zero;
            }

            var remainder = GetElapsedSecondsSinceRestorationStart() % m_mainConfig.oneEnergyRestorationSeconds;
            var timerSeconds = m_mainConfig.oneEnergyRestorationSeconds - remainder;
            return TimeSpan.FromSeconds(timerSeconds);
        }

        public long GetElapsedSecondsSinceRestorationStart()
        {
            if (!IsRestorationInProgress)
            {
                return 0;
            }

            return m_currentTimeProxy.GetTimestamp() - RestorationStartTimestamp;
        }
    }
}
