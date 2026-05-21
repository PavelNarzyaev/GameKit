using System;
using System.Globalization;
using GameKit.Currencies.Contracts;
using JetBrains.Annotations;

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

        public event Action Changed
        {
            add => m_currencyWallet.Changed += value;
            remove => m_currencyWallet.Changed -= value;
        }

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

        public string GetSoftText()
        {
            return m_currencyWallet.Get(CurrencyType.Soft).ToString(CultureInfo.InvariantCulture);
        }

        public string GetHardText()
        {
            return m_currencyWallet.Get(CurrencyType.Hard).ToString(CultureInfo.InvariantCulture);
        }
    }
}
