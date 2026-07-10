using R3;

namespace GameKit.Currencies.Contracts
{
    public interface ICurrencyWallet
    {
        ReadOnlyReactiveProperty<int> SoftCurrency { get; }
        ReadOnlyReactiveProperty<int> HardCurrency { get; }

        int Get(CurrencyType type);
        bool TryAdd(CurrencyType type, int amount);
        bool TrySpend(CurrencyType type, int amount);
    }
}
