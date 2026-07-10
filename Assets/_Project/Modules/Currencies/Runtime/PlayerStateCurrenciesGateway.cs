using System;
using GameKit.Currencies.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.Currencies
{
    [UsedImplicitly]
    public class PlayerStateCurrenciesGateway : IDisposable
    {
        private readonly IPlayerStateProvider m_playerStateProvider;
        private readonly ReactiveProperty<int> m_softCurrency;
        private readonly ReactiveProperty<int> m_hardCurrency;

        public ReadOnlyReactiveProperty<int> SoftCurrency => m_softCurrency;
        public ReadOnlyReactiveProperty<int> HardCurrency => m_hardCurrency;

        public PlayerStateCurrenciesGateway(IPlayerStateProvider playerStateProvider)
        {
            m_playerStateProvider = playerStateProvider;
            m_softCurrency = new ReactiveProperty<int>(Currencies.SoftCurrency);
            m_hardCurrency = new ReactiveProperty<int>(Currencies.HardCurrency);
            m_playerStateProvider.Replaced += HandlePlayerStateReplaced;
            m_playerStateProvider.Refreshed += HandlePlayerStateRefreshed;
        }

        public int Get(CurrencyType type)
        {
            return type switch
            {
                CurrencyType.Soft => Currencies.SoftCurrency,
                CurrencyType.Hard => Currencies.HardCurrency,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Set(CurrencyType type, int value)
        {
            if (Get(type) == value)
            {
                return;
            }

            m_playerStateProvider.Edit(state =>
            {
                switch (type)
                {
                    case CurrencyType.Soft:
                        state.Currencies.SoftCurrency = value;
                        break;
                    case CurrencyType.Hard:
                        state.Currencies.HardCurrency = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            });

            SetPropertyValue(type, value);
        }

        public void Dispose()
        {
            m_playerStateProvider.Replaced -= HandlePlayerStateReplaced;
            m_playerStateProvider.Refreshed -= HandlePlayerStateRefreshed;
            m_softCurrency.Dispose();
            m_hardCurrency.Dispose();
        }

        private void HandlePlayerStateReplaced()
        {
            RefreshProperties();
        }

        private void HandlePlayerStateRefreshed()
        {
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            m_softCurrency.Value = Currencies.SoftCurrency;
            m_hardCurrency.Value = Currencies.HardCurrency;
        }

        private void SetPropertyValue(CurrencyType type, int value)
        {
            switch (type)
            {
                case CurrencyType.Soft:
                    m_softCurrency.Value = value;
                    break;
                case CurrencyType.Hard:
                    m_hardCurrency.Value = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private PlayerCurrenciesDto Currencies => m_playerStateProvider.Data.Currencies;
    }
}
