using System;
using System.Globalization;
using GameKit.Energy;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.EnergyDebugPage
{
    [UsedImplicitly]
    public class EnergyDebugPagePresenter
    {
        [Inject] private IEnergyService m_energyService;
        [Inject] private IEnergyConfig m_energyConfig;

        public event Action Changed
        {
            add => m_energyService.Changed += value;
            remove => m_energyService.Changed -= value;
        }

        public void TrySpend(int amount)
        {
            m_energyService.TrySpend(amount);
        }

        public void TryAdd(int amount)
        {
            m_energyService.TryAdd(amount);
        }

        public string GetEnergyText()
        {
            return m_energyService.Energy.ToString(CultureInfo.InvariantCulture);
        }

        public string GetOneEnergyRestorationSecondsText()
        {
            return m_energyConfig.OneEnergyRestorationSeconds.ToString(CultureInfo.InvariantCulture);
        }

        public string GetRestorationLimitText()
        {
            return m_energyConfig.EnergyRestorationLimit.ToString(CultureInfo.InvariantCulture);
        }

        public string GetRestorationTimerText()
        {
            if (!m_energyService.IsRestorationInProgress)
            {
                return "---";
            }

            return m_energyService.GetRestorationTimer().ToString();
        }
    }
}
