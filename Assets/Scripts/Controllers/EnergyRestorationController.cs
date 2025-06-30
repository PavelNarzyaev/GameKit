using System;
using JetBrains.Annotations;
using Proxies;
using ScriptableObjects.Configs;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class EnergyRestorationController : ITickable
    {
        [Inject] private EnergyProxy m_energyProxy;
        [Inject] private MainConfig m_mainConfig;

        public void Tick()
        {
            m_energyProxy.RefreshRestorationDataIfNeeded();
            if (!m_energyProxy.IsRestorationInProgress)
            {
                return;
            }

            var elapsedSeconds = m_energyProxy.GetElapsedSecondsSinceRestorationStart();
            var energyUnitsByTime = (int)(elapsedSeconds / m_mainConfig.oneEnergyRestorationSeconds);
            var energyToRestore = energyUnitsByTime - m_energyProxy.Restored;
            if (energyToRestore == 0)
            {
                return;
            }

            var availableRestoreCapacity = m_mainConfig.energyRestorationLimit - m_energyProxy.Energy;
            energyToRestore = Math.Min(energyToRestore, availableRestoreCapacity);
            m_energyProxy.Restore(energyToRestore);
        }
    }
}
