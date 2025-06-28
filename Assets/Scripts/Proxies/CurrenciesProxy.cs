using System;
using Data;
using JetBrains.Annotations;
using Zenject;

namespace Proxies
{
    [UsedImplicitly]
    public class CurrenciesProxy
    {
        [Inject] private LocalStateProxy m_localStateProxy;
        public event Action SoftChangedEvent;
        public event Action HardChangedEvent;
        public event Action EnergyChangedEvent;

        public int Soft
        {
            get => Currencies.softCurrency;
            private set
            {
                Currencies.softCurrency = value;
                SoftChangedEvent?.Invoke();
            }
        }

        public int Hard
        {
            get => Currencies.hardCurrency;
            private set
            {
                Currencies.hardCurrency = value;
                HardChangedEvent?.Invoke();
            }
        }

        public int Energy
        {
            get => Currencies.energy;
            private set
            {
                Currencies.energy = value;
                EnergyChangedEvent?.Invoke();
            }
        }

        private Currencies Currencies => m_localStateProxy.Data.currencies;

        public void AddSoft(int number)
        {
            Soft += number;
        }

        public bool TryToSpendSoft(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (Soft < number)
            {
                return false;
            }

            Soft -= number;
            return true;
        }

        public void AddHard(int number)
        {
            Hard += number;
        }

        public bool TryToSpendHard(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (Hard < number)
            {
                return false;
            }

            Hard -= number;
            return true;
        }

        public void AddEnergy(int number)
        {
            Energy += number;
        }

        public bool TryToSpendEnergy(int number)
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
