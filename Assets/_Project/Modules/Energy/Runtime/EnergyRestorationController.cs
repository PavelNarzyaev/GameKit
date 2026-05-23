using System;
using GameKit.Core.Contracts;
using GameKit.Energy.Contracts;
using JetBrains.Annotations;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class EnergyRestorationController : IDisposable
    {
        private readonly IEnergyService m_energyService;
        private readonly IGameTickSource m_gameTickSource;

        public EnergyRestorationController(IEnergyService energyService, IGameTickSource gameTickSource)
        {
            m_energyService = energyService;
            m_gameTickSource = gameTickSource;
            m_gameTickSource.Ticked += HandleTicked;
        }

        public void Dispose()
        {
            m_gameTickSource.Ticked -= HandleTicked;
        }

        private void HandleTicked()
        {
            m_energyService.ProcessPendingRestoration();
        }
    }
}
