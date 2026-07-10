using GameKit.Currencies.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.CurrenciesDebugPage
{
    [UsedImplicitly]
    public class CurrenciesDebugPagePresenter
    {
        private readonly ICurrencyWallet m_currencyWallet;

        public CurrenciesDebugPagePresenter(ICurrencyWallet currencyWallet)
        {
            m_currencyWallet = currencyWallet;
        }

        public ReadOnlyReactiveProperty<int> SoftCurrency => m_currencyWallet.SoftCurrency;
        public ReadOnlyReactiveProperty<int> HardCurrency => m_currencyWallet.HardCurrency;

        public void SpendSoft(int amount)
        {
            m_currencyWallet.TrySpend(CurrencyType.Soft, amount);
        }

        public void AddSoft(int amount)
        {
            m_currencyWallet.TryAdd(CurrencyType.Soft, amount);
        }

        public void SpendHard(int amount)
        {
            m_currencyWallet.TrySpend(CurrencyType.Hard, amount);
        }

        public void AddHard(int amount)
        {
            m_currencyWallet.TryAdd(CurrencyType.Hard, amount);
        }
    }
}
