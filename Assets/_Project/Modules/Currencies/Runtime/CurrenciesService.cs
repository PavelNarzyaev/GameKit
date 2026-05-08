using System;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Currencies
{
    [UsedImplicitly]
    public class CurrenciesService : ICurrencyWallet
    {
        [Inject] private PlayerStateCurrenciesGateway m_gateway;
        public event Action Changed;

        [Inject]
        private void Inject()
        {
            m_gateway.Changed += HandleGatewayChanged;
        }

        public int Get(CurrencyType type)
        {
            return m_gateway.Get(type);
        }

        public bool TryAdd(CurrencyType type, int amount)
        {
            if (amount < 1)
            {
                return false;
            }

            var currentValue = Get(type);
            var nextValue = (long)currentValue + amount;
            if (nextValue > int.MaxValue)
            {
                return false;
            }

            m_gateway.Set(type, (int)nextValue);
            return true;
        }

        public bool TrySpend(CurrencyType type, int amount)
        {
            if (amount < 1)
            {
                return false;
            }

            var currentValue = Get(type);
            if (currentValue < amount)
            {
                return false;
            }

            m_gateway.Set(type, currentValue - amount);
            return true;
        }

        private void HandleGatewayChanged()
        {
            Changed?.Invoke();
        }
    }
}
