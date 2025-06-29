using System;
using JetBrains.Annotations;
using Zenject;

namespace Proxies
{
    [UsedImplicitly]
    public class EnergyProxy
    {
        [Inject] private LocalStateProxy m_localStateProxy;
        public event Action ChangedEvent;

        public int Energy
        {
            get => m_localStateProxy.Data.energy.value;
            private set
            {
                m_localStateProxy.Data.energy.value = value;
                ChangedEvent?.Invoke();
                m_localStateProxy.MarkAsDirty();
            }
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
    }
}
