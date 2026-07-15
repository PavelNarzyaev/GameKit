using GameKit.Currencies.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.Currencies
{
    [UsedImplicitly]
    public class CurrenciesService : ICurrenciesService
    {
        private readonly PlayerStateCurrenciesGateway m_gateway;

        public CurrenciesService(PlayerStateCurrenciesGateway gateway)
        {
            m_gateway = gateway;
        }

        public ReadOnlyReactiveProperty<int> Get(CurrencyType type)
        {
            return m_gateway.Get(type);
        }

        public bool TryAdd(CurrencyType type, int amount)
        {
            if (amount < 1)
            {
                return false;
            }

            var currentValue = Get(type).CurrentValue;
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

            var currentValue = Get(type).CurrentValue;
            if (currentValue < amount)
            {
                return false;
            }

            m_gateway.Set(type, currentValue - amount);
            return true;
        }
    }
}
