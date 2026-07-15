using R3;

namespace GameKit.Currencies.Contracts
{
    public interface ICurrenciesService
    {
        ReadOnlyReactiveProperty<int> Get(CurrencyType type);
        bool TryAdd(CurrencyType type, int amount);
        bool TrySpend(CurrencyType type, int amount);
    }
}
