using System;
using System.Globalization;
using GameKit.Energy.Contracts;
using JetBrains.Annotations;

namespace GameKit.EnergyDebugPage
{
    [UsedImplicitly]
    public class EnergyDebugPagePresenter
    {
        private readonly IEnergyService m_energyService;
        private readonly IEnergyConfig m_energyConfig;

        public EnergyDebugPagePresenter(IEnergyService energyService, IEnergyConfig energyConfig)
        {
            m_energyService = energyService;
            m_energyConfig = energyConfig;
        }

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
