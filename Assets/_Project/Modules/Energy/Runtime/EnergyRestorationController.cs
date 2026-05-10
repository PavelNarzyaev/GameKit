using GameKit.Core.Contracts;
using GameKit.Energy.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Energy
{
    [UsedImplicitly]
    public class EnergyRestorationController
    {
        [Inject] private IEnergyService m_energyService;
        [Inject] private IGameTickSource m_gameTickSource;

        [Inject]
        private void Inject()
        {
            m_gameTickSource.Ticked += HandleTicked;
        }

        private void HandleTicked()
        {
            m_energyService.ProcessPendingRestoration();
        }
    }
}
