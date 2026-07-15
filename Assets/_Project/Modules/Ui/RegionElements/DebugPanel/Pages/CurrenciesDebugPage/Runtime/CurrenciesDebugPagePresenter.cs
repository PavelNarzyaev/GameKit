using GameKit.Currencies.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.CurrenciesDebugPage
{
    [UsedImplicitly]
    public class CurrenciesDebugPagePresenter
    {
        private readonly ICurrenciesService m_currenciesService;

        public CurrenciesDebugPagePresenter(ICurrenciesService currenciesService)
        {
            m_currenciesService = currenciesService;
        }

        public ReadOnlyReactiveProperty<int> Get(CurrencyType type)
        {
            return m_currenciesService.Get(type);
        }

        public void SpendSoft(int amount)
        {
            m_currenciesService.TrySpend(CurrencyType.Soft, amount);
        }

        public void AddSoft(int amount)
        {
            m_currenciesService.TryAdd(CurrencyType.Soft, amount);
        }

        public void SpendHard(int amount)
        {
            m_currenciesService.TrySpend(CurrencyType.Hard, amount);
        }

        public void AddHard(int amount)
        {
            m_currenciesService.TryAdd(CurrencyType.Hard, amount);
        }
    }
}
