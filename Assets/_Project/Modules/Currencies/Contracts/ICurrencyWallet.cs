using System;

namespace GameKit.Currencies
{
    public interface ICurrencyWallet
    {
        event Action Changed;

        int Get(CurrencyType type);
        bool TryAdd(CurrencyType type, int amount);
        bool TrySpend(CurrencyType type, int amount);
    }
}
